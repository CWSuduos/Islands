using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnter : MonoBehaviour
{
    private LevelCharacteristic levelChar;
    private LvlCreator lvlCreator;

    private void Awake()
    {
        levelChar = GetComponent<LevelCharacteristic>();
        if (levelChar == null)
        {
            Debug.LogError("[LvlEnter] �� ������ LevelCharacteristic �� ���� �������!");
        }
        lvlCreator = FindObjectOfType<LvlCreator>();
        if (lvlCreator == null)
        {
            Debug.LogError("[LvlEnter] �� ������ LvlCreator � �����!");
        }
    }
    public void OnClickEnterLevel()
    {
        if (levelChar == null || lvlCreator == null)
        {
            Debug.LogWarning("[LvlEnter]  ���������� ������� CreatePanel!");
            return;
        }
        int currentID = levelChar.LevelID;
        
        lvlCreator.CreatePanel(currentID);

        Debug.Log($"[LvlEnter] ������� {currentID} ������, ������ ������� ����� LvlCreator.");
    }
}
