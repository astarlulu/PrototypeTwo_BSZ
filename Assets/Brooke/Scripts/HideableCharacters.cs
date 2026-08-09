using UnityEngine;

public class HideableCharacters : MonoBehaviour
{
    //attach to every character and give nobebe 1, gumi 2 and player 3 
    //thats the order they disapare in counts down when hiding chraacter and up when unhiding character
    [SerializeField] private float hideOrder;

    //steps
    public float HideOrder => hideOrder;

    public Rigidbody2D rb;
    private PlayerFollower follower;
    private Animator anim;
    [SerializeField] private SpriteRenderer sprite;

    public bool IsHidden { get; private set; }

    //making sure characetrs follow player
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        follower = GetComponent<PlayerFollower>();
        anim = GetComponent<Animator>();
    }

    public void Hide(Transform hidingSpot)
    {
        IsHidden = true;

        transform.position = hidingSpot.position;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        if (follower != null)
            follower.enabled = false;

        if(anim != null)
            anim.enabled = false;
        sprite.color = Color.grey;
    }

    public void UnHide()
    {
        IsHidden = false;

        if (rb != null)
            rb.simulated = true;

        if (follower != null)
            follower.enabled = true;
       
        if (anim != null)
            anim.enabled = true;
        sprite.color = Color.white;
    }
}
