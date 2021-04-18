using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetController : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void OnEnable()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position -= (new Vector3(speed, 0, 0) * Time.fixedDeltaTime);   
    }
}
