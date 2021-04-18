using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAtEnd : MonoBehaviour
{
    public float xMin;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < xMin)
        {
            gameObject.SetActive(false);
        }
    }
}
