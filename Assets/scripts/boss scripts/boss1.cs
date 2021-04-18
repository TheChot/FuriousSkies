using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1 : MonoBehaviour
{
    bossController bc;
    
    public float ySpeed;
    
    public float maxY;
    public float minY;

    public Transform[] shootPoint;

    public string phase1Bullet;
    public string phase3Bullet1;
    public string phase3Bullet2;

    public float shootTime;
    float shootTimeReset;
    float maxShootTime;
    public float phase2ShootTime;
    public float phase3ShootTime;
    float phase3ShootTimeReset;
    public float shootDelayTime;
    public int bulletsToFire;
    bool phase3Ready;
    bool sideCanon;
    // bool canCount;

    // Start is called before the first frame update
    void Start()
    {
        bc = (bossController)GetComponent<bossController>();
        shootTimeReset = shootTime;
        maxShootTime = shootTime;
        phase3ShootTimeReset = phase3ShootTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(bc.bossReady)
        {
            

            if(bc.phase1)
            {
                phase1();
                phase1Movement();
            }

            if(bc.phase2)
            {
                phase1Movement();
                phase2();
            }

            if(bc.phase3)
            {
                moveToPosition();
                phase3();
            }
        }
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

    void phase1()
    {
        if(shootTime > 0)
        {
            shootTime -= Time.deltaTime;
        }

        if(shootTime <= 0)
        {
            // GameObject bulletClone = objectPooler.instance.spawnFromPool(bullet, shootPoint.position, shootPoint.rotation);
            shootPhase1();
            shootTime = shootTimeReset;
        }
    }

    void phase2()
    {
        if(shootTime > 0)
        {
            shootTime -= Time.deltaTime;
        }

        if(shootTime <= 0)
        {
            // GameObject bulletClone = objectPooler.instance.spawnFromPool(bullet, shootPoint.position, shootPoint.rotation);
            shootPhase1();
            shootTime = shootTimeReset - (maxShootTime * phase2ShootTime);
        }
    }

    void phase3()
    {
        if(phase3Ready)
        {
            if(phase3ShootTime > 0)
            {
                phase3ShootTime -= Time.deltaTime;
            }

            if(phase3ShootTime <= 0)
            {
                // GameObject bulletClone = objectPooler.instance.spawnFromPool(bullet, shootPoint.position, shootPoint.rotation);
                shootPhase3();
                phase3ShootTime = phase3ShootTimeReset;
            }
        }
    }
    
    void shootPhase1()
    {
        soundManager.instance.playEnemyBullet();
        GameObject bulletClone = objectPooler.instance.spawnFromPool(phase1Bullet, shootPoint[0].position, shootPoint[0].rotation);
    }

    void shootPhase3()
    {
        

        if(!sideCanon)
        {
            StartCoroutine(shootBulletDelay());
            sideCanon = true;
        } else 
        {
            soundManager.instance.playEnemyBullet();
            GameObject bulletClone1 = objectPooler.instance.spawnFromPool(phase3Bullet1, shootPoint[1].position, shootPoint[1].rotation);
            GameObject bulletClone2 = objectPooler.instance.spawnFromPool(phase3Bullet2, shootPoint[2].position, shootPoint[2].rotation);
            sideCanon = false;
        }
        
        
    }

    IEnumerator shootBulletDelay()
    {
        for (int i = 0; i < bulletsToFire; i++)
        {
            yield return new WaitForSeconds(shootDelayTime);
            GameObject bulletClone = objectPooler.instance.spawnFromPool(phase1Bullet, shootPoint[0].position, shootPoint[0].rotation);
            GameObject bulletClone3 = objectPooler.instance.spawnFromPool(phase1Bullet, shootPoint[3].position, shootPoint[3].rotation);
            GameObject bulletClone4 = objectPooler.instance.spawnFromPool(phase1Bullet, shootPoint[4].position, shootPoint[4].rotation);
            soundManager.instance.playEnemyBullet();
            
        }
    }

    void moveToPosition()
    {
        if(!phase3Ready){
            if(transform.position.y < 0)
            {
                transform.position += (new Vector3(0, ySpeed, 0) * Time.fixedDeltaTime);
            } else if(transform.position.y > 0)
            {
                transform.position += (new Vector3(0, -ySpeed, 0) * Time.fixedDeltaTime);
            }
        }

        if(transform.position.y < 0.2f || transform.position.y > -0.2f)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
            phase3Ready = true;
            
        }
    }

    // void shootPhase2()
    // {
    //     GameObject bulletClone = objectPooler.instance.spawnFromPool(phase1Bullet, shootPoint[0].position, shootPoint[0].rotation);
    // }
}
// use a coroutine that instantiates a warning particle effect thats on a loop(particle effect loop)
// then after the yield return new it instantiates the bullet 
// while deactivating the particle effect
