using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int bestScore = 0;
    public string playerName = "Player";
   // public List<string> highScores = new List<string>();

    [System.Serializable]
    public class SaveData
    {
        public string PlayerName;
        public int BestScore;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadNameAndScore();
        } 
    }
    public void SaveNameAndScore()
    {
        SaveData data = new SaveData();
        data.BestScore = bestScore;
        data.PlayerName = playerName;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadNameAndScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            playerName = data.PlayerName;
            bestScore = data.BestScore;
        }
        else
        {
            //Default values if no saved data
            SetDefaultValues();
        }
    }
    
    public void SetDefaultValues()
    {
        playerName = "Player";
        bestScore = 0;
    }
}
