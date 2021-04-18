using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public float enemySpawnTime = 5.0f;
    float enemySpawnTimeReset;
    // public string[] enemyPatterns;
    public string[] enemyEasyPatterns;
    public string[] enemyNormalPatterns;
    public string[] enemyHardPatterns;
    public string[] enemySHPatterns;
    // bool hasSpawned;
    //the point where the last child has to pass
    // in order for the spawner to start counting down
    public float xGap; 
    Transform patternLastChild;
    bool canCount;
    // use for bosses and end level
    public bool canSpawn = true;

    bool spawnOnDeath;
    public float spawnOnDeathTime;
    float spawnOnDeathReset;
    
    // Start is called before the first frame update
    void Start()
    {
        enemySpawnTimeReset = enemySpawnTime;
        canCount = true;

        spawnOnDeathReset = spawnOnDeathTime;
        

    }

    // Update is called once per frame
    void Update()
    {
        if(levelManager.instance.isPaused)
            return;
            
        if(canSpawn && !levelManager.instance.restActive && !levelManager.instance.bossActivated)
        {
            if(enemySpawnTime > 0 && canCount)
            {
                enemySpawnTime -= Time.deltaTime;
            }
            
            
            if(enemySpawnTime <= 0)
            { 
                // string[] enemyPatterns = 
                canCount = false;

                if(levelManager.instance.currentLevel == 0)
                {
                    int randomNumber = Random.Range(0, enemyEasyPatterns.Length);
                    GameObject enemyPatternClone = objectPooler.instance.spawnFromPool(enemyEasyPatterns[randomNumber], transform.position, transform.rotation);
                    patternLastChild = enemyPatternClone.transform.GetChild(enemyPatternClone.transform.childCount - 1);                
                    enemySpawnTime = enemySpawnTimeReset;
                    // Sets any disable children as active if theyve been deactivated in gameplay
                    for (int i = 0; i < enemyPatternClone.transform.childCount; i++)
                    {
                        enemyPatternClone.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    
                } else if (levelManager.instance.currentLevel == 1)
                {
                    int randomNumber = Random.Range(0, enemyNormalPatterns.Length);
                    GameObject enemyPatternClone = objectPooler.instance.spawnFromPool(enemyNormalPatterns[randomNumber], transform.position, transform.rotation);
                    patternLastChild = enemyPatternClone.transform.GetChild(enemyPatternClone.transform.childCount - 1);                
                    enemySpawnTime = enemySpawnTimeReset;
                    // Sets any disable children as active if theyve been deactivated in gameplay
                    for (int i = 0; i < enemyPatternClone.transform.childCount; i++)
                    {
                        enemyPatternClone.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    
                } else if (levelManager.instance.currentLevel == 2)
                {
                    int randomNumber = Random.Range(0, enemyHardPatterns.Length);
                    GameObject enemyPatternClone = objectPooler.instance.spawnFromPool(enemyHardPatterns[randomNumber], transform.position, transform.rotation);
                    patternLastChild = enemyPatternClone.transform.GetChild(enemyPatternClone.transform.childCount - 1);                
                    enemySpawnTime = enemySpawnTimeReset;
                    // Sets any disable children as active if theyve been deactivated in gameplay
                    for (int i = 0; i < enemyPatternClone.transform.childCount; i++)
                    {
                        enemyPatternClone.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    
                } else if (levelManager.instance.currentLevel >= 3)
                {
                    int randomNumber = Random.Range(0, enemySHPatterns.Length);
                    GameObject enemyPatternClone = objectPooler.instance.spawnFromPool(enemySHPatterns[randomNumber], transform.position, transform.rotation);
                    patternLastChild = enemyPatternClone.transform.GetChild(enemyPatternClone.transform.childCount - 1);                
                    enemySpawnTime = enemySpawnTimeReset;
                    // Sets any disable children as active if theyve been deactivated in gameplay
                    for (int i = 0; i < enemyPatternClone.transform.childCount; i++)
                    {
                        enemyPatternClone.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    
                }

                
                
            }

            if(patternLastChild != null)
            {
                if(!patternLastChild.gameObject.activeSelf)
                {
                    spawnOnDeathTime -= Time.deltaTime;
                }

                if(spawnOnDeathTime <= 0)
                {
                    spawnOnDeathTime = spawnOnDeathReset;
                    spawnOnDeath = true;

                }


                if(patternLastChild.position.x < xGap || spawnOnDeath)
                {
                    canCount = true;
                    patternLastChild = null;
                    spawnOnDeath = false;
                }

                // if(!patternLastChild.gameObject.activeSelf)
                // {
                    
                // }
                    
            }
        }

        if(levelManager.instance.restActive)
        {
            enemySpawnTime = enemySpawnTimeReset;
            canCount = true;
            patternLastChild = null;
            spawnOnDeath = false;
        }
            
        
        if(levelManager.instance.bossActivated)
        {
            enemySpawnTime = enemySpawnTimeReset;
            canCount = true;
            patternLastChild = null;
            spawnOnDeath = false;
        }
            
    }

    // void Update()
    // {
    //     if(canSpawn){

    //     }
    // }

    // Notes
    // when game object instantiates the spawner must take note of the transform
    // When the transform reaches a certain distance thats when it should be allowed to spawn again
    // The timer should only start counting down when the distance is reached
}
