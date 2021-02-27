using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace SpaceTraveler.AudioSystem
{
    #region Summary

    /// <summary>
    /// *********************************************************************************************
    //  *   This is the main class for saving sound settings to playerprefs                         *
    //  *   This script is attached to a gameobject in hierarchy that controls the sound settings   *
    //  *********************************************************************************************
    /// </summary>

    #endregion Summary

    public class AudioMixerController : MonoBehaviour

    {
        #region FIELDS

        [Header("Main Mixer")]
        [SerializeField] private AudioMixer _audioMixer = null;

        [Header("Sliders")]
        [SerializeField] private Slider _musicSlider = null;
        [SerializeField] private Slider _SFXSlider = null;

        #endregion FIELDS

        /// <summary>
        /// this function is called by music slider
        /// </summary>
        /// <param name="volume"></param>
        public void SetMusicVolume(float volume)
        {
            _audioMixer.SetFloat("MusicVolume", volume);
        }

        /// <summary>
        /// this function is called by sfx slider
        /// </summary>
        /// <param name="volume"></param>
        public void SetSFXVolume(float volume)
        {
            _audioMixer.SetFloat("SFXVolume", volume);
        }

        /// <summary>
        /// start function is called when object is created in the game
        /// below code will get the slider values from player prefs and sets the sliders
        /// </summary>
        private void Start()
        {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0);
            float SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0);

            _audioMixer.SetFloat("MusicVolume", musicVolume);
            _audioMixer.SetFloat("SFXVolume", SFXVolume);

            _musicSlider.value = musicVolume;
            _SFXSlider.value = SFXVolume;
        }

        /// <summary>
        /// this object is called when the object is destroyed or object became inactive
        /// below code will save the last volumes for sliders
        /// </summary>
        private void OnDisable()
        {
            float musicVolume = 0;
            float SFXVolume = 0;

            _audioMixer.GetFloat("MusicVolume", out musicVolume);
            _audioMixer.GetFloat("SFXVolume", out SFXVolume);

            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
        }
    }
}