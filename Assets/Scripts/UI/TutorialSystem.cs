using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Managers;

namespace Assets.Scripts.UI
{
    public class TutorialSystem : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private Sprite[] tutorialSteps;
        

        int lastImage = -1;
        public void RunTutorial()
        {
            GameManager.Instance.gameState = Enums.GameState.InTutorial;
            GameManager.Instance.GetCanvasSystem.GetMainMenu.HideMenu();
            image.sprite = tutorialSteps[0];
            lastImage = 0;
            gameObject.SetActive(true);
        }

        private void NextStep()
        {
            if(lastImage+1 <= tutorialSteps.Length -1)
            {
                image.sprite = tutorialSteps[lastImage + 1];
                lastImage++;
            } else
            {
                GameManager.Instance.PrepareFight();
                gameObject.SetActive(false);
            }
        }
        private void Update()
        {
            if(Input.GetButtonDown("XButton"))
            {
                GameManager.Instance.GetCanvasSystem.PlayButtonSound();
                NextStep();
            }
        }
    }
}
