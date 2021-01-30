using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] Text _text;
        //public Action ContinueClicked;
        
        public void ShowGameEndScreen(bool victory)
        {
            _text.text = victory ? "You Won!" : "You Lost";
            gameObject.SetActive(true);
        }
        /*
        public void OnContinue()
        {
            ContinueClicked?.Invoke();
        }*/
    }
}