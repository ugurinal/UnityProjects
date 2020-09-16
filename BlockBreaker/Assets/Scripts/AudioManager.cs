using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    void Awake()
    {
        int audioManagerCount = FindObjectsOfType<AudioManager>().Length;
        if (audioManagerCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    void Start()
    {

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (PlayerPrefs.HasKey("isMusicOn"))
        {
            int isMusicOn = PlayerPrefs.GetInt("isMusicOn");

            if (isMusicOn == 0)
            {
                GetComponent<AudioSource>().Stop();
            }
            else if (isMusicOn == 1)
            {

                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            PlayerPrefs.SetInt("isMusicOn", 1);
            GetComponent<AudioSource>().Play();
        }


    }
}
