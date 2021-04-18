using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncingBullet : MonoBehaviour
{
    public float maxY;
    public float minY;
    public float rotationAngle;
    bool hasTurned;
    float hasTurnedTime = 0.09f;
    float hasTurnedTimeReset;
    
    void Start()
    {
        hasTurnedTimeReset = hasTurnedTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y >= maxY && !hasTurned)
        {
            // Debug.Log(transform.localEulerAngles.z);
            Vector3 bulletRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.localEulerAngles.z + rotationAngle);
            transform.rotation = Quaternion.Euler(bulletRotation);
            hasTurned = true;
        } 

        if(transform.position.y <= minY && !hasTurned)
        {
            Vector3 bulletRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.localEulerAngles.z - rotationAngle);
            transform.rotation = Quaternion.Euler(bulletRotation);
            hasTurned = true;
        }

        if(hasTurned)
        {
            hasTurnedTime -= Time.deltaTime;
        }

        if(hasTurnedTime <= 0)
        {
            hasTurnedTime = hasTurnedTimeReset;
            hasTurned = false;
        }
    }
}
