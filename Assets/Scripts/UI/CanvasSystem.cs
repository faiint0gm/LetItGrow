using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Enums;

namespace Assets.Scripts.UI
{
    public class CanvasSystem : MonoBehaviour
    {
        [SerializeField]
        private Timer timer;
        [SerializeField]
        private PlayerUI playerOneUI;
        [SerializeField]
        private PlayerUI playerTwoUI;
        [SerializeField]
        private InfoTexts infoTexts;
        [SerializeField]
        private MainMenu mainMenu;

        public InfoTexts InfoTextsSystem { get { return infoTexts; } }
        public Timer GetTimer { get { return timer; } }
        public MainMenu GetMainMenu { get { return mainMenu; } }

        public PlayerUI GetPlayerUI(PlayerType type)
        {
            switch (type)
            {
                case PlayerType.PlayerOne: return playerOneUI;
                case PlayerType.PlayerTwo: return playerTwoUI;
                default: return null;
            }
        }
        public void SetupPlayerHP(PlayerType type, int value)
        {
            switch (type)
            {
                case PlayerType.PlayerOne: playerOneUI.HpElement.SetValue(value); break;
                case PlayerType.PlayerTwo: playerTwoUI.HpElement.SetValue(value); break;
            }
        }

        public void ActivatePlayerUIs(bool isActive)
        {
            playerOneUI.gameObject.SetActive(isActive);
            playerTwoUI.gameObject.SetActive(isActive);
        }
    }

}