using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSortingObject : MonoBehaviour
{
    public void SetZSortingOrder(Vector3 sortedPos) 
    {
        transform.position = sortedPos;
    }
}
