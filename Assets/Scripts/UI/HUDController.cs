using Gameplay.Controllers;
using Gameplay.Handlers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] GameController _gameController;
        [SerializeField] InputHandler _inputHandler;
        [SerializeField] GameOverScreen _endGameScreen;
        [SerializeField] Text tapToMoveText;

        bool _gameIsOver;

        void Awake()
        {
            _gameController.GameOver += OnGameOver;
            //_endGameScreen.ContinueClicked += OnContinue;
            _inputHandler.PressedHit += OnPressedHit;
            _inputHandler.ReleasedHit += OnReleasedHit;
        }

        void OnDestroy()
        {
            _gameController.GameOver -= OnGameOver;
            //_endGameScreen.ContinueClicked -= OnContinue;
            _inputHandler.PressedHit -= OnPressedHit;
            _inputHandler.ReleasedHit -= OnReleasedHit;
        }

        void OnPressedHit()
        {
            if (_gameIsOver)
            {
                SceneManager.LoadScene("GameplayScene");
            }

            tapToMoveText.gameObject.SetActive(false);
        }

        void OnReleasedHit()
        {
            tapToMoveText.gameObject.SetActive(true);
        }

        void OnGameOver(bool victory)
        {
            _gameIsOver = true;
            _endGameScreen.ShowGameEndScreen(victory);
        }
    }
}