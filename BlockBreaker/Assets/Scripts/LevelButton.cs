using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelButton : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public int unLocked;
    public GameObject star0;
    public GameObject star1;
    public GameObject star2;
    public GameObject starBack0;
    public GameObject starBack1;
    public GameObject starBack2;
    public GameObject lockObj;

    public void LoadLevel()
    {
        if (unLocked == 1)
        {
            SceneManager.LoadScene(levelText.text);
        }
    }
}