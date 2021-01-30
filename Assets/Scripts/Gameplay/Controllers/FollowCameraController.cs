using UnityEngine;

namespace Gameplay.Controllers
{
    public class FollowCameraController : MonoBehaviour
    {
        [SerializeField] PlayerController _player;
        [SerializeField] float _distance;
        [SerializeField] float _damping;

        Transform _transform;
        Vector3 _distancePos;

        void Start()
        {
            _transform = transform;
            var dir = _transform.position - _player.Position;

            _distancePos = dir.normalized * _distance;
        }

        void LateUpdate()
        {
            Vector3 desiredPosition = _player.Position + _distancePos;
            _transform.position = Vector3.Lerp(_transform.position, desiredPosition, Time.deltaTime * _damping);

            _transform.LookAt(_player.Position);
        }
    }
}