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
        [SerializeField]
        private GameObject hold;
        [SerializeField]
        private GameObject vine;

        private int hp = 0;
        private int wins = 0;
        private PlayerType opponentType;
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
            GameManager.Instance.GetCanvasSystem.SetupPlayerHP(playerType, hp);
            if(hp <= 0)
            {
                Die();
            }
        }

        public void GetHP(int amount)
        {
            hp += amount;
            if(hp>=100)
            {
                hp = 100;
            }
            GameManager.Instance.GetCanvasSystem.SetupPlayerHP(playerType, hp);
        }

        public void AddWin()
        {
            GameManager.Instance.GetCanvasSystem.GetPlayerUI(playerType).SetWin(wins);
            wins++;
            if(wins == GameManager.Instance.GetFightManager.WinsLimit)
            {
                Win();
            }
        }

        public void Init(int hp)
        {
            this.hp = hp;
            vine.SetActive(true);
        }

        void Die()
        {
            if (hp == 0 && GameManager.Instance.GetPlayer(opponentType).hp == 0)
            {
                GameManager.Instance.GetFightManager.NotifyDraw();
            }
            else
            {
                GameManager.Instance.GetFightManager.NotifyPlayerDie(playerType);
            }
        }

        void Win()
        {
            GameManager.Instance.GetFightManager.NotifyPlayerWin(playerType);
        }

        void Movement()
        {
            switch (playerType)
            {
                case PlayerType.PlayerOne:
                    {
                        hold.transform.localPosition += new Vector3
                        {
                            x = Input.GetAxis("LeftThumbX"),
                            y = Input.GetAxis("LeftThumbY"),
                            z = 0
                        };
                        break;
                    }
                case PlayerType.PlayerTwo:
                    {
                        hold.transform.localPosition += new Vector3
                        {
                            x = Input.GetAxis("RightThumbX"),
                            y = Input.GetAxis("RightThumbY"),
                            z = 0
                        };
                        break;
                    }
            }
        }

        public void SetVineActive(bool isActive)
        {
            vine.SetActive(isActive);
        }

        private void Update()
        {
            //DebugPad();
            Movement();
        }

        private void DebugPad()
        {
            if (Input.GetAxis("RightThumbX") != 0)
            {
                Debug.Log("Right Thumb X");
            }
            if (Input.GetAxis("RightThumbY") != 0)
            {
                Debug.Log("Right Thumb Y");
            }
            if (Input.GetAxis("LeftThumbX") != 0)
            {
                Debug.Log("Left Thumb X");
            }
            if (Input.GetAxis("LeftThumbY") != 0)
            {
                Debug.Log("Left Thumb Y");
            }
        }
    }
}