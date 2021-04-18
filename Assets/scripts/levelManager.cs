using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    // mock singleton
    #region 
    public static levelManager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public Text scoreText;
    public Text finalScore;
    public Text highScoreText;
    public float totalScore;
    float highScore;
    float score;
    float distance = 0;
    public float disMultiplier = 5.0f;
    [Header("On Death")]
    public bool isDead;
    public GameObject thePlayer;
    public GameObject deathMenu;
    public GameObject pauseMenu;
    public GameObject continueMenu;
    public Text continueTimeText;    
    public bool canContinue = true;
    public float continueTime;
    public GameObject controlGUI;

    [Header("Rest Period")]
    public float restTime;
    public float restStep;
    public bool restActive;
    float restStart;
    float restEnd;
    [Header("For Bosses")]
    public GameObject[] bosses;
    public GameObject finalBoss;
    public float maxScore;
    int collectibles;
    public float bossDistance;
    public float bossDelay;
    float bossDelayReset;
    public bool bossActivated;
    public float bossDeactivateDelay;
    float bossDeactivateDelayReset;
    bool deactivateTheBoss;
    [Header("Is Paused")]
    public bool isPaused;

    [Header("For Ads")]
    public adManager adManager;
    public bool disableAds;
    bool adDisplayed;

    [Header("For Cam Shake")]
    public Animator cameraAnim;

    [Header("Progression Controller")]
    public int currentLevel;
    public int maxLevel;

    

    // Start is called before the first frame update
    void Start()
    {
        restStart = distance + restStep;
        restEnd = restStart + restTime;
        bossDelayReset = bossDelay;
        
        if(PlayerPrefs.HasKey("High Score")){
            highScore = PlayerPrefs.GetFloat("High Score");
            highScoreText.text = highScore.ToString();
        } else 
        {
            highScoreText.text = "0";
        }

        collectibles = PlayerPrefs.GetInt("Diamonds", 0);
        // Debug.Log(collectibles);
        // collectibles = 12;
        
        currentLevel = 0;

        bossDeactivateDelayReset = bossDeactivateDelay;
    }

    // Update is called once per frame
    void Update()
    {
        

        if(!isDead)
        {
            if(!bossActivated)
            {
                distance += Time.deltaTime * disMultiplier;
            }
            
            totalScore = distance + score;
        }else
        {
            // thePlayer.SetActive(false);
            
            if(!canContinue){
                // display ad after death
                if(!adDisplayed && !adDisplayed)
                {
                    adManager.displayInterstitial();
                    adDisplayed = true;
                }
                
                if(adManager.adClosed || disableAds){
                    
                    deathMenu.SetActive(true);
                    adManager.destroyInterStitial();
                    controlGUI.SetActive(false);
                    // StartCoroutine(ScoreUpdater());
                    finalScore.text = Mathf.Round(totalScore).ToString();
                    
                    if(PlayerPrefs.HasKey("High Score"))
                    {
                        if(highScore < totalScore)
                        {
                            PlayerPrefs.SetFloat("High Score", Mathf.Round(totalScore));
                            highScoreText.text = Mathf.Round(totalScore).ToString();
                        }
                        
                    } else
                    {
                        PlayerPrefs.SetFloat("High Score", Mathf.Round(totalScore));
                        highScoreText.text = Mathf.Round(totalScore).ToString();
                    }
                }
            } else 
            {
                continueMenu.SetActive(true);
                Time.timeScale = 0;
                continueTime -= Time.fixedDeltaTime;
                continueTimeText.text = Mathf.Round(continueTime).ToString();
                controlGUI.SetActive(false);
                if(continueTime < 0)
                {
                    canContinue = false;
                    Time.timeScale = 1;
                    continueMenu.SetActive(false);
                }
            }
        }

        scoreText.text = Mathf.Round(totalScore).ToString();

        // Rest period
        if(distance > restStart)
        {
            restActive = true;
            
        }

        if(distance > restEnd)
        {
            restActive = false;
            restStart = restEnd + restStep;
            restEnd = restStart + restTime;
        }

        if(distance > bossDistance)
        {
            bossActivated = true;
            bossDelay -= Time.deltaTime;
            
        }

        if(bossDelay < 0 && bossActivated)
        {
            
            if(collectibles == collectibleManager.instance.totalCollectibles && totalScore >= maxScore)
            {
                finalBoss.SetActive(true);
                bossDelay = bossDelayReset;
                bossDistance += bossDistance;

            } else
            {
                int randomNumber = Random.Range(0, bosses.Length);
                bosses[randomNumber].SetActive(true);
                bossDelay = bossDelayReset;
                bossDistance += bossDistance;
                
            }
            
        }

        if(bossActivated && restActive)
        {
            
            restStart = restEnd + restStep;
            restEnd = restStart + restTime;
            restActive = false;
            
        }
        
        
        
    }

    void FixedUpdate()
    {
        if(deactivateTheBoss)
        {
            bossDeactivateDelay -= Time.deltaTime;
        }

        if(bossDeactivateDelay <= 0)
        {
            deactivateTheBoss = false;
            bossDeactivateDelay = bossDeactivateDelayReset;
            bossActivated = false;

        }
    }

    public void deactivateBoss()
    {
        deactivateTheBoss = true;
    }

    // private IEnumerator ScoreUpdater()
    // {
    //     float displayScore = 0f;
    //     while(true)
    //     {
            
    //         if(displayScore < score)
    //         {
    //             displayScore += 5.0f; 
    //             finalScore.text = Mathf.Round(displayScore).ToString(); //Write it to the UI
    //         }
    //         yield return new WaitForSeconds(0.2f); // I used .2 secs but you can update it as fast as you want
    //     }
    // }

    public void addToScore(float points)
    {
        score += points;
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        controlGUI.SetActive(false);
    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        controlGUI.SetActive(true);
    }

    public void reloadLevel()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void revivePlayer()
    {
        canContinue = false;
        Time.timeScale = 1;
        continueMenu.SetActive(false);
        thePlayer.SetActive(true);
        isDead = false;
        controlGUI.SetActive(true);
    }

    public void camShake()
    {
        cameraAnim.SetTrigger("shake");
    }

    public void killPlayer()
    {
        StartCoroutine(killPlayerDelay());
    }

    IEnumerator killPlayerDelay()
    {
        yield return new WaitForSeconds(1.0f);
        isDead = true;
    }

    public void levelUpdate()
    {
        if(currentLevel < maxLevel)
        {
            currentLevel++;
        }
    }

    
    

    public void quitGame()
    {
        Application.Quit();
    }

    public void activateGodMode()
    {
        thePlayer.GetComponent<playerController>().godMode = !thePlayer.GetComponent<playerController>().godMode;
    }

    public void closeContinueMenu()
    {
        continueMenu.SetActive(false);
        canContinue = false;
        Time.timeScale = 1;
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene(0);
    }



}
