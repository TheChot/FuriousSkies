using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyV2 : MonoBehaviour
{
    public float introSpeed;
    public float speed;    
    public float rotateSpeed;
    public float xStop;
    public Vector2 maxCords;
    public Vector2 minCords;
    float xCord;
    float yCord;
    public float stopTime;
    float stopTimeReset;
    
    bool isStopped;
    bool canCount;
    bool canShoot = true;

    public int bulletsToFire;
    public Transform shootPoint;
    public string bullet;
    public float shootDelayTime;

    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        stopTimeReset = stopTime;
        target = GameObject.Find("character").transform;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.x > xStop)
        {
            transform.position -= (new Vector3(introSpeed, 0, 0) * Time.fixedDeltaTime);    
        } else {

            if(!isStopped)
            {
                moveToPosition();
            } else 
            {
                xCord = Random.Range(minCords.x, maxCords.x);
                yCord = Random.Range(minCords.y, maxCords.y);
                if(canCount)
                {
                    stopTime -= Time.deltaTime;
                }

                if(canShoot)
                {
                    fireBullets();
                }
                
                if(stopTime <= 0)
                {
                    isStopped = false;
                    stopTime = stopTimeReset;
                    canCount = false;
                    canShoot = true;

                }
            }

            if(transform.position.x == xCord && transform.position.y == yCord)
            {
                isStopped = true;
            }
        }
            
    }

    void moveToPosition()
    {
        
        
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        

        

        // move enemy to position
        Vector3 moveTo = new Vector3(xCord, yCord, transform.position.z);
        float stepSpeed = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, moveTo, stepSpeed);
    }

    void fireBullets()
    {
        StartCoroutine(shootBulletDelay());
        canCount = true;
        canShoot = false;
    }

    IEnumerator shootBulletDelay()
    {
        for (int i = 0; i < bulletsToFire; i++)
        {
            yield return new WaitForSeconds(shootDelayTime);
            soundManager.instance.playEnemyBullet();
            GameObject bulletClone = objectPooler.instance.spawnFromPool(bullet, shootPoint.position, shootPoint.rotation);
        }
    }



}
