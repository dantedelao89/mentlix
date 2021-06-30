using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSortingController : MonoBehaviour
{
    public enum AxisNature { Negative, Positive }

    [SerializeField]
    float AxisMin;
    [SerializeField]
    float AxisMax;
    [SerializeField]
    float RelativeAxisMin;
    [SerializeField]
    float RelativeAxisMax;
    [SerializeField]
    AxisNature ZAboveIndicator;

    [Header("Sort Objects")]
    [SerializeField]
    ZSortingObject[] ObjectsToSort;
    [SerializeField]
    bool AutoFindObjects;

    float _relativeAxisDif;
    float _mainAxisDif;

    // Start is called before the first frame update
    void Start()
    {
        _relativeAxisDif = RelativeAxisMax - RelativeAxisMin;
        _mainAxisDif = AxisMax - AxisMin;

        if (AutoFindObjects) 
        {
            ObjectsToSort = GameObject.FindObjectsOfType<ZSortingObject>();
        }

        foreach (var obj in ObjectsToSort)
        {
            Vector3 sortedPos = new Vector3(obj.transform.position.x, obj.transform.position.y, GetZFromY(obj.transform.position.y));
            obj.SetZSortingOrder(sortedPos);
        }
    }

    /// <summary>
    /// Get the sorting position on Z axis relative to Y
    /// </summary>
    /// <param name="Y">The y axis of the object</param>
    /// <returns>Z axis order relative to Y</returns>
    public float GetZFromY(float Y) 
    {
        float zVal;
        if (ZAboveIndicator == AxisNature.Negative)
        {
            zVal = Mathf.Clamp(AxisMin + ((Y - RelativeAxisMin) / (_relativeAxisDif / _mainAxisDif)), AxisMax, AxisMin);
        }
        else 
        {
            zVal = Mathf.Clamp(AxisMin + ((Y - RelativeAxisMin) / (_relativeAxisDif / _mainAxisDif)), AxisMin, AxisMax);
        }
        return zVal;
    }
}
