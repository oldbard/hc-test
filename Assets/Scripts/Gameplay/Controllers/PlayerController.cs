using Gameplay.Handlers;
using Gameplay.Objects;
using UnityEngine;

namespace Gameplay.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        const string AnimRun = "runStart";
        const string AnimMoving = "Moving";
        const string AnimDead = "Dead";
        const string AnimDance = "Dance";

        [SerializeField] Animator _animator;
        [SerializeField] InputHandler _inputHandler;
        [SerializeField] float _moveSpeed;

        int _id;
        public int Id
        {
            get => _id;
        }

        Transform _transform;
        Vector3 _targetNodePos;
        Vector3 _checkPointPos;

        bool _moving;

        public Vector3 Position
        {
            get => _transform.position;
        }

        void Awake()
        {
            _transform = transform;            
        }

        public void Init(int id)
        {
            _id = id;
            RegisterEvents();
        }

        void OnDestroy()
        {
            UnregisterEvents();
        }

        void Update()
        {
            LookAt();

            if(_moving)
            {
                Move();
            }
        }

        void RegisterEvents()
        {
            _inputHandler.PressedHit += OnPressedHit;
            _inputHandler.ReleasedHit += OnReleasedHit;
        }

        void UnregisterEvents()
        {
            _inputHandler.PressedHit -= OnPressedHit;
            _inputHandler.ReleasedHit -= OnReleasedHit;
        }

        void OnPressedHit()
        {
            StartRunning();
        }

        void OnReleasedHit()
        {
            StopRunning();
        }

        void StartRunning()
        {
            _moving = true;
            _animator.Play(Animator.StringToHash(AnimRun));
            _animator.SetBool(Animator.StringToHash(AnimMoving), true);
        }

        void StopRunning()
        {
            _moving = false;
            _animator.SetBool(Animator.StringToHash(AnimMoving), false);
        }

        public void SetTargetNode(Vector3 nodePos)
        {
            UpdateCheckPointPos();
            _targetNodePos = nodePos;
        }

        public void CompletedRun()
        {
            StopRunning();
            UnregisterEvents();
            _animator.SetTrigger(AnimDance);
        }

        void UpdateCheckPointPos()
        {
            _checkPointPos = Position;
        }

        void LookAt()
        {
            _transform.LookAt(_targetNodePos);

            var euler = _transform.rotation.eulerAngles;
            euler.x = euler.z = 0f;

            _transform.rotation = Quaternion.Euler(euler);
        }

        void Move()
        {
            _transform.position += _transform.forward * _moveSpeed * Time.deltaTime;
        }

        void OnCollisionEnter(Collision collision)
        {
            var collider = collision.collider.GetComponent<Obstacle>();

            if(collider != null)
            {
                Debug.Log("Ops! Hit an obstacle!");
                _transform.position = _checkPointPos;
            }
        }
    }
}