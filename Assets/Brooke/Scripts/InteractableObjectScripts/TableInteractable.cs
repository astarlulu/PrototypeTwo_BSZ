using UnityEngine;

public class TableInteractable : MonoBehaviour, IInteractionTrigger
{

    [SerializeField] private string InteractText = "[E] to hide";


    public void Interact()
    {
        Debug.Log("Hiding under table");
    }

    //returns the string
    public string InteractionText { get { return InteractText; } }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
