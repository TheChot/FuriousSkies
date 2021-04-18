using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileLauncher : MonoBehaviour
{
    public string missile;
    public float timer;
    float timerReset;
    public float shootTimer;
    float shootTimerReset;
    public float attackChance;
    public GameObject warning;
    public float maxY;
    public float minY;
    float missileYPoint;
    bool firing;
    float randomChance;
    public Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        timerReset = timer;    
        shootTimerReset = shootTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(levelManager.instance.isPaused)
            return;
        // GameObject explosionClone = objectPooler.instance.spawnFromPool(explosion, transform.position, transform.rotation);

        if(levelManager.instance.bossActivated || levelManager.instance.restActive)
        {
            timer = timerReset;
            firing = false;
            warning.SetActive(false);
            return;
        }

        if(timer > 0)
        {
            timer -= Time.deltaTime;
            randomChance = Random.Range(0f, 1.0f);
            // missileYPoint = Random.Range(minY, maxY);
            missileYPoint = playerPosition.position.y;
        }

        if(timer <= 0)
        {   
            
            if(randomChance < attackChance)
            {
                firing = true;
                warning.SetActive(true);
                warning.transform.position = new Vector3(warning.transform.position.x, missileYPoint, warning.transform.position.z);
                shootTimer -= Time.deltaTime;
                
                if(shootTimer <= 0)
                {
                    fireMissile();
                }
            }

            if(!firing)
            {
                timer = timerReset;
                shootTimer = shootTimerReset;
            }
            
        }
        
    }

    public void fireMissile()
    {
        Vector3 missileSpawnPoint = new Vector3(transform.position.x, missileYPoint, transform.position.z);
        GameObject missileClone = objectPooler.instance.spawnFromPool(missile, missileSpawnPoint, transform.rotation);
        warning.SetActive(false);
        firing = false;
    }
}
