using UnityEngine;

public class TableInteractable : MonoBehaviour, IInteractionTrigger
{
    [SerializeField] private string HideText = "[E] to hide";
    [SerializeField] private string LeaveText = "[E] to leave";
    //[SerializeField] private Animator Animator;

    private bool isHiding;

    //returns the string
    public string InteractionText
    {
        get
        {
            return isHiding ? LeaveText : HideText;
        }
    }

    public void Interact()
    {
        isHiding = !isHiding;
        //Animator.SetBool("isHiding", isHiding);
        Debug.Log("Hiding under table");
    }
}
