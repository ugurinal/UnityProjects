using UnityEngine;

namespace SpaceTraveler.AudioSystem
{
    public class SoundController : MonoBehaviour
    {
        public static SoundController instance;

        [SerializeField] public AudioSource musicPlayer;
        [SerializeField] public AudioSource sfxPlayer;

        [SerializeField] public AudioClip[] menuMusics;
        [SerializeField] public AudioClip[] levelMusics;
        [SerializeField] public AudioClip[] SFXs;

        private void Awake()
        {
            SetSingleton();
        }

        private void SetSingleton()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            musicPlayer.clip = menuMusics[0];
            musicPlayer.Play();
        }

        // Update is called once per frame
        private void Update()
        {
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