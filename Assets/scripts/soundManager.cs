using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    // mock singleton
    #region 
    public static soundManager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public AudioSource explosionSound;
    public AudioSource enemyBulletSound;
    public AudioSource playerBulletSound;

    public AudioSource themeSong;

    public bool soundEffects = true;
    public bool bgMusic = true;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Theme Song") || PlayerPrefs.HasKey("Sound Effects"))
        {
            bgMusic = PlayerPrefs.GetInt("Theme Song") == 0 ? true : false;
            soundEffects = PlayerPrefs.GetInt("Sound Effects") == 0 ? true : false;
        }
        else
        {
            // if sound is on then player prefs is saved as 0 else 1
            PlayerPrefs.GetInt("Theme Song", 0);
            PlayerPrefs.GetInt("Sound Effects", 0);
        }

        if(!soundEffects)
        {
            explosionSound.gameObject.SetActive(false);
            enemyBulletSound.gameObject.SetActive(false);
            playerBulletSound.gameObject.SetActive(false);
        }

        if(!themeSong)
        {
            themeSong.gameObject.SetActive(false);
        }
    }

    public void playPlayerBullet()
    {
        if(!soundEffects)
            return;
        
        playerBulletSound.Play(0);
    }

    public void playEnemyBullet()
    {
        if(!soundEffects)
            return;
        
        enemyBulletSound.Play(0);
    }

    public void playExplosionSound()
    {
        if(!soundEffects)
            return;
        
        explosionSound.Play(0);
    }

    
}
