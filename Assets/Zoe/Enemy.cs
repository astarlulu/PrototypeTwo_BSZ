using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyView currentView;
    [SerializeField] public GameObject scanSprite;
    
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
    }


    public void Scan()
    {
        
        foreach (HidingSpot hidingSpot in allHidingSpots)
        {
            if (hidingSpot.hidingSpotType == HidingSpot.HidingSpotType.Top && currentView == EnemyView.Top)
            {
                if (hidingSpot.isHiding)
                {
                    Debug.Log("Found You!! (top)");
                }
            }
            if (hidingSpot.hidingSpotType == HidingSpot.HidingSpotType.Middle && currentView == EnemyView.Middle)
            {
                if (hidingSpot.isHiding)
                {
                    Debug.Log("Found You!! (middle)");
                }
            }
            if (hidingSpot.hidingSpotType == HidingSpot.HidingSpotType.Bottom && currentView == EnemyView.Bottom)
            {
                if (hidingSpot.isHiding)
                {
                    Debug.Log("Found You!! (bottom)");
                }
            }
        }
        if (allHidingSpots.All(hidingSpot => !hidingSpot.isHiding))
        {
            Debug.Log("Found You!! (not hiding)");
        }
    }
}
