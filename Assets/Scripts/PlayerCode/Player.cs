using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;

namespace Assets.Scripts.PlayerCode
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private PlayerType playerType;
        [SerializeField]
        private Collider collisionDetector;

        private int hp = 0;
        private int wins = 0;

        private void Awake()
        {
            SetPotCollisionTag();
        }

        private void SetPotCollisionTag()
        {
            switch (playerType)
            {
                case PlayerType.PlayerOne: collisionDetector.tag = "PlayerOnePot"; break;
                case PlayerType.PlayerTwo: collisionDetector.tag = "PlayerTwoPot"; break;
            }
        }

        public void TakeDamage(int amount)
        {
            hp -= amount;
        }

        public void AddWin()
        {
            GameManager.Instance.GetCanvasSystem.GetPlayerUI(playerType).SetWin(wins);
            wins++;
        }

        public void Init(int hp)
        {
            this.hp = hp;
        }

        void Die()
        {

        }
    }
}