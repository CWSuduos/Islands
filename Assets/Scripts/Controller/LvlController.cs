using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
           

public class LvlController : MonoBehaviour
{
    [Header("������� ��� ������")]
   
    public GameObject prefabB;

    [Header("����� ������ (RectTransform)")]
    public List<RectTransform> spawnPoints;

    [Header("������������ ������ ��� ��������� ������")]
    public Transform parent;

    void Start()
    {
       
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            RectTransform point = spawnPoints[i];
            if (point != null)
            {
                int currentID = i + 1;
                
                SpawnAtPoint(prefabB, point, currentID);
            }
            else
            {
                Debug.LogWarning($"����� ������ � �������� {i} �� ���������!");
            }
        }
    }
    private void SpawnAtPoint(GameObject prefabToSpawn, RectTransform point, int number)
    {
        // ������ ������
        GameObject spawnedObject = Instantiate(prefabToSpawn, parent);
        spawnedObject.transform.position = point.position;
        spawnedObject.transform.rotation = point.rotation;

        // ��������� LevelID, ���� � ������� ���� ��������� LevelCharacteristic
        LevelCharacteristic levelChar = spawnedObject.GetComponent<LevelCharacteristic>();
        if (levelChar != null)
        {
            levelChar.LevelID = number; // ������������� ID
        }
        else
        {
            Debug.LogWarning($"[LvlController] � ������� \"{spawnedObject.name}\" ����������� ��������� LevelCharacteristic!");
        }

        // ������������� ��������� ����
        AssignNumberToText(spawnedObject, number);
    }
    private void AssignNumberToText(GameObject spawnedObject, int number)
    {
        Text textUI = spawnedObject.GetComponentInChildren<Text>();
        if (textUI != null)
        {
            textUI.text = number.ToString();
            return;
        }
        Debug.LogWarning($"[LvlController] ��������� ���� �� �������  \"{spawnedObject.name}\"!");
    }
}