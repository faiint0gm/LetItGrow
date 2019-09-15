using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Tap
{

    public class Dew : MonoBehaviour
    {
        private int hpRecoveryAmount = 0;
        public Vector3 startForce = new Vector3(0, -3, 0);
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
                GameManager.Instance.GetPlayer(Enums.PlayerType.PlayerOne).TakeDamage(HalfHPLoseAmount);
                GameManager.Instance.GetPlayer(Enums.PlayerType.PlayerTwo).TakeDamage(HalfHPLoseAmount);
                GameManager.Instance.GetFightManager.GetTap.ReturnDewToPool(this);
            }

            if (other.CompareTag("PlayerOnePot"))
            {
                GameManager.Instance.GetPlayer(Enums.PlayerType.PlayerTwo).TakeDamage(hpRecoveryAmount);
                GameManager.Instance.GetPlayer(Enums.PlayerType.PlayerOne).GetHP(hpRecoveryAmount);
                GameManager.Instance.GetFightManager.GetTap.ReturnDewToPool(this);
            }

            if (other.CompareTag("PlayerTwoPot"))
            {
                GameManager.Instance.GetPlayer(Enums.PlayerType.PlayerOne).TakeDamage(hpRecoveryAmount);
                GameManager.Instance.GetPlayer(Enums.PlayerType.PlayerTwo).GetHP(hpRecoveryAmount);
                GameManager.Instance.GetFightManager.GetTap.ReturnDewToPool(this);
            }
        }
    }
}