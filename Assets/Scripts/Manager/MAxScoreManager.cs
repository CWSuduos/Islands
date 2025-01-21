using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MAxScoreManager : MonoBehaviour
{
    public static MAxScoreManager Instance { get; private set; }

    public Text maxPointsText;
    public Text totalMaxTimes;

    private int maxPoints; // Максимальные очки среди всех уровней
    private float maxTimeSurvived;

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

        // Загрузка сохраненных данных
        LoadData();
    }

    private void Update()
    {
        // (Этот метод Update у вас пустой, но, возможно, он понадобится вам в будущем)
    }

    public void AddPoints(int levelId, int points, float timeSpent, int stars)
    {
        // Сравниваем текущие очки с текущим максимумом
        if (points > maxPoints)
        {
            maxPoints = points;
            PlayerPrefs.SetInt("MaxPoints", maxPoints);
            UpdateMaxPointsUI(maxPoints); // Обновляем UI
        }

        Debug.Log("Набранные очки: " + points);

        // Сохраняем время прохождения и количество звёзд для уровня (если нужно)
        PlayerPrefs.SetFloat("TimeLevel" + levelId, timeSpent);
        PlayerPrefs.SetInt("StarsLevel" + levelId, stars);

        // Сохраняем данные
        PlayerPrefs.Save();
    }

    public void UpdateTimeSurvived(float timeSurvived)
    {
        if (timeSurvived > maxTimeSurvived)
        {
            maxTimeSurvived = timeSurvived;
            PlayerPrefs.SetFloat("MaxTimeSurvived", maxTimeSurvived);
            // Обновить UI с максимальным временем (если нужно)
            UpdateTotalMaxTimeUI(maxTimeSurvived);
        }
    }

    private void UpdateMaxPointsUI(int maxPoints)
    {
        // Обновление UI с максимальными очками
        Debug.Log("Максимальные очки: " + maxPoints);
        maxPointsText.text = "" + maxPoints.ToString();
    }

    private void UpdateTotalMaxTimeUI(float maxTime)
    {
        // Обновление UI с максимальным временем выживания
        totalMaxTimes.text = "Max Time: " + maxTime.ToString("F2");
    }

    private void LoadData()
    {
        maxPoints = PlayerPrefs.GetInt("MaxPoints", 0);
        maxTimeSurvived = PlayerPrefs.GetFloat("MaxTimeSurvived", 0f);

        // Обновляем UI при загрузке данных
        UpdateMaxPointsUI(maxPoints);
        UpdateTotalMaxTimeUI(maxTimeSurvived);

        // Вывод в консоль загруженных данных для отладки
        Debug.Log("Загружено максимальное количество очков: " + maxPoints);
        Debug.Log("Загружено максимальное время выживания: " + maxTimeSurvived);
    }
}