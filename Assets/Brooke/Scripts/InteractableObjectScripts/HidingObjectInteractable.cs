// using UnityEditor.EditorTools;
using UnityEngine;

public class HidingObjectInteractable : MonoBehaviour, IInteractionTrigger
{
    [SerializeField] private string HideText = "[E] to hide";
    [SerializeField] private string LeaveText = "[E] to leave";
    //[SerializeField] private Animator Animator;

    [SerializeField] private HidingSpot hidingSpot;
    [SerializeField] private HidingManager hidingManager;
    //private bool isHiding;


    // sienna
    private RoomManager roomManager;
    private bool noInteractText;

    [SerializeField] private SpriteRenderer sparkle;

    private void Awake()
    {
        roomManager = Object.FindAnyObjectByType<RoomManager>();
    }

    //returns the string
    public string InteractionText 
    { 
        get 
        {
            if (noInteractText)
                return "";

            return hidingSpot.isHiding ? LeaveText : HideText; 
        } 
    }

    public void Interact()
    {
        if (roomManager.enemyStarted)
        {
            noInteractText = true;
            return;
        }

        noInteractText = false;
            

        //hidingSpot.isHiding = !hidingSpot.isHiding;
        if (!hidingSpot.isHiding)
        {
            hidingSpot.isHiding = true;
            hidingManager.HideNext(hidingSpot);
            sparkle.enabled = false;
            Debug.Log("Hiding");
        }
        else
        {
            hidingSpot.isHiding = false;
            hidingManager.ReleaseLast(hidingSpot);
            sparkle.enabled = true;
            Debug.Log("Not hiding");
        }
        //Animator.SetBool("isHiding", isHiding);
    }

    private void UnhideAll()
    {
        hidingManager.ReleaseLast(hidingSpot); hidingManager.ReleaseLast(hidingSpot); // yu

        hidingManager.ReleaseLast(hidingSpot); hidingManager.ReleaseLast(hidingSpot); // meg

        hidingManager.ReleaseLast(hidingSpot); hidingManager.ReleaseLast(hidingSpot); // bara

    }
}
