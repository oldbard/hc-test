using System;
using UnityEngine;

namespace Gameplay.Input
{
    public class InputDispatcher : MonoBehaviour
    {
        public Action PressedHit;
        public Action ReleasedHit;

        protected bool _getInputs = true;

        protected virtual void Update()
        {
            if (!_getInputs) return;

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                PressedHit?.Invoke();
            }
            else if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                ReleasedHit?.Invoke();
            }

        }
    }
}