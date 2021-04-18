using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeMovement : MonoBehaviour
{
    public bool stillSpike;
    public float speed;
    public float vSpeed;
    public bool upDown;
    Vector3 ogPos;
    public float maxY;
    public float minY;
    public bool singleMove;
    public float startX;
    public float stopX;

    bool moveUp;
    bool isDropping;

    // Start is called before the first frame update
    void Start()
    {
        isDropping = false;
        ogPos = transform.position;
        if(ogPos.y < 0)
        {
            moveUp = true;
            
        } else 
        {
            
            moveUp = false;
            
        }
    }
    
    void OnEnable()
    {
        ogPos = transform.position;
        isDropping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(stillSpike)
            transform.position -= (new Vector3(speed, 0, 0) * Time.fixedDeltaTime);

        if (upDown)
            upDownMove();

        if(singleMove)
            singleMovement();
        
    }

    void upDownMove()
    {
        if(transform.position.y <= minY)
            moveUp = true;
        
        if(transform.position.y >= maxY)
            moveUp = false;
        

        if(moveUp)
        {
            transform.position += (new Vector3(-speed, vSpeed, 0) * Time.fixedDeltaTime);
        } else
        {
            transform.position -= (new Vector3(speed, vSpeed, 0) * Time.fixedDeltaTime);
        }
    }

    void singleMovement()
    {
        if(ogPos.y < 0)
        {
            if(transform.position.x < startX && !isDropping)
            {
                if(transform.position.y < maxY)
                {
                    transform.position += (new Vector3(-speed, vSpeed, 0) * Time.fixedDeltaTime);
                } else 
                {
                    transform.position -= (new Vector3(speed, 0, 0) * Time.fixedDeltaTime);
                }
                
            }

            if(transform.position.x < stopX)
            {
                isDropping = true;
                transform.position -= (new Vector3(speed, vSpeed, 0) * Time.fixedDeltaTime);
            }
            

        }

        if(ogPos.y > 0)
        {
            if(transform.position.x < startX && !isDropping)
            {
                if(transform.position.y > minY)
                {
                    transform.position -= (new Vector3(speed, vSpeed, 0) * Time.fixedDeltaTime);
                } else
                {
                    transform.position -= (new Vector3(speed, 0, 0) * Time.fixedDeltaTime);
                }
                
            }

            if(transform.position.x < stopX)
            {
                isDropping = true;
                transform.position += (new Vector3(-speed, vSpeed, 0) * Time.fixedDeltaTime);
            }
            

        }
    }
}
