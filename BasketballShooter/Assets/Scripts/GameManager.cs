using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    private int _selectedBallIndex; // ball index

    public int SelectedBallIndex { get { return _selectedBallIndex; } set { _selectedBallIndex = value; } }

    [SerializeField] private List<GameObject> _balls;    // ball prefabs

    public List<GameObject> Balls { get { return _balls; } }

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}