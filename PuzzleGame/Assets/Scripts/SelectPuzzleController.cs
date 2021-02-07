using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SelectPuzzleController : MonoBehaviour
{
    public void SelectPuzzle()
    {
        string[] name = EventSystem.current.currentSelectedGameObject.name.Split();

        GameManager instance = GameManager.Instance;
        if (instance != null)
        {
            instance.SelectPuzzleIndex(int.Parse(name[1]));
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}