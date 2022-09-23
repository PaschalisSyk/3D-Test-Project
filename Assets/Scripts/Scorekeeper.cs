using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Scorekeeper : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] int startingScore = 0;
    [SerializeField] int level;
    [SerializeField] float time;
    [SerializeField] int collectedObj;

    static Scorekeeper instance;
    SaveData save;


    private void Awake()
    {
        ManageSingleton();
        level = SceneManager.GetActiveScene().buildIndex + 1;
        save = FindObjectOfType<SaveData>();
        startingScore += score;
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
        return score;
    }

    public void ModifyScore(int value)
    {
        score += value + value*(level/2);
        Mathf.Clamp(score, 0, int.MaxValue);
        if(score < 0)
        {
            score = 0;
        }
        if(score >= 100)
        {
            StartCoroutine(NextLevel());

            level++;
            if (level > 4)
            {
                print("WIN");
                Save();
                Time.timeScale = 0;
            }

        }
        print(score);
    }

    public void ResetScore()
    {
        score = 0;
        startingScore = startingScore + 100;
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
        return startingScore;
    }
}
