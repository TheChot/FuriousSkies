using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOverDistance : MonoBehaviour
{
    public float xMax;
    public float xMin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > xMax)
        {
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }

        if(transform.position.x < xMin)
        {
            gameObject.SetActive(false);
        }
        
    }
}
