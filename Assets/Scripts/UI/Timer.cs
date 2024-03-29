﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Assets.Scripts.Managers;

namespace Assets.Scripts.UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI timerTM;
        [SerializeField]
        private bool showOnlySeconds;

        private int timeLeft;

        public void SetupTimer(int seconds, bool autoStart)
        {
            timeLeft = seconds;
            SetTime(timeLeft);
            if (autoStart)
            {
                StartCountdown();
            }
        }

        void SetTime(int seconds)
        {
            TimeSpan timespan = TimeSpan.FromSeconds(seconds);
            DateTime dt;
            dt = Convert.ToDateTime(timespan.ToString());
            if (!showOnlySeconds)
            {
                timerTM.text = dt.ToString("mm:ss");
            }
            else
            {
                timerTM.text = seconds.ToString();
            }
        }

        public void StartCountdown()
        {
            gameObject.SetActive(true);
            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            while (timeLeft > 0)
            {
                SetTime(timeLeft);
                timeLeft--;
                yield return new WaitForSeconds(1);
            }
            if (timeLeft <= 0)
            {
                SetTime(0);
                GameManager.Instance.GetFightManager.NotifyTimeOut();
                yield break;
            }
        }

        public void StopTimer()
        {
            timerTM.gameObject.SetActive(false);
        }
        public int GetTimeLeft { get { return timeLeft; } }
    }
}