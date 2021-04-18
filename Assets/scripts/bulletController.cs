using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    
    public Vector2 attackCheckSize;
    public LayerMask whatIsEnemy;
    public bool playerBullet;
    public int dmg;
    public string explosion;    

    
    void FixedUpdate()
    {


        
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(attackCheckSize.x, attackCheckSize.y, 1));
    }
}

