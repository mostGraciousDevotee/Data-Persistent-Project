using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public string CurrentUser;
    public string BestUser;
    public int BestScore;
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load serialized data here
        LoadScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string UserName;
        public int HighScore;
    }

    public void UpdateScore(int bestScore)
    {
        if (bestScore > BestScore)
        {
            BestUser = CurrentUser;
            BestScore = bestScore;

            SaveScore();
        }
    }
    
    public void ResetScore()
    {
        BestUser = "<Empty>";
        BestScore = 0;

        SaveScore();
    }
    
    void SaveScore()
    {
        SaveData data = new SaveData();
        data.UserName = BestUser;
        data.HighScore = BestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestUser = data.UserName;
            BestScore = data.HighScore;
        }
    }
}
