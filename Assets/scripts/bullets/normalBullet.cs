using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalBullet : MonoBehaviour
{
    public float speed;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += (transform.right * Time.fixedDeltaTime * speed);        
    }
}
