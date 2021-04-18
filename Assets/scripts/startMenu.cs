using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startMenu : MonoBehaviour
{
    public Text highScoreText;
    float highScore;

    void Start()
    {
        if(PlayerPrefs.HasKey("High Score")){
            highScore = PlayerPrefs.GetFloat("High Score");
            highScoreText.text = highScore.ToString();
        } else 
        {
            highScoreText.text = "0";
        }
    }

    public void startGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
