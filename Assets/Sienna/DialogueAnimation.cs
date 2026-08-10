using TMPro;
using UnityEngine;

public class DialogueAnimation : MonoBehaviour
{
    [SerializeField] public string dialogue;
    [SerializeField] private TextMeshProUGUI textObject;

    private void Update()
    {
        textObject.text = dialogue;
    }
}
