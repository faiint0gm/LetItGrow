using System.Collections;
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
        [SerializeField]
        private SoundManager soundManager;

        [HideInInspector]
        public GameState gameState;

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
            Cursor.visible = false;
            canvasSystem.GetMainMenu.ShowMenu();

        }

        private void Update()
        {
            if(gameState == GameState.InMenu)
            {
                if(Input.GetButtonDown("TriangleButton"))
                {
                    canvasSystem.GetMainMenu.RunAway();
                }
            }
            else if (gameState == GameState.InBattle)
            {
                if(Input.GetButtonDown("TriangleButton"))
                {
                    canvasSystem.GetPauseMenu.Show();
                }
            }
            else if (gameState == GameState.InTutorial)
            {
                if(Input.GetButtonDown("TriangleButton"))
                {
                    canvasSystem.Tutorial.gameObject.SetActive(false);
                    canvasSystem.GetMainMenu.ShowMenu();
                }
            }
        }

        public void PrepareFight()
        {
            gameState = GameState.InBattle;
            canvasSystem.GetMainMenu.HideMenu();
            fightManager.SetupBattle();
        }

        public int DewHPRecoveryAmount { get { return fightManager.DewHPRecoveryAmount; } }
        public int PlayerHPAmount { get { return playerHPAmount; } }
        public CanvasSystem GetCanvasSystem { get { return canvasSystem; } }
        public FightManager GetFightManager { get { return fightManager; } }
        public SoundManager GetSoundManager { get { return soundManager; } }

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