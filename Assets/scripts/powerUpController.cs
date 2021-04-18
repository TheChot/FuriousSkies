using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpController : MonoBehaviour
{

    public float speed = -5.0f;
    Vector3 pos;
    
    [Header("Sin Movement")]
    public bool sinMovement;
    public float magnitude = 0.5f;
    public float frequency = 20.0f;
    public string explosion;



    void Start()
    {
        pos = transform.position;
        // ogPos = transform.position;
        sinMovement = Random.Range(0.0f, 1.0f) > 0.5 ? true : false;
    }

    void OnEnable()
    {
        pos = transform.position;
        sinMovement = Random.Range(0.0f, 1.0f) > 0.5 ? true : false;
        // ogPos = transform.position;
    }

    void FixedUpdate()
    {
        transform.position -= (new Vector3(speed, 0, 0) * Time.fixedDeltaTime);        
        sinMove();
    }

    void sinMove()
    {
        if(sinMovement)
        {
            pos -= transform.right * Time.fixedDeltaTime * speed;
            transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        } else
        {
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            other.gameObject.GetComponent<powerUpPlayer>().collectPowerUp();
            GameObject powerUpExplosion = objectPooler.instance.spawnFromPool(explosion, other.gameObject.transform.position, other.gameObject.transform.rotation);
            gameObject.SetActive(false);
        }
        // Debug.Log("I hit the player");
    }


}
