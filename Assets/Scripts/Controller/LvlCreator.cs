using UnityEngine;
using UnityEngine.UI;


public class LvlCreator : MonoBehaviour
{
    [SerializeField] private GameObject panelPrefab;
    [SerializeField] private Transform parentForPanel;
    private int currentLevelID;
    public void CreatePanel(int levelID)
    {
        if (panelPrefab != null && parentForPanel != null)
        {
            currentLevelID = levelID;
            GameObject panelInstance = Instantiate(panelPrefab, parentForPanel);
            AssignLevelIDText(panelInstance, levelID);
        }
        else
        {
            Debug.LogWarning("[LvlCreator] panelPrefab ��� parentForPanel �� ���������!");
        }
    }

    private void AssignLevelIDText(GameObject panel, int levelID)
    {
        Text textUI = panel.GetComponentInChildren<Text>();
        if (textUI != null)
        {
            
            textUI.text = $"Level {levelID}";
            return;
        }

        Debug.LogWarning("[LvlCreator] �� ������ Text � ������!");
    }
    public int GetCurrentLevelID()
    {
        return currentLevelID;
    }
}