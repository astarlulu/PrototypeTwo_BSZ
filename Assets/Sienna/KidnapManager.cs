using UnityEngine;
using UnityEngine.Playables;

public class KidnapManager : MonoBehaviour
{
    public bool baraIsKidnapped;
    [SerializeField] private GameObject bara;

    [SerializeField] private PlayableDirector cutsceneDirector;
    private bool hasPlayed = false;

    public Transform transformSpot;

    public void KidnapBara()
    {
        baraIsKidnapped = true;
        // bara.transform.position = transformSpot.position;
        bara.SetActive(false);
        Debug.Log("kidnap");
        StartCutscene();
    }
    
    public void ReleaseBara()
    {
        baraIsKidnapped = false;
         bara.SetActive(true);
        Debug.Log("release");
    }

    private void StartCutscene()
    {
        if (cutsceneDirector != null)
        {
            cutsceneDirector.Play();
            cutsceneDirector.stopped += OnCutsceneEnded;
        }
    }

    private void OnCutsceneEnded(PlayableDirector director)
    {
        cutsceneDirector.stopped -= OnCutsceneEnded;
        Destroy(gameObject);
    }
}
