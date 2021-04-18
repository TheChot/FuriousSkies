using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOverTime : MonoBehaviour
{
    public float destroyTime = 5.0f;
    float destroyTimeReset;
    // Start is called before the first frame update
    void Start()
    {
        destroyTimeReset = destroyTime;
    }
    
    void OnDisable() 
    {
        if(destroyTimeReset != 0)
        {
            destroyTime = destroyTimeReset;
        }
        
    }

    
    // Update is called once per frame
    void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0)
        {
            gameObject.SetActive(false);
            destroyTime = destroyTimeReset;
        }
    }
}
