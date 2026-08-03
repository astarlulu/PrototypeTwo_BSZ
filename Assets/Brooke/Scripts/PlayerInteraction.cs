using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float Range = 0.5f;
    [SerializeField] private LayerMask LayerMask;

    [SerializeField] private TMP_Text InteractionText;

    // sienna for animation
    [SerializeField] private Animator anim;

    //private Camera mainCamera;

    //private void Start()
    //{
    //    mainCamera = GetComponent<Camera>();
    //}

    private void Update()
    {
        HandleRaycastting();
    }

    private void HandleRaycastting()
    {
        //instead of two differnt raycast 
        Collider2D hitHidingSpot = Physics2D.OverlapCircle(transform.position, Range, LayerMask);
        //RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, Range, LayerMask);
        //RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, Range, LayerMask);

        if(hitHidingSpot == null || !hitHidingSpot.TryGetComponent(out IInteractionTrigger interaction))
        {
            InteractionText.text = "";
            anim.SetBool("Interact", false);
            return;
        }
        else
        {
            anim.SetBool("Interact", true);
        }

        InteractionText.text = interaction.InteractionText;

        if (Input.GetKeyDown(KeyCode.E))
            interaction.Interact();

    }
}
