using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{
    [Header("Attacking")]
    public Transform attackPos;
    public string bullet;
    public float attackTimer = 0.04f;
    float attackTimerReset;
    bool hasAttacked;

    [Header("GFX")]
    public Animator anim;

    // AudioSource explosionSound;
    public string explosion;
    public bool godMode;

    powerUpPlayer powerUpController;

    

    // Start is called before the first frame update
    void Start()
    {
        attackTimerReset = attackTimer;
        powerUpController = GetComponent<powerUpPlayer>();    
        // explosionSound = (AudioSource)GameObject.Find("explosion sound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(levelManager.instance.isPaused)
            return;
            
        if(hasAttacked)
        {
            attackTimer -= Time.deltaTime;
        } 

        if(attackTimer <= 0)
        {
            hasAttacked = false;
            if(powerUpController.fstBullet)
            {
                attackTimer = attackTimerReset - (attackTimerReset * powerUpController.fastBulletPercent);
                powerUpController.fastBulletCounter += 1;
            } 
            else
            {
                attackTimer = attackTimerReset;
            }
        }

        

        
    }

    public void fireBullet()
    {
        if(!hasAttacked)
        {
            hasAttacked = true;
            anim.SetTrigger("shoot");
            soundManager.instance.playPlayerBullet();
            // GameObject bulletClone = (GameObject)Instantiate(bullet, attackPos.position, attackPos.rotation);
            if(!powerUpController.lrgBullet)
            {
                GameObject bulletClone = objectPooler.instance.spawnFromPool(bullet, attackPos.position, attackPos.rotation);
            } else 
            {
                GameObject bulletClone = objectPooler.instance.spawnFromPool(powerUpController.largeBullet, attackPos.position, attackPos.rotation);
                powerUpController.largeBulletCounter += 1;
            }
            
        } else{
            return;
        }
        
        // bulletClone.transform.localScale = transform.localScale;
    }

    public void playerHit()
    {
        
        if(!godMode)
        {
            levelManager.instance.camShake();
            if(powerUpController.shield)
            {
                powerUpController.shield = false;
                return;
            }

            if(powerUpController.extraLife)
            {
                powerUpController.extraLife = false;
                return;
            }

            soundManager.instance.playExplosionSound();
            gameObject.SetActive(false);
            GameObject explosionClone = objectPooler.instance.spawnFromPool(explosion, transform.position, transform.rotation);
            levelManager.instance.killPlayer();
            
                    
        }
    }

    

    

    
}
