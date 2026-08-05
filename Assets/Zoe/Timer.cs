using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] public float timeRemaining;
    [SerializeField] int decimals;
    
    public static event Action OnTimerFinished;

    bool timerFinished = false;
    

    void Update()
    {
        if (timerFinished)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerFinished = true;

            OnTimerFinished?.Invoke();
        }

        UpdateText();
    }

    void UpdateText()
    {
        text.text = timeRemaining.ToString($"F{decimals}");
    }
}