using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Scorekeeper : MonoBehaviour
{
    [SerializeField] int levelScore;
    [SerializeField] int totalScore = 0;
    [SerializeField] int level;
    [SerializeField] float time;
    [SerializeField] int collectedObj;
    [SerializeField] GameObject gameOverPanel;

    static Scorekeeper instance;
    SaveData save;
    Player player;


    private void Awake()
    {
        gameOverPanel.SetActive(false);
        ManageSingleton();
        level = SceneManager.GetActiveScene().buildIndex + 1;
        save = FindObjectOfType<SaveData>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        Timer();
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

    public int GetScore()
    {
        return levelScore;
    }

    public void ModifyScore(int value)
    {
        int amount = value + (level * 3);
        totalScore += amount;
        levelScore += amount;
        Mathf.Clamp(levelScore, 0, int.MaxValue);
        if(levelScore < 0)
        {
            levelScore = 0;
        }
        if(levelScore >= 100)
        {
            if (level <= 3)
            {
                StartCoroutine(NextLevel());
            }
            else
            {
                Time.timeScale = 0;
                print("WIN");
                Save();
            }
            level++;


        }
        print(levelScore);
    }

    public void ResetScore()
    {
        levelScore = 0;
    }

    void Timer()
    {
        time += Time.deltaTime;
        time = (float)Math.Round(time, 2);
    }
    public void ObjectCollection()
    {
        collectedObj++;
    }

    public float GetTime()
    {
        return time;
    }

    public void Save()
    {  
        save.Save();
        if(player._lose)
        {
            gameOverPanel.SetActive(true);
        }
    }
    public int GetObjects()
    {
        return collectedObj;
    }
    public int GetLevel()
    {
        return level;
    }

    IEnumerator NextLevel()
    {
        Save();

        yield return new WaitForSeconds(0.5f);
        ResetScore();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public int GetTotalScore()
    {
        return totalScore;
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
    }
    public void ResetTotalScore()
    {
        totalScore = 0;
    }
}
