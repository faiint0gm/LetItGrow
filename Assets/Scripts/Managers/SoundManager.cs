using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Managers
{
    public enum SoundType
    {
        SFX,
        MUSIC
    }
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource sfxSource;
        [SerializeField]
        private AudioSource sfxSecondSource;
        [SerializeField]
        private AudioSource musicSource;
        [SerializeField]
        private AudioMixer audioMixer;

        public static SoundManager Instance = null;

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

        public void PlaySingle(AudioClip clip)
        {
            sfxSource.clip = clip;
            sfxSource.Play();
        }

        public void PlayOneShot(int source, AudioClip clip)
        {
            if(source == 0)
            {
                sfxSource.PlayOneShot(clip);
            }
            else
            {
                sfxSecondSource.PlayOneShot(clip);
            }
        }
        public void SetVolume(SoundType type, float value)
        {
            switch(type)
            {
                case SoundType.SFX: audioMixer.SetFloat("SFXVolume", value); break;
                case SoundType.MUSIC: audioMixer.SetFloat("MusicVolume", value); break;
            }
        }

        public void PauseMusic()
        {
            musicSource.Pause();
        }

        public void ReturnMusic()
        {
            musicSource.UnPause();
        }
    }
}
