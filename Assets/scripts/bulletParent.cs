using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletParent : MonoBehaviour
{
    void OnEnable()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
