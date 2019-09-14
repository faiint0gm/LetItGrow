﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Tap;
using Assets.Scripts.UI;
using Assets.Scripts.Enums;
using Assets.Scripts.PlayerCode;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {

        [Header("Players Configuration")]
        [SerializeField]
        private int playerHPAmount;
        [SerializeField]
        private Player playerOne;
        [SerializeField]
        private Player playerTwo;

        [Header("Game Management")]
        [SerializeField]
        private CanvasSystem canvasSystem;
        [SerializeField]
        private FightManager fightManager;
        

        public static GameManager Instance = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            canvasSystem.GetMainMenu.ShowMenu();
        }

        public void PrepareFight()
        {
            canvasSystem.GetMainMenu.HideMenu();
            fightManager.SetupBattle();
        }

        public int DewHPRecoveryAmount { get { return fightManager.DewHPRecoveryAmount; } }
        public int PlayerHPAmount { get { return playerHPAmount; } }
        public CanvasSystem GetCanvasSystem { get { return canvasSystem; } }
        public FightManager GetFightManager { get { return fightManager; } }
        public Player GetPlayer(PlayerType type)
        {
            switch(type)
            {
                case PlayerType.PlayerOne: return playerOne;
                case PlayerType.PlayerTwo: return playerTwo;
                default: return null;
            }
        }

        public void ActivatePlayers(bool isActive)
        {
            playerOne.gameObject.SetActive(isActive);
            playerTwo.gameObject.SetActive(isActive);
        }
    }
}