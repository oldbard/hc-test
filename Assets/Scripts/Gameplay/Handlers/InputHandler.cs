using System;
using UnityEngine;

namespace Gameplay.Handlers
{
    public class InputHandler : MonoBehaviour
    {
        public Action PressedHit;
        public Action ReleasedHit;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PressedHit?.Invoke();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ReleasedHit?.Invoke();
            }

        }
    }
}