using UnityEngine;
using System.Collections.Generic;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private int delayFrames = 15;

    //offseting so that the follower/gumi and nobebe arent inside/behind the player
    [SerializeField] private Vector3 offset;

    private Queue<Vector3> positionHistory = new Queue<Vector3>();

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

        //flipping them too with the player
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

    }

}
