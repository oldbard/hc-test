using Gameplay.Handlers;
using System;
using UnityEngine;

namespace Gameplay.Controllers
{
    public class InputRecorder : MonoBehaviour
    {
        [SerializeField] InputHandler _inputHandler;
//        [SerializeField] GameController _gameController;

        int _levelId;
        int _inputId;
        int _totalInputs = 0;

        DateTime _startTime;

        DateTime _currentPressStart;
        double _timeSinceStart;

        public void StartRecording(int levelId)
        {
            _levelId = levelId;
            _startTime = DateTime.UtcNow;

            _inputHandler.PressedHit += OnPressedHit;
            _inputHandler.ReleasedHit += OnReleasedHit;
        }

        public void FinishRecording()
        {
            _inputHandler.PressedHit -= OnPressedHit;
            _inputHandler.ReleasedHit -= OnReleasedHit;

            PlayerPrefs.SetInt($"TotalInputs{_levelId}", _totalInputs);

            PlayerPrefs.Save();
        }

        void OnPressedHit()
        {
            _currentPressStart = DateTime.UtcNow;
            _timeSinceStart = (DateTime.UtcNow - _startTime).TotalSeconds;
            PlayerPrefs.SetString($"Input{_levelId}_{_totalInputs}_started", _timeSinceStart.ToString());

            PlayerPrefs.Save();
        }

        void OnReleasedHit()
        {
            var timePressed = (DateTime.UtcNow - _currentPressStart).TotalSeconds;

            PlayerPrefs.SetString($"Input{_levelId}_{_totalInputs}_duration", timePressed.ToString());
            PlayerPrefs.Save();

            _totalInputs++;
        }
    }
}