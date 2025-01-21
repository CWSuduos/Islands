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

    private int maxPoints; // ������������ ���� ����� ���� �������
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

        // �������� ����������� ������
        LoadData();
    }

    private void Update()
    {
        // (���� ����� Update � ��� ������, ��, ��������, �� ����������� ��� � �������)
    }

    public void AddPoints(int levelId, int points, float timeSpent, int stars)
    {
        // ���������� ������� ���� � ������� ����������
        if (points > maxPoints)
        {
            maxPoints = points;
            PlayerPrefs.SetInt("MaxPoints", maxPoints);
            UpdateMaxPointsUI(maxPoints); // ��������� UI
        }

        Debug.Log("��������� ����: " + points);

        // ��������� ����� ����������� � ���������� ���� ��� ������ (���� �����)
        PlayerPrefs.SetFloat("TimeLevel" + levelId, timeSpent);
        PlayerPrefs.SetInt("StarsLevel" + levelId, stars);

        // ��������� ������
        PlayerPrefs.Save();
    }

    public void UpdateTimeSurvived(float timeSurvived)
    {
        if (timeSurvived > maxTimeSurvived)
        {
            maxTimeSurvived = timeSurvived;
            PlayerPrefs.SetFloat("MaxTimeSurvived", maxTimeSurvived);
            // �������� UI � ������������ �������� (���� �����)
            UpdateTotalMaxTimeUI(maxTimeSurvived);
        }
    }

    private void UpdateMaxPointsUI(int maxPoints)
    {
        // ���������� UI � ������������� ������
        Debug.Log("������������ ����: " + maxPoints);
        maxPointsText.text = "" + maxPoints.ToString();
    }

    private void UpdateTotalMaxTimeUI(float maxTime)
    {
        // ���������� UI � ������������ �������� ���������
        totalMaxTimes.text = "Max Time: " + maxTime.ToString("F2");
    }

    private void LoadData()
    {
        maxPoints = PlayerPrefs.GetInt("MaxPoints", 0);
        maxTimeSurvived = PlayerPrefs.GetFloat("MaxTimeSurvived", 0f);

        // ��������� UI ��� �������� ������
        UpdateMaxPointsUI(maxPoints);
        UpdateTotalMaxTimeUI(maxTimeSurvived);

        // ����� � ������� ����������� ������ ��� �������
        Debug.Log("��������� ������������ ���������� �����: " + maxPoints);
        Debug.Log("��������� ������������ ����� ���������: " + maxTimeSurvived);
    }
}