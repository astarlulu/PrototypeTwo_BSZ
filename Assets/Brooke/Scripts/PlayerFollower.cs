using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private int delayFrames = 15;

    //offseting so that the follower/gumi and nobebe arent inside/behind the player
    [SerializeField] private Vector3 offset;

    [SerializeField] private Animator anim;
    private Vector3 lastPosition;
    public bool isMoving;
    private float movementThreshold = 0.001f; 

    private Queue<Vector3> positionHistory = new Queue<Vector3>();

    void Start()
    {
        lastPosition = transform.position;
    }

    void LateUpdate()
    {
        //storing the player's current position to follow later
        positionHistory.Enqueue(target.position);

        //once enough positions are stored move to the oldest one in queue
        if (positionHistory.Count > delayFrames)
        {
            Vector3 targetPos = positionHistory.Dequeue() + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);
        }
       

        // flipping them too with the player

        //Vector3 scale = transform.localScale;
        //scale.x *= -1;
        //transform.localScale = scale;

        // for move animation
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        if (distanceMoved > movementThreshold)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
        lastPosition = transform.position;
    }

    public void FlipFollower()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}
