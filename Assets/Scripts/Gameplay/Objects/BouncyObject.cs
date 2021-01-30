
using UnityEngine;

namespace Gameplay.Objects
{
    public class BouncyObject : Obstacle
    {
        [SerializeField] float _moveDistance = 0f;
        [SerializeField] float _moveSpeed = 0f;
        [SerializeField] Vector3 _initialDirection = Vector3.right;

        Transform _transform;

        Vector3 _curDirection = Vector3.right;
        Vector3 _endPosition;

        void Awake()
        {
            _transform = transform;
            _curDirection = _initialDirection;
            _endPosition = _transform.position + _curDirection * _moveDistance;
        }

        void Update()
        {
            var step = _curDirection * _moveSpeed * Time.deltaTime;

            var dist = (_endPosition - _transform.position).sqrMagnitude;

            if (step.sqrMagnitude > dist)
            {
                _transform.position = _endPosition;
                ChangeDirection();
            }
            else
            {
                _transform.position += step;
            }
        }

        void ChangeDirection()
        {
            _curDirection *= -1;
            _endPosition = _transform.position + _curDirection * _moveDistance;
        }
    }
}