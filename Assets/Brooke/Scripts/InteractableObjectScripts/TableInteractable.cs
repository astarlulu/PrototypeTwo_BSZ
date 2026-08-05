using UnityEngine;

public class TableInteractable : MonoBehaviour, IInteractionTrigger
{
    [SerializeField] private string HideText = "[E] to hide";
    [SerializeField] private string LeaveText = "[E] to leave";
    //[SerializeField] private Animator Animator;

    //private bool isHiding;
    [SerializeField] private HidingSpot hidingSpot;

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
        hidingSpot.isHiding = !hidingSpot.isHiding;
        //Animator.SetBool("isHiding", isHiding);
        Debug.Log("Hiding under table");
    }
}
