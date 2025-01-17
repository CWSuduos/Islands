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
            Debug.LogError("[LvlEnter] Не найден LevelCharacteristic на этом объекте!");
        }
        lvlCreator = FindObjectOfType<LvlCreator>();
        if (lvlCreator == null)
        {
            Debug.LogError("[LvlEnter] Не найден LvlCreator в сцене!");
        }
    }
    public void OnClickEnterLevel()
    {
        if (levelChar == null || lvlCreator == null)
        {
            Debug.LogWarning("[LvlEnter]  невозможно вызвать CreatePanel!");
            return;
        }
        int currentID = levelChar.LevelID;
        
        lvlCreator.CreatePanel(currentID);

        Debug.Log($"[LvlEnter] Уровень {currentID} выбран, панель создана через LvlCreator.");
    }
}
