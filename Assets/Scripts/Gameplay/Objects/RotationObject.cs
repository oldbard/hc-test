using UnityEngine;

namespace Gameplay.Objects
{
    public class RotationObject : Obstacle
    {
        [SerializeField] bool _clockWise;
        [SerializeField] float _rotationSpeed;

        private void Update()
        {
            var speed = _rotationSpeed * (_clockWise ? 1f : -1f);

            _transform.Rotate(0f, speed * Time.deltaTime, 0f);
        }
    }
}