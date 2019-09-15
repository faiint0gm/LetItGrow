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
        private Vector3 interruptMove;

        [SerializeField]
        private float xMinValue;
        [SerializeField]
        private float xMaxValue;
        [SerializeField]
        private float yMinValue;
        [SerializeField]
        private float yMaxValue;

        private int hp = 0;
        private int wins = 0;
        private PlayerType opponentType;

        private Vector3 startPosition;

        private bool interrupt;

        private void Awake()
        {
            SetSides();
            startPosition = hold.transform.localPosition;
        }

        private void SetSides()
        {
            switch (playerType)
            {
                case PlayerType.PlayerOne: collisionDetector.tag = "PlayerOnePot";
                    opponentType = PlayerType.PlayerTwo;
                    break;
                case PlayerType.PlayerTwo: collisionDetector.tag = "PlayerTwoPot";
                    opponentType = PlayerType.PlayerOne;
                    break;
            }
        }

        public void TakeDamage(int amount)
        {
            hp -= amount;
            if(hp<= 0)
            {
                hp = 0;
            }
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
            GameManager.Instance.GetCanvasSystem.SetupPlayerHP(playerType, hp);
        }

        void Die()
        {
            StartCoroutine(Dying());
        }

        IEnumerator Dying()
        {
            yield return new WaitForSeconds(0.1f);
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
            if(interrupt)
            {
                return;
            }
            Vector3 movement = Vector3.zero;
            Vector3 leftThumb = new Vector3
            {
                x = Input.GetAxis("LeftThumbX"),
                y = -Input.GetAxis("LeftThumbY"),
                z = 0
            };

            Vector3 rightThumb = new Vector3
            {
            x = Input.GetAxis("RightThumbX"),
            y = -Input.GetAxis("RightThumbY"),
            z = 0
            };

            switch (playerType)
            {
                case PlayerType.PlayerOne:
                    {
                        if ((leftThumb.x < 0 && LockNegativeX() && leftThumb.y < 0 && LockNegativeY()) ||
                            (leftThumb.x > 0 && LockPositiveX() && leftThumb.y > 0 && LockPositiveY()) ||
                            (leftThumb.x < 0 && LockNegativeX() && leftThumb.y > 0 && LockPositiveY()) ||
                            (leftThumb.x > 0 && LockPositiveX() && leftThumb.y < 0 && LockNegativeY()))
                        {
                            movement = Vector3.zero;
                        }
                        else if ((leftThumb.x < 0 && LockNegativeX()) ||
                            (leftThumb.x > 0 && LockPositiveX()))
                        {
                            movement = new Vector3
                            {
                                x = 0,
                                y = leftThumb.y,
                                z = 0
                            };
                        }
                        else if ((leftThumb.y < 0 && LockNegativeY()) ||
                            (leftThumb.y > 0 && LockPositiveY()))
                        {
                            movement = new Vector3
                            {
                                x = leftThumb.x,
                                y = 0,
                                z = 0
                            };
                        }
                        else
                            movement = new Vector3
                            {
                                x = leftThumb.x,
                                y = leftThumb.y,
                                z = 0
                            };
                        break;
                    }
                case PlayerType.PlayerTwo:
                    {
                        if ((rightThumb.x < 0 && LockNegativeX() && rightThumb.y < 0 && LockNegativeY()) ||
                            (rightThumb.x > 0 && LockPositiveX() && rightThumb.y > 0 && LockPositiveY()) ||
                            (rightThumb.x < 0 && LockNegativeX() && rightThumb.y > 0 && LockPositiveY()) ||
                            (rightThumb.x > 0 && LockPositiveX() && rightThumb.y < 0 && LockNegativeY()))
                        {
                            movement = Vector3.zero;
                        }
                        else if ((rightThumb.x < 0 && LockNegativeX()) ||
                            (rightThumb.x > 0 && LockPositiveX()))
                        {
                            movement = new Vector3
                            {
                                x = 0,
                                y = rightThumb.y,
                                z = 0
                            };
                        }
                        else if ((rightThumb.y < 0 && LockNegativeY()) ||
                            (rightThumb.y > 0 && LockPositiveY()))
                        {
                            movement = new Vector3
                            {
                                x = rightThumb.x,
                                y = 0,
                                z = 0
                            };
                        }
                        else
                        movement = new Vector3
                            {
                                x = rightThumb.x,
                                y = rightThumb.y,
                                z = 0
                            };
                        break;
                    }
            }
            hold.transform.localPosition += movement;
        }

        private void Update()
        {
            DebugPad();
            Movement();
        }

        bool LockNegativeX()
        {
            if (hold.transform.localPosition.x <= startPosition.x + xMinValue)
                return true;
            else
                return false;
        }

        bool LockPositiveX()
        {
            if (hold.transform.localPosition.x >= startPosition.x + xMaxValue)
                return true;
            else
                return false;
        }

        bool LockNegativeY()
        {
            if (hold.transform.localPosition.y <= startPosition.y + yMinValue)
                return true;
            else
                return false;
        }

        bool LockPositiveY()
        {
            if (hold.transform.localPosition.y >= startPosition.y + yMaxValue)
                return true;
            else
                return false;
        }

        public void SetInterruptInput(bool isInterrupting)
        {
            interrupt = isInterrupting;
        }

        public void GetHit()
        {
            hold.transform.localPosition += interruptMove;
        }
        private void DebugPad()
        {
            if (Input.GetAxis("RightThumbX") != 0)
            {
                Debug.Log("Right Thumb X : " + Input.GetAxis("RightThumbX"));
            }
            if (Input.GetAxis("RightThumbY") != 0)
            {
                Debug.Log("Right Thumb Y: " + Input.GetAxis("RightThumbY"));
            }
            if (Input.GetAxis("LeftThumbX") != 0)
            {
                Debug.Log("Left Thumb X: " + Input.GetAxis("LeftThumbX"));
            }
            if (Input.GetAxis("LeftThumbY") != 0)
            {
                Debug.Log("Left Thumb Y: " + Input.GetAxis("LeftThumbY"));
            }
        }
    }
}