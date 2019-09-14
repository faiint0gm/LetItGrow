using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.PlayerCode;
using Assets.Scripts.Tap;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Managers
{
    public class FightManager : MonoBehaviour
    {
        [Header("Dripping Tap System")]
        [SerializeField]
        private int dewsPoolCapacity = 0;
        [SerializeField]
        private int dewsPerSecond = 0;
        [SerializeField]
        private int dewHPRecoveryAmount = 0;
        [SerializeField]
        private TapSystem tapSystem = null;
        [SerializeField]
        private int roundTime;
        [SerializeField]
        private int winLimit;

        public bool isDrawNotifying;
        private FinishType lastFinishType;

        private void Awake()
        {

        }
        public void SetupBattle()
        {
            GameManager.Instance.ActivatePlayers(true);
            RunCountDown();
        }

        public void StartBattle()
        {
            GameManager.Instance.GetPlayer(PlayerType.PlayerOne).Init(GameManager.Instance.PlayerHPAmount);
            GameManager.Instance.GetPlayer(PlayerType.PlayerTwo).Init(GameManager.Instance.PlayerHPAmount);
            GameManager.Instance.GetCanvasSystem.GetTimer.SetupTimer(roundTime, true);
            tapSystem.SetupTap(dewsPerSecond, dewsPoolCapacity);
            tapSystem.StartDispenseDews();
        }

        void RunCountDown()
        {
            GameManager.Instance.GetCanvasSystem.InfoTextsSystem.RunRoundCountdown();
        }

        public void NotifyPlayerDie(PlayerType type)
        {
            switch (type)
            {
                case PlayerType.PlayerOne: {
                        GameManager.Instance.GetPlayer(PlayerType.PlayerTwo).AddWin();
                        lastFinishType = FinishType.PlayerTwoWin;
                        StartCoroutine(GameManager.Instance.GetCanvasSystem.InfoTextsSystem.ShowFinishText(FinishType.PlayerTwoWin));
                        break;
                    }
                case PlayerType.PlayerTwo:
                    {
                        GameManager.Instance.GetPlayer(PlayerType.PlayerOne).AddWin();
                        lastFinishType = FinishType.PlayerOneWin;
                        StartCoroutine(GameManager.Instance.GetCanvasSystem.InfoTextsSystem.ShowFinishText(FinishType.PlayerOneWin));
                        break;
                    }
            }
        }

        public void NotifyTimeOut()
        {
            lastFinishType = FinishType.TimeOut;
            StartCoroutine(GameManager.Instance.GetCanvasSystem.InfoTextsSystem.ShowFinishText(FinishType.TimeOut));
        }

        public void NotifyPlayerWin(PlayerType type)
        {
            FinishType finish = type == PlayerType.PlayerOne ? FinishType.PlayerOneWin : FinishType.PlayerTwoWin;
            lastFinishType = finish;
            StartCoroutine(GameManager.Instance.GetCanvasSystem.InfoTextsSystem.ShowFinishText(finish));
        }

        public void NotifyDraw()
        {
            if(!isDrawNotifying)
            {
                isDrawNotifying = true;
                lastFinishType = FinishType.Draw;
                StartCoroutine(GameManager.Instance.GetCanvasSystem.InfoTextsSystem.ShowFinishText(FinishType.Draw));
            }
            else
            {
                return;
            }
        }

        public void HandleRoundFinish(bool isGameOver)
        {
            if(isGameOver)
            {
                GameManager.Instance.ActivatePlayers(false);
                GameManager.Instance.GetCanvasSystem.GetMainMenu.ShowMenu();
            }
        }

        public int DewHPRecoveryAmount { get { return dewHPRecoveryAmount; } }
        public TapSystem GetTap { get { return tapSystem; } }
        public int WinsLimit { get { return winLimit; } }
    }
}
