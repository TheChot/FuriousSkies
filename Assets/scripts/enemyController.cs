using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyController : MonoBehaviour
{
    [Header("Health")]
    public int hp;
    int hpReset;
    // hi
    // [Header("Reset Position")]
    Vector3 startPos;
    bool hasSet = false;

    [Header("Explosion")]
    public string explosion;        
    // AudioSource explosionSound;

    [Header("Shooting Enemy")]
    public bool shootingEnemy;
    public string bullet;
    public Transform[] shootPoint;
    public float shootTime;
    float shootTimeReset;
    public float shootPointX;
    public float shootPointXMax;
    public bool shootDelay;
    public float shootDelayTime;

    [Header("Score")]
    public float points;
    public string scoreObj;
    // public TextMesh scoreText;
    [Header("Indestructable")]
    public bool indestructable;

    powerUpPlayer powerUpController;

    // Start is called before the first frame update
    void Awake()
    {
        // startPos = transform.position;
        shootTimeReset = shootTime;
        // scoreText.text = points.ToString() + "+";
    }

    void Start()
    {
        // explosionSound = (AudioSource)GameObject.Find("explosion sound").GetComponent<AudioSource>();
        powerUpController = GameObject.Find("character").GetComponent<powerUpPlayer>();
        
    }
    
    void OnEnable()
    {
        if(!hasSet)
        {
            startPos = transform.localPosition;
            hasSet = true;
            hpReset = hp;
        }

        
    }

    void OnDisable()
    {
        if(hasSet)
        {
            transform.localPosition = startPos;
            hp = hpReset;
            // Set other things like enemy health here
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            soundManager.instance.playExplosionSound();
            // GameObject explosionClone = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            GameObject explosionClone = objectPooler.instance.spawnFromPool(explosion, transform.position, transform.rotation);
            if(!levelManager.instance.isDead)
            {
                if(powerUpController.multiplier)
                {
                    powerUpController.multiplierCounter += 1;
                    points *= powerUpController.pointsMultiplier;
                }
            }
            
            levelManager.instance.addToScore(points);
            GameObject scoreText = objectPooler.instance.spawnFromPool(scoreObj, transform.position, transform.rotation);
            // StartCoroutine(ScoreUpdater(scoreText));
            scoreText.GetComponent<TextMesh>().text = points.ToString() + "+";
            levelManager.instance.camShake();
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }

        if(shootingEnemy)
        {
            if(transform.position.x < shootPointX && transform.position.x > shootPointXMax)
            {
                if(shootTime > 0)
                {
                    shootTime -= Time.deltaTime;
                }

                if(shootTime <= 0)
                {
                    
                    if(!shootDelay)
                    {
                        soundManager.instance.playEnemyBullet();
                        for (int i = 0; i < shootPoint.Length; i++)
                        {
                            GameObject bulletClone = objectPooler.instance.spawnFromPool(bullet, shootPoint[i].position, shootPoint[i].rotation);
                        }
                    } else
                    {
                        StartCoroutine(shootBulletDelay());
                    }
                    
                    
                    shootTime = shootTimeReset;
                }
                
            }
        }
    }

    public void takeDamage(int dmg)
    {
        if(indestructable)
            return;

        hp -= dmg;
    }

    IEnumerator shootBulletDelay()
    {
        for (int i = 0; i < shootPoint.Length; i++)
        {
            yield return new WaitForSeconds(shootDelayTime);
            soundManager.instance.playEnemyBullet();
            GameObject bulletClone = objectPooler.instance.spawnFromPool(bullet, shootPoint[i].position, shootPoint[i].rotation);
        }
    }

    // private IEnumerator ScoreUpdater(GameObject _scoreObj)
    // {
    //     float displayScore = 0f;
    //     while(true)
    //     {
            
    //         if(displayScore < points)
    //         {
    //             displayScore += 2.0f; 
    //             // finalScore.text = Mathf.Round(displayScore).ToString(); //Write it to the UI
    //             _scoreObj.GetComponent<TextMesh>().text = points.ToString() + "+";
    //         }
    //         yield return new WaitForSeconds(0.3f); // I used .2 secs but you can update it as fast as you want
    //     }
    // }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            // Destroy(other.gameObject);
            other.gameObject.GetComponent<playerController>().playerHit();
            GameObject explosionClone = objectPooler.instance.spawnFromPool(explosion, transform.position, transform.rotation);
            gameObject.SetActive(false);
            
        }
        // Debug.Log("I hit the player");
    }
}
