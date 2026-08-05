using System.Collections;

using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;



public class EnemyMovement : MonoBehaviour

{
    [SerializeField] private Enemy enemyScript;

    public GameObject PointA;

    public GameObject PointB;

    private Rigidbody2D rb;

    [SerializeField] private Animator anim; 

    private Transform currentPoint;

    public float speed;

    public Timer timer;



    bool stop = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created 

    void Start()

    {
        flip();

        rb = GetComponent<Rigidbody2D>();

        //anim = GetComponent<Animator>(); 

        currentPoint = PointB.transform;

        //anim.SetBool("Walking", true); 

    }



    // Update is called once per frame 



    void Update()

    {

        if (timer.timeRemaining != 0)
        {
            return;
        }
        if (!stop)
        {
            EnemyMove();
        }

        if (currentPoint.position == transform.position)

        {

            Debug.Log("PointA");

        }



    }



    public void EnemyMove()

    {

        Vector2 point = currentPoint.position - transform.position;

        if (currentPoint == PointB.transform)

        {

            rb.linearVelocity = new Vector2(speed, 0);

        }
        else
        {

            rb.linearVelocity = new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 3f && currentPoint == PointB.transform)

        {
            flip();
            currentPoint = PointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 3f && currentPoint == PointA.transform)

        {
            flip();

            StartCoroutine(stopAndWait(3f));

            currentPoint = PointB.transform;

        }
        anim.SetBool("Walking", true);
    }

    private void flip() //flip sprite when turning  

    {

        Vector3 localScale = transform.localScale;

        localScale.x *= -1;

        transform.localScale = localScale;

    }



    private void OnDrawGizmos() //visualise point a & b in the scene  

    {

        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);

        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);

        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);

    }



    private IEnumerator stopAndWait(float seconds)

    {

        anim.SetBool("Walking", false);

        stop = true;

        enemyScript.scanSprite.SetActive(true);
        enemyScript.Scan();

        yield return new WaitForSeconds(seconds);

        Debug.Log("WAITTT");
        enemyScript.scanSprite.SetActive(false);


        anim.SetBool("Walking", true);

        stop = false;

    }

}





 