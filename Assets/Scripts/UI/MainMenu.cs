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

        private void OnEnable()
        {
            GameManager.Instance?.GetCanvasSystem.GetTimer.gameObject.SetActive(false);
            GameManager.Instance?.GetCanvasSystem.ActivatePlayerUIs(false);
        }
    }
}
