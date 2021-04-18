using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibleManager : MonoBehaviour
{
    // mock singleton
    #region 
    public static collectibleManager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public string diamond;
    public int easyLevelCount;
    public int medLevelCount;
    public int hardLevelCount;
    public int shLevelCount;
    public int levelTotal;
    public float easySpawnChance;
    public float medSpawnChance;
    public float hardSpawnChance;
    public float shSpawnChance;
    public int totalCollectibles;
    int totalCollected;
    int collectedNow = 0;
    int spawnedNow;

    public float spawnTimer;
    float spawnTimerReset;
    public float spawnTimerCut;
    // Start is called before the first frame update
    void Start()
    {
        totalCollectibles = easyLevelCount + medLevelCount + hardLevelCount + shLevelCount;
        spawnTimerReset = spawnTimer;
        if(PlayerPrefs.HasKey("Diamonds")){
            totalCollected = PlayerPrefs.GetInt("Diamonds");
            // totalCollected = totalCollectibles;
            
        } else 
        {
            
            PlayerPrefs.SetInt("Diamonds", 0);            
            totalCollected = 0;
        }

        collectedNow = 0;
        spawnedNow = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!levelManager.instance.bossActivated)
        {

            if(collectedNow < levelTotal)
                spawnTimer -= Time.deltaTime;
            
            if(spawnTimer <= 0 && spawnedNow < levelTotal)
            {
                if (levelManager.instance.currentLevel == 0 && totalCollected < easyLevelCount)
                {
                    float randomChance = Random.Range(0f, 1.0f);
                    if(randomChance < easySpawnChance)
                    {                        
                        GameObject diamondClone = objectPooler.instance.spawnFromPool(diamond, transform.position, transform.rotation);
                        spawnedNow++;
                    }
                }

                int medCount = medLevelCount + easyLevelCount;

                if (levelManager.instance.currentLevel == 1 && totalCollected < medCount)
                {
                    float randomChance = Random.Range(0f, 1.0f);
                    if(randomChance < medSpawnChance)
                    {                        
                        GameObject diamondClone = objectPooler.instance.spawnFromPool(diamond, transform.position, transform.rotation);
                        spawnedNow++;
                    }
                }

                int hardCount = medCount + hardLevelCount;

                if (levelManager.instance.currentLevel == 2 && totalCollected < hardCount)
                {
                    float randomChance = Random.Range(0f, 1.0f);
                    if(randomChance < hardSpawnChance)
                    {                        
                        GameObject diamondClone = objectPooler.instance.spawnFromPool(diamond, transform.position, transform.rotation);
                        spawnedNow++;
                    }
                }

                int shCount = shLevelCount + hardCount;

                if (levelManager.instance.currentLevel >= 3 && totalCollected < shCount)
                {
                    float randomChance = Random.Range(0f, 1.0f);
                    if(randomChance < shSpawnChance)
                    {                        
                        GameObject diamondClone = objectPooler.instance.spawnFromPool(diamond, transform.position, transform.rotation);
                        spawnedNow++;
                    }
                }

                spawnTimer = spawnTimerReset;
            }

        }

        if(levelManager.instance.isDead)
        {
            PlayerPrefs.SetInt("Diamonds", totalCollected);
        }
    }

    public void collectDiamond()
    {
        totalCollected++;
        collectedNow++;
    }
}
