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

    private int maxTime = 0; // ���������� ��� �������� ������������� �������
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

        // ��������� ������������ ����� �� PlayerPrefs
        LoadMaxTime();

        // �������� ������� ����� ��� ������
        startTime = DateTime.Now;
        lastTime = DateTime.Now; // �������������� lastTime ������� ��������

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
        lastTime = DateTime.Now; // ��������� lastTime ��� ������� �������
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
        Debug.Log("������ �������� ���� ������!");

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
        Debug.Log("������������ ����� ���������: " + maxTime);
    }

    private void LoadMaxTime()
    {
        maxTime = PlayerPrefs.GetInt("MaxTime", 0);
        Debug.Log("������������ ����� ���������: " + maxTime);
    }
}