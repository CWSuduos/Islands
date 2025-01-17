using UnityEngine;

public class LvlManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panelPrefabs;
    [SerializeField] private string parentTag; // ��� ��� ������ ������������� �������
    [SerializeField] private GameObject panelToHide; // ������, ������� ����� �������
    
    private Transform parentForPanel; // ������ �� ������ ��������
    private LvlCreator lvlCreator;

    private void Start()
    {
        lvlCreator = FindObjectOfType<LvlCreator>();

        if (lvlCreator == null)
        {
            Debug.LogWarning("[LvlManager] LvlCreator ��� �� �����!");
        }

        // ���� ������������ ������ �� ����
        GameObject parentObject = GameObject.FindGameObjectWithTag(parentTag);
        if (parentObject != null)
        {
            parentForPanel = parentObject.transform;
        }
        else
        {
            Debug.LogError($"[LvlManager] �� ������ ������ � ����� '{parentTag}'!");
        }
    }

    public void CreatePanel()
    {
        if (panelPrefabs != null && parentForPanel != null && lvlCreator != null)
        {
            int levelID = lvlCreator.GetCurrentLevelID();
            if (levelID >= 0 && levelID < panelPrefabs.Length)
            {
                GameObject panelInstance = Instantiate(panelPrefabs[levelID], parentForPanel);
            }
            else
            {
                Debug.LogWarning("[LvlManager] ��������� ID ������ ���!");
            }
        }
        else
        {
            Debug.LogWarning("[LvlManager] panelPrefabs ��� parentForPanel �� ���������, ��� LvlCreator �� ������!");
        }
    }

    public void OnButtonClick()
    {
        CreatePanel();
    }

    // ����� ����� ��� ������� ��������� ������
    public void HideSpecifiedPanel()
    {
        if (panelToHide != null)
        {
            Destroy(panelToHide); // ���������� ��������� ������
            panelToHide = null;   // �������� ������, ����� �������� ���������� ������
        }
        else
        {
            Debug.LogWarning("[LvlManager] ������ ��� ������� �� ��������� ��� ��� �������!");
        }
    }
}