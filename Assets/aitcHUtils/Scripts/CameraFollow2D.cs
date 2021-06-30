using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public bool allowFollow = true;
    public GameObject target;
    public Transform baseParent;
    [Range(0f, 100f)][SerializeField] float smoothingModifier;
    [SerializeField] float maxDistanceLimit;

    bool followOnStart = true;
    Vector3 refVelocity = Vector3.zero;

    //private void Start()
    //{
    //    if (photonView.IsMine)
    //    {
    //        _cameraWork.OnStartFollowing();
    //    }
    //}

    // Update is called once per frame
    void LateUpdate()
    {
        if (allowFollow && followOnStart && target != null)
        //{
        //    transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.transform.position.x,
        //                                                                            target.transform.position.y,
        //                                                                            transform.position.z),
        //                                                                            ref refVelocity, smoothingModifier);

        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z), smoothingModifier * Time.deltaTime);
        }

        //if (target.GetComponent<PlayerMovement>().isMoving)
        //{
        //    Debug.Log(Mathf.Abs(new Vector2(transform.position.x, transform.position.y).sqrMagnitude - new Vector2(target.transform.position.x, target.transform.position.y).sqrMagnitude));
        //    if (Mathf.Abs(transform.position.x - target.transform.position.x) >= maxDistanceLimit)
        //    {
        //        //transform.parent = target.transform;
        //        //allowFollow = false;
        //    }
        //}
        //else
        //{
        //    //transform.parent = baseParent;
        //    //allowFollow = true;
        //}
    }

    //public void AllowFollowOnStart(bool status) 
    //{
    //    followOnStart = status;
    //}


}
