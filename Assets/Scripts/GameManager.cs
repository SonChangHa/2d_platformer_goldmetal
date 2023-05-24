using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //ΩÃ±€≈Ê
    public static GameManager instance = null;
    GameObject canvas;
    TextMeshProUGUI ScoreText;
    TextMeshProUGUI StageText;
    GameObject GameOverText;

    int HP = 3;

    private void Awake()
    {
        StartUI();

        //ΩÃ±€≈Ê ±∏«ˆ
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    void StartUI()
    {
        //¿œ¥‹ æ¿¿Ãµø∞°¥…«œ∞‘ º≥¡§
        canvas = GameObject.Find("Canvas").gameObject;

        ScoreText = canvas.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        StageText = canvas.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        GameOverText = canvas.transform.GetChild(2).gameObject;

        this.ScoreText.text = "Score: " + score.ToString();
        this.StageText.text = "Stage " + stage.ToString();
    }

    public int score = 0;
    public int stage = 1;

    public void GetCoin(int value)
    {
        this.score += value;
        this.ScoreText.text = "Score: " + score.ToString();
    }

    public void StageMoved()
    {
        SceneManager.LoadScene($"Stage{stage + 1}");
        this.stage += 1;
        this.StageText.text = "Stage " + stage.ToString();
    }

    public void GameOver()
    {
        GameOverText.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene($"Stage{stage}");
        GameOverText.SetActive(false);
    }

    public void GetDamage()
    {
        HP -= 1;
        //hp ¿ÃπÃ¡ˆ
    }
}
