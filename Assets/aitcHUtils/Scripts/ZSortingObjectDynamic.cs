using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSortingObjectDynamic : MonoBehaviour
{
    ZSortingController sorter;

    // Start is called before the first frame update
    void Start()
    {
        sorter = GameObject.FindObjectOfType<ZSortingController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sorter != null) 
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, sorter.GetZFromY(transform.position.y));
        }
    }
}
