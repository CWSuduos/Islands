using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MAxScoreManager : MonoBehaviour
{
    public static MAxScoreManager Instance { get; private set; }

    public Text maxPointsText;
 
    public Text totalMaxTimes; // Предполагаю, это текст для отображения максимального времени

    private int totalMaxPoints;
    private float maxTimeSurvived;
    private void Update()
    {
        

    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        totalMaxPoints = PlayerPrefs.GetInt("TotalMaxPoints", 0);
        maxTimeSurvived = PlayerPrefs.GetFloat("MaxTimeSurvived", 0f);

        UpdateTotalMaxPointsUI();
        UpdateMaxTimeSurvivedUI();
    }

    public void AddPoints(int levelId, int points, int maxPointsForLevel)
    {
        int previousMaxPoints = PlayerPrefs.GetInt("MaxPointsLevel" + levelId, 0);
        if (points > previousMaxPoints)
        {
            PlayerPrefs.SetInt("MaxPointsLevel" + levelId, points);
        }
        maxPointsText.text = PlayerPrefs.GetInt("MaxPointsLevel" + levelId, 0).ToString();

        totalMaxPoints += points;
        PlayerPrefs.SetInt("TotalMaxPoints", totalMaxPoints);
        UpdateTotalMaxPointsUI();
    }

    public void UpdateTimeSurvived(float timeSurvived)
    {
        if (timeSurvived > maxTimeSurvived)
        {
            maxTimeSurvived = timeSurvived;
            PlayerPrefs.SetFloat("MaxTimeSurvived", maxTimeSurvived);
            UpdateMaxTimeSurvivedUI();
        }
    }


    private void UpdateTotalMaxPointsUI()
    {
        
    }

    private void UpdateMaxTimeSurvivedUI()
    {
        TimeSpan time = TimeSpan.FromSeconds(maxTimeSurvived);
        string formattedTime = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
        totalMaxTimes.text = formattedTime; // Обновляем totalMaxTimes
    }
}