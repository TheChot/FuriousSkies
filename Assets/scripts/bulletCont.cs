using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCont : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10.0f;
    public Vector2 attackCheckSize;
    public LayerMask whatIsEnemy;
    public bool playerBullet;
    public int dmg;
    public string explosion;

    public bool diagonal;
    public float diagonalX;
    public float diagonalY;

    public bool bouncingBullet;
    public float maxY;
    public float minY;
    float ogY;
    public bool largeBullet;
    public int largeBulletDmg;
    // bool ySet;

    // bool gameStart;
    
    // [Header("Audio")]
    // AudioSource bulletSound;

    // Start is called before the first frame update
    void Awake()
    {
        // bulletSound = (AudioSource)GameObject.Find("bullet sound").GetComponent<AudioSource>();        
        ogY = diagonalY;
        // if(!ySet){
        //     ogY = diagonalY;
        //     ySet = true;
        // }
    }

    void Start()
    {
        dmg = largeBullet ? largeBulletDmg : dmg;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // speed = playerBullet ? speed * 1 : speed * -1;

        if(!diagonal)
        {
            transform.position += (new Vector3(speed, 0, 0) * Time.fixedDeltaTime);
        } else
        {
            transform.position += (new Vector3(diagonalX, diagonalY, 0) * Time.fixedDeltaTime);
        }

        if(bouncingBullet)
        {
            if(transform.position.y >= maxY)
            {
                diagonalY = diagonalY * -1;
            } else if(transform.position.y <= minY)
            {
                diagonalY = Mathf.Abs(diagonalY);
            }
        }
        
        
        Collider2D[] playerToAttack  = Physics2D.OverlapBoxAll(transform.position, attackCheckSize, 0, whatIsEnemy);
        
        if(playerToAttack.Length > 0)
        {
            // playerToAttack[0].gameObject.GetComponent<playerMovement>().playerHurt(meleeDmg);
            if(playerBullet)
            {
                
                if(playerToAttack[0].gameObject.layer == LayerMask.NameToLayer("boss"))
                {
                    playerToAttack[0].gameObject.GetComponent<bossController>().takeDamage((float)dmg);
                } else {
                    
                    playerToAttack[0].gameObject.GetComponent<enemyController>().takeDamage(dmg);
                }
            } else
            {
                playerToAttack[0].gameObject.GetComponent<playerController>().playerHit();
            }
            
            // GameObject explosionClone = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            GameObject explosionClone = objectPooler.instance.spawnFromPool(explosion, transform.position, transform.rotation);
            // Destroy(gameObject);
            gameObject.SetActive(false);
            
        }
    }

    void OnEnable()
    {
        // if(gameStart)
        //     bulletSound.Play(0);
        
        // gameStart = true;
        diagonalY = ogY;
    }
    
    void OnDisable()
    {
        if(transform.parent)
        {
            transform.localPosition = new Vector3(0,0,0);
            // gameObject.SetActive(true);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(attackCheckSize.x, attackCheckSize.y, 1));
    }
}
