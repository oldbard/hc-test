using Gameplay.Controllers;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Input
{
    public class SavedInputDispatcher : InputDispatcher
    {
        [SerializeField] TextAsset[] _savedData;

        InputData _loadedData;
        void Start()
        {
            _getInputs = false;

            LoadRandomData();

            DoRun();
        }

        void LoadRandomData()
        {
            var data = _savedData[Random.Range(0, _savedData.Length)];

            Debug.Log($"Using Data: {data.name}");

            _loadedData = JsonUtility.FromJson<InputData>(data.text);
        }

        async Task DoRun()
        {
            foreach (var step in _loadedData.Steps)
            {
                await Task.Delay(step.SecondsToStart);

                PressedHit?.Invoke();

                await Task.Delay(step.SecondsToEnd);

                ReleasedHit?.Invoke();
            }
        }
    }
}