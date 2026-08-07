using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Controls")]
    [SerializeField] private float jumpForce = 8;
    [SerializeField] public float playerSpeed = 2f;

    private float input;
    [Header("Player References")]
    [SerializeField] private Rigidbody2D rb;
    //gets sprite renderer for flipping on animation sprite not player
    [SerializeField] private SpriteRenderer spriteRenderer;

    public GameObject groundRayObject;
    public GameObject aboveRayObject;
    bool jumpOn;

    // sienna for animation
    [SerializeField] private Animator anim;
    private int facingDirection = 1; // 1 for right, -1 for left

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Collider2D playerCollider;

    private bool passingThrough = false;

    void Start()
    {
        jumpOn = false;
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();

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

        if (rb.linearVelocity.x != 0 && jumpOn)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    void FixedUpdate()
    {
        input = Input.GetAxis("Horizontal");

        if(input > .1f && facingDirection < 0 || input < -.1f && facingDirection > 0)
        {
            Flip();
        }

        rb.linearVelocity = new Vector2(input * playerSpeed, rb.linearVelocity.y);

        IsGrounded();
        IsAbove();

    }
    void Flip()
    {
        facingDirection *= -1;
        spriteRenderer.flipX = facingDirection < 0;
        //facingDirection *= -1;
        //Vector3 scale = transform.localScale;
        //scale.x *= -1;
        //transform.localScale = scale;
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

        anim.SetTrigger("Jump");
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
        if (passingThrough)
            return;

        RaycastHit2D hitAbove = Physics2D.Raycast(aboveRayObject.transform.position, Vector2.up, 0.5f, groundLayer);
        //Debug.DrawRay(aboveRayObject.transform.position, Vector2.up * 0.5f, Color.red);

        if (hitAbove.collider != null)
        {
            StartCoroutine(ResetLayerMaskRoutine(hitAbove.collider));
        }

    }

    //restting the collider back after finish passing though that colldier
    private IEnumerator ResetLayerMaskRoutine(Collider2D platformCollider)
    {
        passingThrough = true;

        // ignoring collision between player and this platform
        Physics2D.IgnoreCollision(playerCollider, platformCollider, true);

        // wait long enough for player to pass through
        yield return new WaitForSeconds(0.25f);

        // turn collision back on
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);

        passingThrough = false;
    }
}
