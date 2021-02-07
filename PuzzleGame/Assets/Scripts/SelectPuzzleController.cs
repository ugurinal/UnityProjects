using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SelectPuzzleController : MonoBehaviour
{
    public void SelectPuzzle()
    {
        string[] name = EventSystem.current.currentSelectedGameObject.name.Split(' ');
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}