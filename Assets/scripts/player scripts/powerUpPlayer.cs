using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpPlayer : MonoBehaviour
{
    public int powerUpCount;
    // Extra life power up
    public bool extraLife;
    public GameObject extraLifeUI;
    
    // large bullets
    public bool lrgBullet;
    public string largeBullet;
    // public int largeBulletTime;    
    // float largeBulletTimeReset;
    public int largeBulletCount;
    public int largeBulletCounter = 0;

    // faster bullets
    public bool fstBullet;
    public float fastBulletPercent;
    // public float fastBulletTime;
    // float fastBulletTimeReset;
    public int fastBulletCount;
    public int fastBulletCounter = 0;
    
    // shield
    public bool shield;
    public GameObject shieldUI;

    // multiplier
    public bool multiplier;
    public float pointsMultiplier;
    public int multiplierCount;
    public int multiplierCounter = 0;

    // Update is called once per frame
    void Update()
    {
        if(levelManager.instance.isPaused)
            return;

        // Extra life power up UI
        if(extraLife)
        {
            extraLifeUI.SetActive(true);
        } else 
        {
            extraLifeUI.SetActive(false);
        }

        if(shield)
        {
            shieldUI.SetActive(true);
        } else 
        {
            shieldUI.SetActive(false);
        }

        if(largeBulletCounter >= largeBulletCount)
        {
            lrgBullet = false;
            largeBulletCounter = 0;
        }

        if(fastBulletCounter >= fastBulletCount)
        {
            fstBullet = false;
            fastBulletCounter = 0;
        }

        if(multiplierCounter >= multiplierCount)
        {
            multiplier = false;
            multiplierCounter = 0;
        }
    }

    public void collectPowerUp()
    {
        int powerUpPick = Random.Range(0, powerUpCount);
        // int powerUpPick = 4;
        switch (powerUpPick)
        {
            case 0:
                extraLife = true;
                break;
            case 1:                
                fstBullet = false;
                multiplier = false;
                lrgBullet = true;
                largeBulletCounter = 0;
                break;
            case 2:
                lrgBullet = false;
                multiplier = false;
                fstBullet = true;
                fastBulletCounter = 0;
                break;
            case 3:
                shield = true;
                break;
            case 4:                
                lrgBullet = false;
                fstBullet = false;
                multiplier = true;
                multiplierCounter = 0;
                break;    
            default:
                extraLife = true;
                break;
        }
        // powerUpCollected = true;
        // return;
        
    }
}

