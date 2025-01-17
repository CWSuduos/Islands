using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] public Text timerText;
    [SerializeField] public int minutes = 1;
    [SerializeField] public int seconds = 0;
    [SerializeField] private GameObject losePanel; // Ссылка на панель проигрыша

    public float timeRemaining;
    private bool isTimerRunning = false;

    private void Start()
    {
        // Убедимся, что панель проигрыша изначально не видна
        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }

        timeRemaining = minutes * 60 + seconds;
        UpdateTimerText();
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
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void ResetTimer()
    {
        timeRemaining = minutes * 60 + seconds;
        UpdateTimerText();

        // Скрываем панель проигрыша при сбросе таймера
        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }
    }

    public void ForceStopTimer()
    {
        isTimerRunning = false;
        Debug.Log("Таймер был принудительно остановлен.");
    }

    public void OnTimerEnd()
    {
        Debug.Log("Таймер закончил весь отсчёт!");

        // Показываем панель проигрыша, если она существует
        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
    }
}