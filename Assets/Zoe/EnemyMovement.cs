using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Enemy enemyScript;

    public GameObject PointA; // scan point
    public GameObject PointB; // enemy start/stop point

    private Rigidbody2D rb;

    [SerializeField] private Animator anim;

    private Transform currentPoint;

    public float speed;

    public Timer timer;

    public RoomManager roomManager;

    bool stop = false;

    private bool roomFinished = false; // stops EnemyFinished from being called multiple times
    private bool hasScanned = false; 

    void Start()
    {
        flip(); //enemy face right way for animation

        rb = GetComponent<Rigidbody2D>();
        currentPoint = PointB.transform; //start at point b
    }

    void Update()
    {
        if (timer.timeRemaining != 0) //has timer NOT reached 0??
        {
            return; //don't move if not 0
        }

        if (!stop) //if enemy isnt stopped, move
        {
            EnemyMove();
        }
    }

    public void EnemyMove()
    {
        Vector2 point = currentPoint.position - transform.position;

        if (currentPoint.position.x > transform.position.x) // If the target is to the RIGHT of the enemy
        {
            rb.linearVelocity = new Vector2(speed, 0); // Move right
        }
        else // If the target is to the LEFT of the enemy
        {
            rb.linearVelocity = new Vector2(-speed, 0); // Move left
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 4f && currentPoint == PointB.transform) 
        {
            flip();

            
            if (hasScanned && !roomFinished) //only finish the room if the enemy has already scanned
            {
                roomFinished = true;

                
                roomManager.EnemyFinished(); //tell RoomManager the enemy has finished
            }

            currentPoint = PointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 4f && currentPoint == PointA.transform)
        {
            flip();

            hasScanned = true; //enemy has reached the scan point

            StartCoroutine(stopAndWait(3f));//stand still for 3 secs

            currentPoint = PointB.transform; //go to point b (irrelevant ??)
        }

        anim.SetBool("Walking", true);
    }

    private void flip() // flip sprite when turning
    {
        Vector3 localScale = transform.localScale;

        localScale.x *= -1;

        transform.localScale = localScale;
    }

    private void OnDrawGizmos() // visualise point A & B in the scene
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);

        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);

        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
    }

    private IEnumerator stopAndWait(float seconds) 
    {
        anim.SetBool("Walking", false);

        stop = true;

        enemyScript.scanSprite.SetActive(true); //turn on scan drawing

        enemyScript.Scan(); //call scan ability from Enemy.cs

        yield return new WaitForSeconds(seconds);//wait 3 secs

        Debug.Log("WAITTT");

        enemyScript.scanSprite.SetActive(false);

        anim.SetBool("Walking", true);

        stop = false; //enemy can move again
    }

    public void ResetRoom()
    {
        roomFinished = false; //reset finished state
        hasScanned = false; //reset scanned state
    }
}