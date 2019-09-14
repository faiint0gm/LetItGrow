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
            gameObject.SetActive(true);
        }

        public void HideMenu()
        {
            gameObject.SetActive(false);
        }

        public void Fight()
        {
            GameManager.Instance.PrepareFight();
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
