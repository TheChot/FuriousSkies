using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss3 : MonoBehaviour
{
    bossController bc;

    public string phase1Bullet;
    public Transform shootPoint;
    public Transform[] shootPoints;
    public Transform target;
    public float shootTimeP1;
    float shootTimeP1Reset;
    bool canCount = true;
    bool canShoot = true;
    public int bulletsToFire;
    public int bulletsToFire2;
    public float bulletDelay;
    public float bulletDelay2;
    public float rotateSpeed;
    bool singleCannon;
    public float shieldTime;
    float shieldTimeReset;
    public float shieldOnTime;
    float shieldOnTimeReset;

    public string missile;
    public GameObject[] warningSigns;
    // public float warningTime;
    public float missileDelay;
    public int missilesToFire;
    public float shootTimeP2;
    float shootTimeP2Reset;
    
    bool phase3Start;
    // for spikes
    public float spikeDelay;
    public Animator spikeAnim;
    // for enemy
    public Transform enemyPoint;
    public GameObject enemy;
    public float enemySpawnTime;
    float enemySpawnTimeReset;
    

    // Start is called before the first frame update
    void Start()
    {
        bc = (bossController)GetComponent<bossController>();
        shootTimeP1Reset = shootTimeP1;
        shootTimeP2Reset = shootTimeP2;
        shieldTimeReset = shieldTime;
        shieldOnTimeReset = shieldOnTime;
        enemySpawnTimeReset = enemySpawnTime;
    }

    void OnDisable() 
    {
        phase3Start = false;
        spikeAnim.SetBool("activated", false);
        enemy.SetActive(false);
        canCount = true;
        canShoot = true;

        for (int j = 0; j < warningSigns.Length; j++)
        {            
            warningSigns[j].SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(bc.bossReady)
        {
            moveTurret();

            

            if(bc.phase1)
            {
                phase1();
                theShield();
            }

            if(bc.phase2)
            {
                phase2();
                theShield();
                
            }

            if(bc.phase3)
            {
                if(!phase3Start)
                {
                    for (int j = 0; j < warningSigns.Length; j++)
                    {            
                        warningSigns[j].SetActive(false);
                    }
                    StartCoroutine(spikeAnimate());
                } else 
                {
                    phase3();
                    theShield();
                }
                    
            }
        }
    }

    void moveTurret()
    {
        // rotation code
        Vector2 direction = target.position - shootPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        shootPoint.rotation = Quaternion.Slerp(shootPoint.rotation, rotation, rotateSpeed * Time.deltaTime);
    }

    void theShield()
    {
        if(shieldTime > 0)
        {
            shieldTime -= Time.deltaTime;
        }

        if(shieldTime <= 0 && !bc.shielded)
        {
            bc.activateShield();
        }

        if(bc.shielded)
        {
            if(shieldOnTime > 0)
            {
                shieldOnTime -= Time.deltaTime;
            }

            if(shieldOnTime <= 0)
            {
                bc.activateShield();
                shieldTime = shieldTimeReset;
                shieldOnTime = shieldOnTimeReset;
            }
            
        }
    }

    void phase1()
    {
        if(canCount && shootTimeP1 > 0)
        {
            shootTimeP1 -= Time.deltaTime;
        }

        if(shootTimeP1 <=0)
        {
            if(canShoot)
            {
                canCount = false;
                shootTimeP1 = shootTimeP1Reset;
                canShoot = false;
                if(singleCannon)
                {
                    StartCoroutine(shootBulletDelay());
                    singleCannon = !singleCannon;
                } else 
                {
                    StartCoroutine(shootBulletDelay2());
                    singleCannon = !singleCannon;
                }
                
            }
            
        }

    }

    
    IEnumerator shootBulletDelay()
    {
        
        for (int i = 0; i < bulletsToFire; i++)
        {
            yield return new WaitForSeconds(bulletDelay);            
            soundManager.instance.playEnemyBullet();
            GameObject bulletClone = objectPooler.instance.spawnFromPool(phase1Bullet, shootPoint.position, shootPoint.rotation);
                
            
        }
        canCount = true;
        canShoot = true;
        
    }

    IEnumerator shootBulletDelay2()
    {
        
        for (int i = 0; i < bulletsToFire; i++)
        {
            yield return new WaitForSeconds(bulletDelay);
            for (int j = 0; j < shootPoints.Length; j++)
            {
                soundManager.instance.playEnemyBullet();
                GameObject bulletClone = objectPooler.instance.spawnFromPool(phase1Bullet, shootPoints[j].position, shootPoints[j].rotation);
                
            }
        }
        canCount = true;
        canShoot = true;
        
    }

    void phase2()
    {
        if(canCount && shootTimeP2 > 0)
        {
            shootTimeP2 -= Time.deltaTime;
        }

        if(shootTimeP2 <=0)
        {
            if(canShoot)
            {
                canCount = false;
                shootTimeP2 = shootTimeP2Reset;
                canShoot = false;
                if(singleCannon)
                {
                    StartCoroutine(shootBulletDelay());
                    singleCannon = !singleCannon;
                } else 
                {
                    StartCoroutine(shootMissiles());
                    singleCannon = !singleCannon;
                }
                
            }
            
        }
    }

    IEnumerator shootMissiles()
    {
        
        for (int i = 0; i < missilesToFire; i++)
        {
            int randomNumber = Random.Range(0, warningSigns.Length);
            for (int j = 0; j < warningSigns.Length; j++)
            {
                if(randomNumber == j)
                    continue;

                warningSigns[j].SetActive(true);
                
            }

            yield return new WaitForSeconds(missileDelay);
            for (int j = 0; j < warningSigns.Length; j++)
            {
                if(randomNumber == j)
                    continue;

                warningSigns[j].SetActive(false);
                soundManager.instance.playEnemyBullet();
                GameObject bulletClone = objectPooler.instance.spawnFromPool(missile, warningSigns[j].transform.position, warningSigns[j].transform.rotation);
                
                
            }
        }
        canCount = true;
        canShoot = true;
        
    }

    void phase3()
    {
        if(canCount && shootTimeP1 > 0)
        {
            shootTimeP1 -= Time.deltaTime;
        }

        if(shootTimeP1 <=0)
        {
            if(canShoot)
            {
                canCount = false;
                shootTimeP1 = shootTimeP1Reset;
                canShoot = false;
                StartCoroutine(shootBulletDelay());
            }            
        }

        if(!enemy.activeInHierarchy)
        {
            if(enemySpawnTime > 0)
            {
                enemySpawnTime -= Time.deltaTime;
            }

            if(enemySpawnTime <= 0)
            {
                enemy.transform.position = enemyPoint.position;
                enemy.transform.rotation = enemyPoint.rotation;
                enemy.SetActive(true);
                enemySpawnTime = enemySpawnTimeReset;
            }
        }
    }

    IEnumerator spikeAnimate()
    {
        spikeAnim.SetBool("activated", true);
        if(!bc.shielded)
        {            
            bc.activateShield();
        }
        yield return new WaitForSeconds(spikeDelay);     
        phase3Start = true;
        if(bc.shielded)
        {            
            bc.activateShield();
        }
        
    }

    
}
