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
        private Button playButton;
        [SerializeField]
        private PlayerUI playerOneUI;
        [SerializeField]
        private PlayerUI playerTwoUI;
        [SerializeField]

        public Timer GetTimer { get { return timer; } }
        public Button PlayButton { get { return playButton; } }

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
    }

}