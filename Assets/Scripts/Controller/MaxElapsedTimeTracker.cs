using UnityEngine;
using UnityEngine.UI;

public class MaxElapsedTimeTracker : MonoBehaviour
{
    public float MaxElapsedTime { get; private set; } = 0f;
    public Text maxElapsedTimeText;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
        // Load the MaxElapsedTime value
        SetMaxElapsedTime(PlayerPrefs.GetFloat("MaxElapsedTime", 0f));
        Debug.Log("Loaded Max Elapsed Time: " + MaxElapsedTime);
        UpdateMaxElapsedTimeText();
    }

    void Update()
    {
        
    }

    private void UpdateMaxElapsedTimeText()
    {
        if (maxElapsedTimeText != null)
        {
            // Здесь изменено форматирование
            maxElapsedTimeText.text = Mathf.RoundToInt(MaxElapsedTime).ToString();
        }
    }

    public void UpdateMaxValue(float elapsedTime)
    {
        MaxElapsedTime = Mathf.Max(MaxElapsedTime, elapsedTime);
        UpdateMaxElapsedTimeText();
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("MaxElapsedTime", MaxElapsedTime);
        PlayerPrefs.Save();
        Debug.Log("Saved Max Elapsed Time: " + MaxElapsedTime);
    }

    public void CheckAndUpdateMaxElapsedTime(float elapsedTime)
    {
        UpdateMaxValue(elapsedTime);
    }

    public void SetMaxElapsedTime(float value)
    {
        MaxElapsedTime = value;
        UpdateMaxElapsedTimeText();
    }
}