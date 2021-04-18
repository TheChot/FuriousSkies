using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2 : MonoBehaviour
{
    bossController bc;

    public float ySpeed;
    public float maxY;
    public float minY;
    
    public float shootTimeP1;
    float shootTimeResetP1;
    public int bulletsToFire;
    public Transform shootPoint;
    public Transform[] shootPoints;    
    public string phase1Bullet;
    public float shootDelayTime;
    bool shootingDone;
    
    bool charge;
    bool stopMove;
    bool startRetreat;
    bool startCharge;
    bool startRetreat2;
    bool isCharging;
    public Transform thePlayer;
    public float chargeSpeed;
    public float retreatSpeed;
    public float posX;
    public float maxRetreatX;
    public float minChargeX;

    public float chargeTimer;
    float chargeTimerReset;
    bool canShoot = true;

    // phaase 
    public float speed;
    public Vector2 maxCords;
    public Vector2 minCords;
    float xCord;
    float yCord;
    bool canCount;
    bool canShoot2;
    public float stopTime;
    float stopTimeReset;
    bool phase3Start;
    bool isStopped;
    public float shootDelayTime2;
    public int bulletsToFire2;
    public string phase3Bullet;
    bool firstTime;
    bool bulletsFired;
    public float stopTime2;
    float stopTimeReset2;
    

    // Start is called before the first frame update
    void Start()
    {
        bc = (bossController)GetComponent<bossController>();
        shootTimeResetP1 = shootTimeP1;
        chargeTimerReset = chargeTimer;
        stopTimeReset = stopTime;
        stopTimeReset2 = stopTime2;
    } 
    
    void OnDisable() 
    {
        phase3Start = false;
        shootTimeP1 = shootTimeResetP1; 
        
    }   

    void FixedUpdate()
    {
        if(bc.bossReady)
        {
            if(bc.phase1)
            {
                phase1();
                // phase1Movement();
            }

            if(bc.phase2)
            {
                
                phase2();
            }

            if(bc.phase3)
            {
                
                if(phase3Start)
                {
                    phase3();
                } else 
                {
                    moveToPosition();
                }
                    
            }

            // if(isStopped)
            //     transform.
        }
    }

    void phase1()
    {
        phase1Movement();
        phase1Shooting();
        
    }

    void phase1Movement()
    {
        transform.position += (new Vector3(0, ySpeed, 0) * Time.fixedDeltaTime);

        if(transform.position.y >= maxY)
        {
            ySpeed = -ySpeed;
        } else if(transform.position.y <= minY)
        {
            ySpeed = Mathf.Abs(ySpeed);
        }
    }

    void phase1Shooting()
    {
        if(shootTimeP1 > 0)
        {
            canShoot = true;
            shootingDone = false;
            shootTimeP1 -= Time.deltaTime;
        }

        if(shootTimeP1 <= 0)
        {
            if(canShoot)
            {
                StartCoroutine(shootBulletDelay());
            }
            
            if(shootingDone)
            {
                shootTimeP1 = shootTimeResetP1; 
            }
            
            
            
        }
    }

    IEnumerator shootBulletDelay()
    {
        canShoot = false;
        
        for (int i = 0; i <= bulletsToFire; i++)
        {
            yield return new WaitForSeconds(shootDelayTime);
            GameObject bulletClone = objectPooler.instance.spawnFromPool(phase1Bullet, shootPoint.position, shootPoint.rotation);            
            soundManager.instance.playEnemyBullet();
        }

        shootingDone = true;
        
    }

    void phase2()
    {
        if(!stopMove)
        {
            phase1Movement();
        }
        

        if(!charge)
        {
            
            phase1Shooting();
            if(shootingDone)
            {
                charge = true;
            }
            // if(chargeTimer > 0)
            // {
            //     chargeTimer -= Time.deltaTime;
            //     canShoot = true;
            // }

            // if(canShoot)
            // {
            //     phase1Shooting();
            // }

            // if(chargeTimer <= 0)
            // {
            //     charge = true;
            //     canShoot = false;
            // }
            
        } else 
        {
            if(!isCharging)
            {
                bc.activateShield();
            }
            
            isCharging = true;
            chargeAttack();            
            
        } 
    }

    void chargeAttack()
    {
        if(thePlayer.position.y > transform.position.y - 0.3 && thePlayer.position.y < transform.position.y + 0.3)
        {
            startRetreat = true;
        }

        if(startRetreat)
        {
            stopMove = true;
            transform.position += (new Vector3(retreatSpeed, 0, 0) * Time.fixedDeltaTime);
        }

        if(transform.position.x > maxRetreatX && startRetreat)
        {
            startRetreat = false;
            startCharge = true;
        }

        if(startCharge)
        {
            transform.position -= (new Vector3(chargeSpeed, 0, 0) * Time.fixedDeltaTime);
        }

        if(transform.position.x < minChargeX && startCharge)
        {
            startCharge = false;
            startRetreat2 = true;
        }

        if(startRetreat2)
        {
            transform.position += (new Vector3(retreatSpeed, 0, 0) * Time.fixedDeltaTime);
        }

        if(transform.position.x >= posX && startRetreat2)
        {
            startRetreat2 = false;
            charge = false;
            stopMove = false;
            isCharging = false;
            bc.activateShield();
            canShoot = true;
            startRetreat = false;
        }
    }

    void phase3()
    {
        if(!isStopped)
        {
            moveAround();
            canShoot2 = true;
            // Debug.Log("Hi");
        } else 
        {
            xCord = Random.Range(minCords.x, maxCords.x);
            yCord = Random.Range(minCords.y, maxCords.y);
            if(canCount)
            {
                stopTime -= Time.deltaTime;
            }

            if(canShoot2)
            {
                fireBullets();
            }
            
            if(stopTime <= 0)
            {
                
                stopTime = stopTimeReset;
                canCount = false;
                // canShoot2 = true;
                if(bc.shielded)
                {
                    bc.activateShield();
                    
                }
                
                if(firstTime)
                {
                    canCount = true;
                    firstTime = false;
                    isStopped = false;
                    canShoot2 = true;
                }

            }

            if(bulletsFired)
            {
                if(stopTime2 > 0)
                {
                    stopTime2 -= Time.deltaTime;
                }

                if(stopTime2 <= 0)
                {
                    canCount = true;        
                    isStopped = false;  
                    stopTime2 = stopTimeReset2;  
                    bulletsFired = false;
                }
                
            }
        }

        if(transform.position.x == xCord && transform.position.y == yCord)
        {
            isStopped = true;
        }
    }

    void moveToPosition()
    {
        if(!bc.shielded)
        {
            bc.activateShield();
        }

        if(transform.position.x < minCords.x && !isStopped)
        {
            transform.position += (new Vector3(retreatSpeed, 0, 0) * Time.fixedDeltaTime);

        }
        if(transform.position.x > maxCords.x  && !isStopped)
        {
            transform.position -= (new Vector3(retreatSpeed, 0, 0) * Time.fixedDeltaTime);
        }

        if(transform.position.x >= minCords.x && transform.position.x <= maxCords.x)
        {
            isStopped = true;
            canShoot2 = false;
            canCount = true;
            phase3Start = true;
            firstTime = true;
        }
    }

    void moveAround()
    {
        // move enemy to position
        Vector3 moveTo = new Vector3(xCord, yCord, transform.position.z);
        float stepSpeed = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, moveTo, stepSpeed);
    }

    void fireBullets()
    {
        StartCoroutine(shootBulletDelay2());        
    }

    IEnumerator shootBulletDelay2()
    {
        bulletsFired = false;
        canShoot2 = false;
        for (int i = 0; i < bulletsToFire2; i++)
        {
            yield return new WaitForSeconds(shootDelayTime2);
            for (int j = 0; j < shootPoints.Length; j++)
            {
                soundManager.instance.playEnemyBullet();
                GameObject bulletClone = objectPooler.instance.spawnFromPool(phase3Bullet, shootPoints[j].position, shootPoints[j].rotation);
                
            }
        }
        bulletsFired = true;
        
    }


}
