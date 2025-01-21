using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerManager : MonoBehaviour
{
    [SerializeField] public Text timerText;
    [SerializeField] public int minutes = 1;
    [SerializeField] public int seconds = 0;
    [SerializeField] private GameObject losePanel;
    public Text MaxTimerText;
    public float timeRemaining;
    private bool isTimerRunning = false;

    private int maxTime = 0; // Переменная для хранения максимального времени
    private DateTime startTime;
    private DateTime lastTime;


    private void Start()
    {
        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }

        timeRemaining = minutes * 60 + seconds;
        UpdateTimerText();

        // Загружаем максимальное время из PlayerPrefs
        LoadMaxTime();

        // Получаем текущее время при старте
        startTime = DateTime.Now;
        lastTime = DateTime.Now; // Инициализируем lastTime текущим временем

        StartTimer();

    }

    private void Update()
    {
        if (isTimerRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isTimerRunning = false;
                OnTimerEnd();
            }
            UpdateTimerText();
        }


      

    }



    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        lastTime = DateTime.Now; // Обновляем lastTime при запуске таймера
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        SaveMaxTime();
    }

    public void ResetTimer()
    {
        timeRemaining = minutes * 60 + seconds;
        UpdateTimerText();

        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }
    }
    public float GetElapsedTime()
    {
        return (minutes * 60 + seconds) - timeRemaining;
    }
    public void OnTimerEnd()
    {
        Debug.Log("Таймер закончил весь отсчёт!");

        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
        SaveMaxTime();
    }


    private void OnApplicationQuit()
    {
    SaveMaxTime();
        
    }

    private void SaveMaxTime()
    {
        PlayerPrefs.SetFloat("MaxTime", maxTime);
        PlayerPrefs.Save();
        
        MaxTimerText.text = maxTime.ToString();
        Debug.Log("Максимальное время сохранено: " + maxTime);
    }

    private void LoadMaxTime()
    {
        maxTime = PlayerPrefs.GetInt("MaxTime", 0);
        Debug.Log("Максимальное время загружено: " + maxTime);
    }
}