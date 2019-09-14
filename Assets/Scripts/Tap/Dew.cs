using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Tap
{

    public class Dew : MonoBehaviour
    {
        private int hpRecoveryAmount = 0;

        public void Setup(int hpRecoveryAmount)
        {
            this.hpRecoveryAmount = hpRecoveryAmount;
        }

        public int HPRecoveryAmount { get { return hpRecoveryAmount; } }
        public int HalfHPLoseAmount { get { return hpRecoveryAmount / 2; } }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BottomCollider"))
            {
                GameManager.Instance.GetFightManager.GetTap.ReturnDewToPool(this);
            }
        }
    }
}