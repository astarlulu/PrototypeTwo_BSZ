using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text; 
    [SerializeField] public float timeRemaining; 
    [SerializeField] int decimals; 

    public static event Action OnTimerFinished;

    private bool timerFinished = false;

    public RoomManager roomManager; 

    void Update()
    {
        if (timerFinished) return; //has timer reach 0? if so stop update

        timeRemaining -= Time.deltaTime; //timer countdown

        
        if (timeRemaining <= 0) //has timer reach 0
        {
            timeRemaining = 0; //cannot go below 0
            timerFinished = true; 

           
            roomManager.TimerFinished();

            
            OnTimerFinished?.Invoke(); //send timer finished to listening scripts
        }

        UpdateText();
    }

    public void SetTimer(float newTime)
    {
        
        timeRemaining = newTime; 

        timerFinished = false; //allows timer to count again

        UpdateText();
    }

    void UpdateText()
    {
        text.text = timeRemaining.ToString($"F{decimals}");
    }
}