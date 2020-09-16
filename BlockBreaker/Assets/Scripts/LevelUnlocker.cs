using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlocker : MonoBehaviour
{

    public void UnlockAllLevels()
    {

        for (int i = 1; i <= 35; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), 1);
        }
    }
}
