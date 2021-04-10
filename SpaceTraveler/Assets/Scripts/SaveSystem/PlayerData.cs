using System.Collections.Generic;
using SpaceTraveler.ManagerSystem;

namespace SpaceTraveler.SaveAndLoadSystem
{
    [System.Serializable]
    public class PlayerData
    {
        public int Coin;
        public int Diamond;

        public int LevelReached;
        public List<int> HighScores;
        public List<int> LevelStars;

        public List<int> PurchasedShips;
        public int SelectedShip;

        public bool OneShotPU;
        public bool TwoShotPU;
        public bool LifePU;

        public PlayerData(GameManager gameManager)
        {
            Coin = gameManager.Coin;
            Diamond = gameManager.Diamond;
            LevelReached = gameManager.LevelReached;
            PurchasedShips = gameManager.PurchasedShips;
            SelectedShip = gameManager.SelectedShip;
            OneShotPU = gameManager.OneShotPU;
            TwoShotPU = gameManager.TwoShotPU;
            LifePU = gameManager.LifePU;
            LevelStars = gameManager.LevelStars;
            HighScores = gameManager.HighScores;
        }

        public PlayerData()
        {
            //  if there is no data
            //  this will be initial data
            List<int> highScores = new List<int>();
            highScores.Add(0);

            List<int> levelStars = new List<int>();
            levelStars.Add(0);

            List<int> purchasedShips = new List<int>();
            purchasedShips.Add(0);

            Coin = 500000;
            Diamond = 10;
            LevelReached = 1;
            HighScores = highScores;
            LevelStars = levelStars;
            PurchasedShips = purchasedShips;
            SelectedShip = 0;
            OneShotPU = false;
            TwoShotPU = false;
            LifePU = false;
        }
    }
}