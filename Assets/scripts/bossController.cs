using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    public float maxX;
    public bool boss1;
    public float introSpeed;
    public float hp;  
    float maxHp;  
    public bool bossReady;
    public bool phase1 = true;
    public bool phase2;
    public bool phase3;
    public float phase2HpEasy;
    public float phase2HpMed;
    public float phase2HpHard;
    public float phase2HpSh;    
    public float phase3HpMed;
    public float phase3HpHard;
    public float phase3HpSh;    
    float phase2Hp;
    float phase3Hp;
    float ogPhase3;
    float ogPhase2;

    public float bossScore;
    public string scoreObj;
    public string explosion;
    // AudioSource explosionSound;

    Vector3 ogPos;

    public GameObject shield;
    public bool shielded;
    
    void Start()
    {
        maxHp = hp;
        ogPos = transform.position;

        switch (levelManager.instance.currentLevel)
        {
            case 0:
                phase2Hp = phase2HpEasy;
                phase3Hp = 0;
                break;
            case 1:                
                phase2Hp = phase2HpMed;
                phase3Hp = phase3HpMed;
                break;
            case 2:
                phase2Hp = phase2HpHard;
                phase3Hp = phase3HpHard;
                break;
            case 3:
                phase2Hp = phase2HpSh;
                phase3Hp = phase3HpSh;
                break;                
            default:
                phase2Hp = phase2HpSh;
                phase3Hp = phase3HpSh;
                break;
        }

        // explosionSound = (AudioSource)GameObject.Find("explosion sound").GetComponent<AudioSource>();
    }

    void OnEnable() 
    {
        
        switch (levelManager.instance.currentLevel)
        {
            case 0:
                phase2Hp = phase2HpEasy;
                phase3Hp = 0;
                break;
            case 1:                
                phase2Hp = phase2HpMed;
                phase3Hp = phase3HpMed;
                break;
            case 2:
                phase2Hp = phase2HpHard;
                phase3Hp = phase3HpHard;
                break;
            case 3:
                phase2Hp = phase2HpSh;
                phase3Hp = phase3HpSh;
                break;                
            default:
                phase2Hp = phase2HpSh;
                phase3Hp = phase3HpSh;
                break;
        }
    }

    void FixedUpdate() 
    {
        if(transform.position.x > maxX && !bossReady)
        {
            transform.position -= (new Vector3(introSpeed, 0, 0) * Time.fixedDeltaTime);
        }

        if(shielded)
        {
            shield.SetActive(true);
        } else
        {
            shield.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(levelManager.instance.isPaused)
            return;
        
        if(transform.position.x <= maxX)
        {
            bossReady = true;            
        }

        if(bossReady)
        {
            if(hp <= maxHp * phase2Hp)
            {
                phase1 = false;
                phase2 = true;
            }

            if(hp <= maxHp * phase3Hp)
            {
                phase1 = false;
                phase2 = false;
                phase3 = true;
            }
        }

        if(hp <= 0)
        {
            // explosionSound.Play(0);
            soundManager.instance.playExplosionSound();
            GameObject explosionClone = objectPooler.instance.spawnFromPool(explosion, transform.position, transform.rotation);
            // levelManager.instance.bossActivated = false;
            levelManager.instance.deactivateBoss();
            levelManager.instance.addToScore(bossScore);
            levelManager.instance.levelUpdate();
            GameObject scoreText = objectPooler.instance.spawnFromPool(scoreObj, transform.position, transform.rotation);
            scoreText.GetComponent<TextMesh>().text = bossScore.ToString() + "+";
            gameObject.SetActive(false);
        }
        
    }

    public void takeDamage(float dmg)
    {
        if(bossReady)
        {
            if(!shielded)
            {
                hp -= dmg;
            }
            
        }
        
    }

    public void activateShield()
    {
        // shielded = !shielded;
        if(!shielded)
        {
            shielded = true;
        } else
        {
            shielded = false;
        }
    }

    void OnDisable()
    {
        transform.position = ogPos;
        hp = maxHp;
        bossReady = false;
        phase1 = true;
        phase2 = false;
        phase3 = false;    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            other.gameObject.GetComponent<playerController>().playerHit();
        }
    }
}
