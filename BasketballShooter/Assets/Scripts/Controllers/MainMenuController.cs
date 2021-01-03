using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private Animator mainButtonHolder;
    [SerializeField] private Animator ballSelectPanel;

    [Header("Buttons")]
    [Space(20)]
    [SerializeField] private Button ballsButton;
    [SerializeField] private Button backButton;

    private void Awake()
    {
        ballsButton.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();

        ballsButton.onClick.AddListener(() => ShowBallSelect());
        backButton.onClick.AddListener(() => ShowMainButtons());
    }

    private void ShowBallSelect()
    {
        mainButtonHolder.SetTrigger("FadeOut");
        ballSelectPanel.SetTrigger("FadeIn");
    }

    private void ShowMainButtons()
    {
        ballSelectPanel.SetTrigger("FadeOut");
        mainButtonHolder.SetTrigger("FadeIn");
    }
}