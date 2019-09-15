using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.UI
{
    public class PauseMenu : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetButtonDown("TriangleButton"))
            {
                GameManager.Instance.GetCanvasSystem.InfoTextsSystem.StopAll();
                GameManager.Instance.GetFightManager.HandleRoundFinish(true);
                Hide(true);
            }
            if(Input.GetButtonDown("XButton"))
            {
                Hide(false);
            }

        }

        public void Show()
        {
            GameManager.Instance.gameState = Enums.GameState.InPause;
            GameManager.Instance.GetSoundManager.PauseMusic();
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }
        public void Hide(bool toMenu)
        {
            if(toMenu)
            {
                GameManager.Instance.gameState = Enums.GameState.InMenu;
            }
            else
            {
                GameManager.Instance.gameState = Enums.GameState.InBattle;
            }
            GameManager.Instance.GetSoundManager.ReturnMusic();
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}
