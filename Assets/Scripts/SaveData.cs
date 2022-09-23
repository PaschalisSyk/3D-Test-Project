using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    static SaveData instance;

    [SerializeField] private ScoreData scoreData = new ScoreData();
    private List<Level> levels = new List<Level>();
    

    Scorekeeper scorekeeper;

    private void Awake()
    {
        ManageSingleton();
        scorekeeper = FindObjectOfType<Scorekeeper>();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Save()
    {
        scoreData.scoreValue = scorekeeper.GetTotalScore();
        scoreData.timeValue = scorekeeper.GetTime();
        scoreData.levels[scorekeeper.GetLevel() -1].itemsCollected = scorekeeper.GetObjects();

        SaveToJson();
    }

    public void SaveToJson()
    {
        string score = JsonUtility.ToJson(scoreData);
        File.WriteAllText(Application.dataPath + "/ScoreData.json", score);
    }
}

[System.Serializable]
public class ScoreData
{
    public int scoreValue;
    public float timeValue;
    public List<Level> levels;

}

[System.Serializable]
public class Level
{
    public string level;
    public int itemsCollected;
}



