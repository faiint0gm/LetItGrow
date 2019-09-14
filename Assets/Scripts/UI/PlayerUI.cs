using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField]
        PlayerType playerType;
        [SerializeField]
        PointElement[] pointElements;
        [SerializeField]
        HPElement hpElement;

        PlayerColor playerColor;

        public HPElement HpElement { get { return hpElement; } }
        public PointElement GetPointElement(int index)
        {
            return pointElements[index];
        }
        public void SetPlayerColor(PlayerColor color)
        {
            playerColor = color;
            foreach(PointElement e in pointElements)
            {
                e.Setup(color, RoundType.NotSet);
            }
        }

        public void SetWin(int index)
        {
            GetPointElement(index)?.Setup(playerColor, RoundType.Won);
        }
    }
}
