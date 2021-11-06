using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // File Input/Out

public class PlayerInfoController : MonoBehaviour
{
    public static PlayerInfoController Instance;
    public string playerName;
    public int highScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadName();
        if (playerName == null)
        {
            playerName = "AAA"; // Initially there's no player's name data
        }
        LoadHighScore();
        Debug.Log(playerName);
        Debug.Log(highScore);
    }
    #region Save Player's Name
    [System.Serializable]
    class SavePlayerNameData
    {
        public string SD_playerName;
    }

    public void SaveName()
    {
        SavePlayerNameData data = new SavePlayerNameData();
        data.SD_playerName = playerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savenamefile.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savenamefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SavePlayerNameData data = JsonUtility.FromJson<SavePlayerNameData>(json); // Convert string json to SaveData type info, then assign it to the SaveData class's instance called data

            playerName = data.SD_playerName;
        }
    }
    #endregion

    #region Save High Score
    [System.Serializable]
    class SaveHighScoreData
    {
        public int SD_highScore;
    }

    public void SaveHighScore()
    {
        SaveHighScoreData data = new SaveHighScoreData();
        data.SD_highScore = highScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savescorefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savescorefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveHighScoreData data = JsonUtility.FromJson<SaveHighScoreData>(json);
            highScore = data.SD_highScore;
        }
    }
    #endregion
}
