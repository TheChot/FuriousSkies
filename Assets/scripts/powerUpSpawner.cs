using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpSpawner : MonoBehaviour
{
    public string powerUp;        
    public float spawnChance;
    public bool hasSpawned;
    float randomChance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {   

        if(levelManager.instance.restActive)
        {
            if(!hasSpawned)
            {
                randomChance = Random.Range(0f, 1.0f);
                if(randomChance < spawnChance)
                {
                    hasSpawned = true;
                    GameObject powerUpClone = objectPooler.instance.spawnFromPool(powerUp, transform.position, transform.rotation);
                }
            }
        }

        if(!levelManager.instance.restActive)
        {
            hasSpawned = false;
        }        
        
    }

    
}
