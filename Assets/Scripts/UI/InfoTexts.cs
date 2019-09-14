using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Enums;
using TMPro;

namespace Assets.Scripts.UI
{
    public class InfoTexts : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI counterText;
        [SerializeField]
        TextMeshProUGUI fightText;
        [SerializeField]
        TextMeshProUGUI finalText;

        public void RunRoundCountdown()
        {
            StartCoroutine(StartCountdown());
        }

        IEnumerator StartCountdown()
        {
            int number = 3;
            while (number > 0)
            {
                counterText.text = number.ToString();
                counterText.gameObject.SetActive(true);
                number--;
                yield return new WaitForSeconds(1);
                counterText.gameObject.SetActive(false);
            }
            if (number == 0)
            {
                fightText.text = "FIGHT!";
                fightText.gameObject.SetActive(true);
                GameManager.Instance.GetFightManager.StartBattle();
                yield return new WaitForSeconds(1);
                fightText.gameObject.SetActive(false);
                yield break;
            }
        }

        public IEnumerator ShowFinishText(FinishType finishType, bool isGameOver = false)
        {
            switch (finishType)
            {
                case FinishType.PlayerOneWin: finalText.text = "Player One WINS"; break;
                case FinishType.PlayerTwoWin: finalText.text = "Player Two WINS"; break;
                case FinishType.Draw:
                    finalText.text = "DRAW!";
                    GameManager.Instance.GetFightManager.isDrawNotifying = false;
                    break;
                case FinishType.TimeOut: finalText.text = "TIME OUT!"; break;
            }
            finalText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            finalText.gameObject.SetActive(false);
            GameManager.Instance.GetFightManager.HandleRoundFinish(isGameOver);
        }
    }
}
