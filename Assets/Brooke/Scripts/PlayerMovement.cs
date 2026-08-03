using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float jumpForce = 8;
    [SerializeField] public float playerSpeed = 2f;

    private float input;
    [SerializeField] private Rigidbody2D rb;

    public GameObject groundRayObject;
    public GameObject aboveRayObject;
    bool jumpOn;

    [SerializeField] private LayerMask groundLayer;
    void Start()
    {
        jumpOn = false;
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //jumping
        if (Input.GetKeyDown(KeyCode.Space)) //is grounded so that player can only jump again once theyve hit the ground
        {
            Debug.Log("Jump");
            Jump();
        }

    }

    void FixedUpdate()
    {
        input = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(input * playerSpeed, rb.linearVelocity.y);
        IsGrounded();
        IsAbove();

    }

    public void StartGame() 
    {
        rb.gravityScale = 1; //setting gravity
        rb.linearVelocity = Vector3.zero;
    }

    void Jump()
    {
        if(!jumpOn)
        {
            return;
        }

        jumpOn = false;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }

    //stop double jumping, checking if touched the ground again so that player can jump again
    private void IsGrounded()
    {
        //check for ground or interactable so no double jumping on anything
        RaycastHit2D hitGround = Physics2D.Raycast(groundRayObject.transform.position, Vector2.down, 0.5f,groundLayer);
        //Debug.DrawRay(groundRayObject.transform.position, Vector2.down * 0.5f, Color.red); //draw ray not needed rn

        if (hitGround.collider !=null)
        {
                //Debug.Log("Hit " + hitGround.collider.name);
                jumpOn = true;
            }
            else
            {
                //Debug.Log("Nothing hit");
                jumpOn = false;
            }
        
    }
    private void IsAbove()
    {
        RaycastHit2D hitAbove = Physics2D.Raycast(aboveRayObject.transform.position, Vector2.up, 0.5f, groundLayer);
        Debug.DrawRay(aboveRayObject.transform.position, Vector2.up * 0.5f, Color.red); //draw ray not needed rn

        if (hitAbove.collider != null)
        {
            //go trhogh the ground collider if the above collider hits it
            //if (hitAbove.collider LayerMask)
            //{

            //}

        }

    }

    //time for how long its turned off for
    private IEnumerator ResetLayerMaskRoutine()
    {
        // Turn off interaction (disable collider temporarily)
        //myCollider.enabled = false;

        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Turn back on
        //myCollider.enabled = true;
    }
}
