using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float jumpForce = 8;
    [SerializeField] public float playerSpeed = 2f;

    private float input;
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //move left or right
        MovePlayer();

        //jumping
        if (Input.GetKeyDown(KeyCode.Space)) //is grounded so that player can only jump again once theyve hit the ground
        {
            Debug.Log("Jump");
            Jump();

        }

    }

    public void MovePlayer()
    {
        input = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(input * playerSpeed, rb.linearVelocity.y);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Pipe"))
    //    {
    //        Debug.Log("Player Hit");
    //    }

    //    if (collision.CompareTag("Goal"))
    //    {
    //        Debug.Log("Yippeee");
    //    }

    //}

    public void StartGame() 
    {
        rb.gravityScale = 1; //setting gravity
        rb.linearVelocity = Vector3.zero;
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    //stop double jumping
    //private bool GetIsGrounded()
    //{
    //    //check for ground or interactable so no double jumping on anything
    //    bool grounded = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, LayerMask.GetMask("Ground") | LayerMask.GetMask("Interactable"));
        
    //    return grounded;
    
    //}
}
