using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    public Button button;

    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI progressText;

    Scorekeeper scorekeeper;

    void Start()
    {
        scorekeeper = FindObjectOfType<Scorekeeper>();
        slider.value = 0;

        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(Restart);
    }
    void Restart()
    {
        scorekeeper.RestartGame();
    }

    private void Update()
    {
        Slidder();
        ScoreText();
    }

    void Slidder()
    {
        slider.value = Mathf.Lerp(slider.value, (slider.value += scorekeeper.GetScore()) / 100, 3f * Time.deltaTime);
    }
    void ScoreText()
    {
        var score = scorekeeper.GetScore();
        scoreText.text = "Score: " + scorekeeper.GetTotalScore();
        if(score >= 100)
        {
            score = 100;
        }

        progressText.text = score + "%" ;
    }
}
