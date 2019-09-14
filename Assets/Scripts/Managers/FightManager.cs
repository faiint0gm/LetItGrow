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
        private void Awake()
        {
            SetupBattle();
        }
        public void SetupBattle()
        {
            GameManager.Instance.GetPlayer(PlayerType.PlayerOne).Init(GameManager.Instance.PlayerHPAmount);
            GameManager.Instance.GetPlayer(PlayerType.PlayerTwo).Init(GameManager.Instance.PlayerHPAmount);
        }

        public void StartBattle()
        {
            GameManager.Instance.GetCanvasSystem.GetTimer.SetupTimer(roundTime, true);
            tapSystem.SetupTap(dewsPerSecond, dewsPoolCapacity);
            tapSystem.StartDispenseDews();
        }

        public int DewHPRecoveryAmount { get { return dewHPRecoveryAmount; } }
        public TapSystem GetTap { get { return tapSystem; } }
    }
}
