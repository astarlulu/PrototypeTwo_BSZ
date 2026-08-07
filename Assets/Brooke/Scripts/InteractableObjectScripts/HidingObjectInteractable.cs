using UnityEngine;

public class HidingObjectInteractable : MonoBehaviour, IInteractionTrigger
{
    [SerializeField] private string HideText = "[E] to hide";
    [SerializeField] private string LeaveText = "[E] to leave";
    //[SerializeField] private Animator Animator;

    [SerializeField] private HidingSpot hidingSpot;
    [SerializeField] private HidingManager hidingManager;
    //private bool isHiding;

    //returns the string
    public string InteractionText 
    { 
        get 
        { 
            return hidingSpot.isHiding ? LeaveText : HideText; 
        } 
    }

    public void Interact()
    {
        //hidingSpot.isHiding = !hidingSpot.isHiding;
        if (!hidingSpot.isHiding)
        {
            hidingSpot.isHiding = true;
            hidingManager.HideNext(hidingSpot);
            Debug.Log("Hiding");
        }
        else
        {
            hidingSpot.isHiding = false;
            hidingManager.ReleaseLast(hidingSpot);
            Debug.Log("Not hiding");
        }
        //Animator.SetBool("isHiding", isHiding);
    }
}
