using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public float speed = -5.0f;
    Vector3 pos;
    
    [Header("Sin Movement")]
    public bool sinMovement;
    public float magnitude = 0.5f;
    public float frequency = 20.0f;

    [Header("Mid Movement")]
    public bool otherMove;
    public float changePoint;
    public float maxOtPoint;
    public float minOtPoint;
    bool hMove;
    bool hMoved;
    Vector3 ogPos;
    
    

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        ogPos = transform.position;
    }

    void OnEnable()
    {
        pos = transform.position;
        ogPos = transform.position;
        hMoved = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!otherMove)
            transform.position -= (new Vector3(speed, 0, 0) * Time.fixedDeltaTime);
        
        sinMove();
        otherMovement();
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

    void otherMovement()
    {
        if(otherMove)
        {
            if(!hMove)
            {
                transform.position -= (new Vector3(speed, 0, 0) * Time.fixedDeltaTime);
            }

            if(transform.position.x <= changePoint && !hMoved)
                hMove = true;
            
            if(hMove)
            {
                if(ogPos.y > 0)
                {
                    transform.position -= (new Vector3(0, speed, 0) * Time.fixedDeltaTime);
                } else 
                {
                    transform.position += (new Vector3(0, speed, 0) * Time.fixedDeltaTime);
                }

                if(ogPos.y > 0 && transform.position.y <= -minOtPoint)
                {
                    hMove = false;
                    hMoved = true;
                } 
                else if(ogPos.y < 0 && transform.position.y >= maxOtPoint)
                {
                    hMove = false;
                    hMoved = true;
                }
                
            }

        } else
        {
            return;
        }
    }

    

    
}
