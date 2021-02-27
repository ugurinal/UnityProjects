using UnityEngine;

namespace SpaceTraveler.AudioSystem
{
    public class SoundController : MonoBehaviour
    {
        public static SoundController instance;

        public AudioSource musicPlayer;
        public AudioSource sfxPlayer;

        public AudioClip[] menuMusics;
        public AudioClip[] levelMusics;
        public AudioClip[] SFXs;

        private void Awake()
        {
            MakeSingleton();
        }

        private void MakeSingleton()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            musicPlayer.clip = menuMusics[0];
            musicPlayer.Play();
        }

        public void PlayMusic(string name)
        {
            musicPlayer.Stop();

            if (name == "MainMenu")
            {
                musicPlayer.clip = menuMusics[0];
            }
            else if (name == "LevelMenu")
            {
                musicPlayer.clip = menuMusics[1];
            }
            else
            {
                musicPlayer.clip = levelMusics[0];
            }

            musicPlayer.Play();
        }

        /// <summary>
        /// name is not used since there is only one sfx for now
        /// </summary>
        /// <param name="name">Name of the SFX to play</param>
        public void PlaySFX(string name)
        {
            sfxPlayer.Stop();
            sfxPlayer.clip = SFXs[0];

            if (!sfxPlayer.isPlaying)
            {
                sfxPlayer.Play();
            }
        }
    }
}