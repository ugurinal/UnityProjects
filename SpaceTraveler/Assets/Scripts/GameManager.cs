using System.Collections.Generic;
using UnityEngine;
using SpaceTraveler.SaveAndLoadSystem;

namespace SpaceTraveler.ManagerSystem
{
    #region DESCRIPTION

    /// <summary>
    /// This is the gamemanager class that almost control everything.
    /// This script take care of player pref (save), game current state, shop system etc.
    /// </summary>

    #endregion DESCRIPTION

    public class GameManager : MonoBehaviour
    {
        #region FIELDS

        private static GameManager _instance = null;
        public static GameManager Instance { get => _instance; }
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
            MakeSingleton();

            LoadPlayerData();

            //  IsPlayerAlive = true;
            //  IsPaused = false;
        }

        private void MakeSingleton()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public void SaveData()
        {
            SaveLoadManager.SavePlayer(this);
        }

        private void LoadPlayerData()
        {
            Debug.Log("Loading player data...");
            PlayerData data = SaveLoadManager.LoadPlayer();

            Coin = data.Coin;
            Diamond = data.Diamond;
            LevelReached = data.LevelReached;
            HighScores = data.HighScores;
            LevelStars = data.LevelStars;
            PurchasedShips = data.PurchasedShips;
            SelectedShip = data.SelectedShip;
            OneShotPU = data.OneShotPU;
            TwoShotPU = data.TwoShotPU;
            LifePU = data.LifePU;
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
                return Diamond >= price;
            }
            else
            {
                return Coin >= price;
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
            // if its not new level
            if (LevelReached >= levelToUnlock) return;

            LevelReached = levelToUnlock;
            LevelStars.Add(0);
            HighScores.Add(0);

            SaveData();
        }
    }
}