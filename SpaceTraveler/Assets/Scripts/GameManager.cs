using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region DESCRIPTION

    //  *********************************************************************************************
    //  * This is the gamemanager class that almost control everything.                             *
    //  * This script take care of player pref (save), game current state, shop system etc.         *
    //  *********************************************************************************************

    #endregion DESCRIPTION

    #region FIELDS

    public static GameManager Instance;
    public string PlayerName { get; set; } = null;
    public int Coin { get; set; } = 5000;
    public int Diamond { get; set; } = 10;
    public int LevelReached { get; set; } = 0;
    public int SelectedShip { get; set; } = 0;
    public List<int> PurchasedShips { get; set; } = new List<int>();
    public List<int> HighScores { get; set; } = new List<int>();
    public List<int> LevelStars { get; set; } = new List<int>();
    public bool OneShotPU { get; set; } = false;
    public bool TwoShotPU { get; set; } = false;
    public bool LifePU { get; set; } = false;
    public bool IsPlayerAlive { get; set; } = true;
    public bool IsPaused { get; set; } = false;

    #endregion FIELDS

    private void Awake()
    {
        SetSingleton();

        LoadPlayerData();

        IsPlayerAlive = true;
        IsPaused = false;
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SaveData()
    {
        SaveLoadManager.SavePlayer(this);
    }

    private void LoadPlayerData()
    {
        Debug.Log("Loading player data...");
        PlayerData data = SaveLoadManager.LoadPlayer();

        Coin = data.coin;
        Diamond = data.diamond;
        LevelReached = data.levelReached;
        HighScores = data.highScores;
        LevelStars = data.levelStars;
        PurchasedShips = data.purchasedShips;
        SelectedShip = data.selectedShip;
        OneShotPU = data.oneShotPU;
        TwoShotPU = data.twoShotPU;
        LifePU = data.lifePU;
    }

    public void IncreaseCoin(int coin)
    {
        Coin += coin;
        SaveData();
    }

    public void IncreaseDiamond(int diamond)
    {
        Diamond += diamond;
        SaveData();
    }

    public bool CanEffort(int price, bool isDiamond)
    {
        if (isDiamond)
        {
            if (Diamond >= price)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (Coin >= price)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void PurchaseShip(int index, int price, bool isDiamond)
    {
        if (isDiamond)
        {
            IncreaseDiamond(-price);
        }
        else
        {
            IncreaseCoin(-price);
        }

        PurchasedShips.Add(index);

        SaveData();
    }

    public void SelectShip(int index)
    {
        SelectedShip = index;
        SaveData();
    }

    public void IncreaseLevel(int levelToUnlock)
    {
        if (LevelReached >= levelToUnlock) return;

        LevelReached = levelToUnlock;
        LevelStars.Add(0);
        HighScores.Add(0);

        SaveData();
    }
}