using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace SpaceTraveler.SaveAndLoadSystem
{
    public static class SaveLoadManager
    {
        // we may split save player functions like save coin, save ship
        // and load them separetely
        public static void SavePlayer(GameManager gameManager)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

            PlayerData data = new PlayerData(gameManager);

            bf.Serialize(stream, data);
            stream.Close();
        }

        public static PlayerData LoadPlayer()
        {
            if (File.Exists(Application.persistentDataPath + "/player.sav"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

                PlayerData data = (PlayerData)bf.Deserialize(stream);

                Debug.Log("Data found");
                return data;
            }
            else
            {
                Debug.LogWarning("Data Not found!");
                Debug.LogWarning("New data will be created!");

                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

                PlayerData data = new PlayerData();

                bf.Serialize(stream, data);
                stream.Close();

                return data;
            }
        }
    }
}