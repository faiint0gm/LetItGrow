using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PointElement : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private Sprite playerGirlNotSetPoint;
        [SerializeField]
        private Sprite playerGirlWonPoint;
        [SerializeField]
        private Sprite playerBoyNotSetPoint;
        [SerializeField]
        private Sprite playerBoyWonPoint;

        RoundType roundType;

        public void Setup(PlayerColor playerColor, RoundType roundType)
        {
            this.roundType = roundType;
                switch(roundType)
                {
                    case RoundType.NotSet:
                    {
                        image.sprite = playerColor == PlayerColor.Pink? playerGirlNotSetPoint : playerBoyNotSetPoint; break;
                    }
                    case RoundType.Won:
                    {
                        image.sprite = playerColor == PlayerColor.Pink ? playerGirlWonPoint : playerBoyWonPoint; break;
                    }
                }
        }
    }
}
