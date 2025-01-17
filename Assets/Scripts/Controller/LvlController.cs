using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
           

public class LvlController : MonoBehaviour
{
    [Header("Префабы для спавна")]
   
    public GameObject prefabB;

    [Header("Точки спавна (RectTransform)")]
    public List<RectTransform> spawnPoints;

    [Header("Родительский объект для созданных клонов")]
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
                Debug.LogWarning($"Точка спавна с индексом {i} не назначена!");
            }
        }
    }
    private void SpawnAtPoint(GameObject prefabToSpawn, RectTransform point, int number)
    {
        // Создаём объект
        GameObject spawnedObject = Instantiate(prefabToSpawn, parent);
        spawnedObject.transform.position = point.position;
        spawnedObject.transform.rotation = point.rotation;

        // Назначаем LevelID, если у объекта есть компонент LevelCharacteristic
        LevelCharacteristic levelChar = spawnedObject.GetComponent<LevelCharacteristic>();
        if (levelChar != null)
        {
            levelChar.LevelID = number; // Устанавливаем ID
        }
        else
        {
            Debug.LogWarning($"[LvlController] У объекта \"{spawnedObject.name}\" отсутствует компонент LevelCharacteristic!");
        }

        // Устанавливаем текстовое поле
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
        Debug.LogWarning($"[LvlController] Текстовое поле не найдено  \"{spawnedObject.name}\"!");
    }
}