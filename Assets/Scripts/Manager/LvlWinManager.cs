using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LvlWinManager : MonoBehaviour
{

    private int currentStars;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public Sprite starSprite;
    public Sprite greyStarSprite;
    public int pointsForLevel;
    public Text pointsText;
    public Text pointsTextLose;
    public Text pointsTextSecond;
    public GameObject winPanel;
    public GameObject LosePanel;
    private LevelCharacteristic levelCharacteristic;
    private TimerManager timerManager;
    [SerializeField] private Text timerTextSecond;
    [SerializeField] private Text timerTextLose;
    public GameObject gamePanel;
    private int currentLevelId;
    public int levelID;
   

        
    
    private void Start()
    {
        
        levelCharacteristic = GameObject.FindObjectOfType<LevelCharacteristic>();
        timerManager = GameObject.FindObjectOfType<TimerManager>();
        winPanel.SetActive(false);
    }
   
    private void HandleLevelSelected(int levelId)
    {
        currentLevelId = levelId;
        Debug.Log("В YourOtherClass получен ID уровня: " + currentLevelId);
        
    }
    public void SetLosePanel()
    {
        LosePanel.SetActive(true);
        gamePanel.SetActive(false);
        Debug.Log("Уровень проигран!");
        Timer timer = new Timer();
        timer.StopTimer();
        SetLevelCompletedById(levelID);
        gamePanel.SetActive(false);
        winPanel.SetActive(true);
         // Расчет времени

        ;
           
       
        
        MAxScoreManager.Instance.UpdateTimeSurvived(0);
    }
    private void UpdateStars(int stars)
    {
        star1.GetComponent<SpriteRenderer>().sprite = starSprite;
        star2.GetComponent<SpriteRenderer>().sprite = starSprite;
        star3.GetComponent<SpriteRenderer>().sprite = starSprite;

        if (stars < 3)
        {
            star3.GetComponent<SpriteRenderer>().sprite = greyStarSprite;
        }

        if (stars < 2)
        {
            star2.GetComponent<SpriteRenderer>().sprite = greyStarSprite;
        }

        if (stars < 1)
        {
            star1.GetComponent<SpriteRenderer>().sprite = greyStarSprite;
        }
    }
    public void SetLevelCompletedById(int levelId)
    {
        LevelCharacteristic[] allLevelCharacteristics = FindObjectsOfType<LevelCharacteristic>();
        foreach (LevelCharacteristic lc in allLevelCharacteristics)
        {
            if (lc.LevelID == levelId+1)
            {
                lc.IsCompleted = true;
                return;
            }
        }
        Debug.LogWarning("LevelCharacteristic с ID " + levelId + " не найден!");
    }


    public void WinLevel()
    {
        Debug.Log("Уровень пройден!");

        // Показываем общее количество очков
        pointsText.text = "" + pointsForLevel.ToString();

        SetLevelCompletedById(levelID);
        gamePanel.SetActive(false);
        winPanel.SetActive(true);

        int stars = CalculateStars();

        
        float timeSpent = timerManager.GetElapsedTime();

       
        MAxScoreManager.Instance.AddPoints(levelID, pointsForLevel, timeSpent, stars);
    }
    public StarCollector StarCollector;
    public void AddStar()
    {
        currentStars++;
        StarCollector.AddStar();
        Debug.Log("Звезд собрано: " + currentStars); // Выводим текущее количество звезд

        // Здесь можно добавить обновление UI, если нужно отображать количество звезд в реальном времени
    }
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public string FormatTimeToScore(float time)
    {
        int Score = Mathf.FloorToInt(time / 1);
        return string.Format("{0}", Score);
    }
    private int CalculateStars()
    {
        float timeRemaining = timerManager.timeRemaining;
        float totalTime = timerManager.minutes * 60 + timerManager.seconds;

        if (timeRemaining <= 0)
        {
            return 0;
        }
        else if (timeRemaining / totalTime > 0.9f)
        {
            return 3;
        }
        else if (timeRemaining / totalTime > 0.6f)
        {
            return 2;
        }
        else if (timeRemaining / totalTime > 0.3f)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}