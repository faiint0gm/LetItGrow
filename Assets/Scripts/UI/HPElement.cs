using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class HPElement : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI hpText;
        [SerializeField]
        private Image valueImage;

        public void SetValue(int value)
        {
            valueImage.fillAmount = value / 100.0f;
            hpText.text = value.ToString();
        }
    }
}