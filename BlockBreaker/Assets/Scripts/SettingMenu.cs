using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{

    Toggle musicToggle;

    void Start()
    {
        musicToggle = GameObject.Find("MusicToggle").GetComponent<Toggle>();

        musicToggle.onValueChanged.AddListener(musicStatusChanged);

        int isMusicOn = PlayerPrefs.GetInt("isMusicOn");

        if (isMusicOn == 0)
        {
            musicToggle.isOn = false;
        }
        else if (isMusicOn == 1)
        {
            musicToggle.isOn = true;

        }
    }

    public void musicStatusChanged(bool isChanged)
    {

        AudioSource mainAudio = GameObject.Find("AudioManager").GetComponent<AudioSource>();

        if (isChanged)
        {
            PlayerPrefs.SetInt("isMusicOn", 1);
            mainAudio.Play();

        }
        else
        {
            PlayerPrefs.SetInt("isMusicOn", 0);
            mainAudio.Stop();

        }
    }
}
