using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void ShowMenu()
        {
            GameManager.Instance.gameState = Enums.GameState.InMenu;
            gameObject.SetActive(true);
        }

        public void HideMenu()
        {
            gameObject.SetActive(false);
        }

        public void Fight()
        {
            GameManager.Instance.gameState = Enums.GameState.InBattle;
            GameManager.Instance.GetCanvasSystem.Tutorial.RunTutorial();
        }

        public void RunAway()
        {
            Application.Quit();
        }

        private void OnEnable()
        {
            GameManager.Instance?.GetCanvasSystem.GetTimer.gameObject.SetActive(false);
            GameManager.Instance?.GetCanvasSystem.ActivatePlayerUIs(false);
        }
    }
}
