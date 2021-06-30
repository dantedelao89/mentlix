using aitcHUtils.TwoDimesional;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimationController : MonoBehaviour
{
    [SerializeField] string MovementAnimationParameter;

    private Vector3 previousPos;
    private Vector3 currentPos;
    private float velocity;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        previousPos = currentPos;
    }

    private void LateUpdate()
    {
        currentPos = transform.position;
        velocity = Vectors.GetSquaredVelocity(previousPos, currentPos);
        animator.SetFloat(MovementAnimationParameter, velocity);
    }
}
