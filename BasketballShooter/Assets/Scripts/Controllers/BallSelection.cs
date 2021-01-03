using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSelection : MonoBehaviour
{
    [SerializeField] private List<Button> balls = new List<Button>();

    private void Awake()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].onClick.RemoveAllListeners();
            balls[i].onClick.AddListener(() => SelectBall());
        }
    }

    private void SelectBall()
    {
        int index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        Debug.Log("Selected Ball is " + index);
    }
}