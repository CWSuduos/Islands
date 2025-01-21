using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Timer : MonoBehaviour
{
    public float duration = 180f;
    public Text timerText;
    public Text elapsedTimeText;
    public Text delayText;
    private MaxElapsedTimeTracker maxElapsedTimeTracker;
    public bool showCurrentAndElapsedTime = true;

    private float timeLeft;
    private bool isTimerRunning = false;
    private LvlWinManager lvlWinManager;

    void Start()
    {
        timeLeft = duration;
        StartTimer();

        maxElapsedTimeTracker = FindObjectOfType<MaxElapsedTimeTracker>();
        if (maxElapsedTimeTracker != null)
        {
            maxElapsedTimeTracker.SetMaxElapsedTime(PlayerPrefs.GetFloat("MaxElapsedTime", 0f));
            Debug.Log("Loaded Max Elapsed Time: " + maxElapsedTimeTracker.MaxElapsedTime);
        }
        else
        {
            Debug.LogError("MaxElapsedTimeTracker not found in the scene!");
        }

        lvlWinManager = FindObjectOfType<LvlWinManager>();
        if (lvlWinManager == null)
        {
            Debug.LogError("LvlWinManager не найден на сцене!");
        }
    }

    public void StartTimer()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            StartCoroutine(CountdownTimer());
        }
    }

    public void StopTimer()
    {
        if (isTimerRunning)
        {
            isTimerRunning = false;
            StopAllCoroutines();

            float currentTimeElapsed = duration - timeLeft;

            if (showCurrentAndElapsedTime)
            {
                UpdateElapsedTimeText(currentTimeElapsed);
            }

            if (maxElapsedTimeTracker != null)
            {
                maxElapsedTimeTracker.CheckAndUpdateMaxElapsedTime(currentTimeElapsed);
            }
            else
            {
                Debug.Log("MaxElapsedTimeTracker not found in the scene!");
            }


        }
    }
   
    public void UpdateElapsedTimeText(float elapsedTime)
    {
        if (elapsedTimeText != null)
        {
            elapsedTimeText.text = Mathf.RoundToInt(elapsedTime).ToString();
            delayText.text = elapsedTimeText.text;
        }
    }

    private IEnumerator CountdownTimer()
    {
        while (timeLeft > 0 && isTimerRunning)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        if (isTimerRunning)
        {
            if (timerText != null) timerText.text = "00:00";
            Debug.Log("Time's up!");
            isTimerRunning = false;

            if (lvlWinManager != null)
            {
                lvlWinManager.SetLosePanel();
            }
            StopTimer();
        }
    }

    private void Update()
    {
        if (showCurrentAndElapsedTime && isTimerRunning)
        {
            int minutes = Mathf.FloorToInt(timeLeft / 60);
            int seconds = Mathf.FloorToInt(timeLeft % 60);
            if (timerText != null)
            {
                timerText.text = $"{minutes:D2}:{seconds:D2}";
            }
        }
    }
}