using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patternController : MonoBehaviour
{
    public float maxX;
    Transform lastChild;
    // Start is called before the first frame update
    void Start()
    {
        lastChild = transform.GetChild(transform.childCount - 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(levelManager.instance.isPaused)
            return;
        
        // if the last child passes the end the gameobject disables itself        
        if(lastChild.position.x < maxX)
        {
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        lastChild = transform.GetChild(transform.childCount - 1);
    }
}
