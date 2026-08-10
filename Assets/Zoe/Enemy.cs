using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyView currentView;
    [SerializeField] public GameObject scanSprite;

    public bool firstEnemy;

    public KidnapManager kidnapManager;
    
    public enum EnemyView
    {
        Top,
        Middle,
        Bottom
    }

    public HidingSpot[] allHidingSpots;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        allHidingSpots = Object.FindObjectsByType<HidingSpot>(FindObjectsSortMode.None);
        scanSprite.SetActive(false);

        kidnapManager = GetComponent<KidnapManager>();
    }


    public void Scan()
    {
        
        foreach (HidingSpot hidingSpot in allHidingSpots)
        {
            if (hidingSpot.hidingSpotType == HidingSpot.HidingSpotType.Top && currentView == EnemyView.Top)
            {
                if (hidingSpot.isHiding)
                {
                    StartCoroutine(FoundYou());
                }
            }
            if (hidingSpot.hidingSpotType == HidingSpot.HidingSpotType.Middle && currentView == EnemyView.Middle)
            {
                if (hidingSpot.isHiding)
                {
                    StartCoroutine(FoundYou());
                }
            }
            if (hidingSpot.hidingSpotType == HidingSpot.HidingSpotType.Bottom && currentView == EnemyView.Bottom)
            {
                if (hidingSpot.isHiding)
                {
                    StartCoroutine(FoundYou());
                }
            }
        }
        if (allHidingSpots.All(hidingSpot => !hidingSpot.isHiding))
        {
            StartCoroutine(FoundYou());
        }
    }

    public IEnumerator FoundYou()
    {
        //if (firstEnemy == true)
        //{
        //    Debug.Log("PLAY CUTSCENE");
        //    kidnapManager.KidnapBara();
        //}
        //else
        //{
        //     Debug.Log("Found You!!");

        //    TriggerAllAnimation("Found");

        //    yield return new WaitForSeconds (3);
        //    SceneManager.LoadScene("GreyBoxing");
        //}
        Debug.Log("Found You!!");

        TriggerAllAnimation("Found");

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GreyBoxing");


    }
    public void TriggerAllAnimation(string triggerName)
    {
        Animator[] allAnimators = Object.FindObjectsByType<Animator>(FindObjectsSortMode.None);

        // Loop through each animator and fire the trigger
        foreach (Animator anim in allAnimators)
        {
            if (!anim.enabled)
                anim.enabled = true;

            anim.SetTrigger(triggerName);
        }
    }
}
