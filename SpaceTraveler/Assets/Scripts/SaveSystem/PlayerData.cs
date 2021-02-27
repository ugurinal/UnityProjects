using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int coin;
    public int diamond;

    public int levelReached;
    public List<int> highScores;
    public List<int> levelStars;

    public List<int> purchasedShips;
    public int selectedShip;

    public bool oneShotPU;
    public bool twoShotPU;
    public bool lifePU;

    public PlayerData(GameManager gameManager)
    {
        coin = gameManager.Coin;
        diamond = gameManager.Diamond;
        levelReached = gameManager.LevelReached;
        purchasedShips = gameManager.PurchasedShips;
        selectedShip = gameManager.SelectedShip;
        oneShotPU = gameManager.OneShotPU;
        twoShotPU = gameManager.TwoShotPU;
        lifePU = gameManager.LifePU;
        levelStars = gameManager.LevelStars;
        highScores = gameManager.HighScores;
    }

    public PlayerData()
    {
        List<int> highScores = new List<int>();
        highScores.Add(0);

        List<int> levelStars = new List<int>();
        levelStars.Add(0);

        List<int> purchasedShips = new List<int>();
        purchasedShips.Add(0);

        coin = 5000;
        diamond = 10;
        levelReached = 1;
        this.highScores = highScores;
        this.levelStars = levelStars;
        this.purchasedShips = purchasedShips;
        selectedShip = 0;
        oneShotPU = false;
        twoShotPU = false;
        lifePU = false;
    }
}