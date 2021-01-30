using UnityEngine;

namespace Gameplay.Objects
{
    public class Obstacle : MonoBehaviour
    {
        protected Transform _transform;

        protected virtual void Awake()
        {
            _transform = transform;
        }
    }
}