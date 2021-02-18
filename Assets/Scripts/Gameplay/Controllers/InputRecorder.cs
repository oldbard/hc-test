using Gameplay.Input;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Controllers
{
    [Serializable]
    public class InputData
    {
        public List<InputDataStep> Steps = new List<InputDataStep>();
        public int Level;

        public InputData(int level)
        {
            Level = level;
        }

        public void AddStep(InputDataStep step)
        {
            Steps.Add(step);
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }

    [Serializable]
    public class InputDataStep
    {
        public int SecondsToStart;
        public int SecondsToEnd;
    }

    public class InputRecorder : MonoBehaviour
    {
        [SerializeField] InputDispatcher _inputHandler;
        [SerializeField] GameController _gameController;

        InputData _inputData;
        InputDataStep _currentStep;

        DateTime _noInputsStartTime;
        DateTime _currentPressStart;

        bool _savingStep;

        void Start()
        {
            StartRecording(1);
        }

        public void StartRecording(int levelId)
        {
            Debug.Log("Started Recording");
            _inputData = new InputData(levelId);

            _noInputsStartTime = DateTime.UtcNow;

            _inputHandler.PressedHit += OnPressedHit;
            _inputHandler.ReleasedHit += OnReleasedHit;
            _gameController.GameOver += OnGameOver;
        }

        public void FinishRecording()
        {
            _inputHandler.PressedHit -= OnPressedHit;
            _inputHandler.ReleasedHit -= OnReleasedHit;

            var json = _inputData.ToJson();
            Debug.Log(json);

            var filePath = $"{Application.persistentDataPath}/level{_inputData.Level}.json";
            var outStream = System.IO.File.CreateText(filePath);
            outStream.WriteLine(json);
            outStream.Close();
        }

        void StartStep()
        {
            _savingStep = true;

            _currentStep = new InputDataStep();
            _currentPressStart = DateTime.UtcNow;
            _currentStep.SecondsToStart = (int)(_currentPressStart - _noInputsStartTime).TotalMilliseconds;
        }

        void EndStep()
        {
            _savingStep = false;

            _noInputsStartTime = DateTime.UtcNow;
            _currentStep.SecondsToEnd = (int)(_noInputsStartTime - _currentPressStart).TotalMilliseconds;
            _inputData.AddStep(_currentStep);
        }

        void OnPressedHit()
        {
            Debug.Log("OnPressedHit");

            StartStep();
        }

        void OnReleasedHit()
        {
            Debug.Log("OnReleasedHit");

            EndStep();
        }

        void OnGameOver(bool winner)
        {
            Debug.Log("OnGameOver");
            _gameController.GameOver -= OnGameOver;

            if(_savingStep)
            {
                EndStep();
            }
            
            FinishRecording();
        }
    }
}