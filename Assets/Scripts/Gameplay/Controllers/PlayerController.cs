using Gameplay.Handlers;
using Gameplay.Objects;
using System.Collections;
using UnityEngine;

namespace Gameplay.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        const string AnimRun = "runStart";
        const string AnimMoving = "Moving";
        const string AnimDead = "Dead";
        const string AnimDance = "Dance";
        const string AnimHit = "Hit";

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
        bool _dead;

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
            if(_dead) return;

            _moving = true;
            _animator.Play(Animator.StringToHash(AnimRun));
            _animator.SetBool(Animator.StringToHash(AnimMoving), true);
        }

        void StopRunning()
        {
            if(_dead) return;

            _moving = false;
            _animator.SetBool(Animator.StringToHash(AnimMoving), false);
        }

        public void SetTargetNode(Vector3 nodePos)
        {
            UpdateCheckPointPos();
            _targetNodePos = nodePos;
        }

        public void CompletedRun(int winnerId)
        {
            StopRunning();
            UnregisterEvents();
            _animator.SetBool(Animator.StringToHash(winnerId == _id ? AnimDance : AnimDead), true);
            if(winnerId != _id)
            {
                //_animator.SetInt("", 1);
            }
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
            bool wasHit = false;

            var collider = collision.collider.GetComponent<Obstacle>();

            wasHit = collider != null;

            if(!wasHit)
            {
                var enemy = collision.collider.GetComponent<PlayerController>();

                wasHit = enemy != null;
            }

            if(wasHit)
            {
                Debug.Log("Ops! Hit an obstacle!");
                StartCoroutine(PlayDeath());
            }
        }

        IEnumerator PlayDeath()
        {
            _moving = false;
            _dead = true;

            _animator.SetTrigger(Animator.StringToHash(AnimHit));
            _animator.SetBool(Animator.StringToHash(AnimDead), true);

            yield return new WaitForSeconds(2f);

            _animator.SetBool(AnimDead, false);

            _transform.position = _checkPointPos;
            _dead = false;
        }
    }
}