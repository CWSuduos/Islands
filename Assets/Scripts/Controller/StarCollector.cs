using UnityEngine;
using System.Collections.Generic;

public class StarCollector : MonoBehaviour
{
    private int _starCount = 0;
    public GameObject gameOverPanel; // Панель проигрыша
    public GameObject prefabToInstantiate; // Префаб для отображения
    public RectTransform[] instantiationTargets; // Массив RectTransform для позиционирования звезд
    private List<GameObject> _instantiatedPrefabs = new List<GameObject>();

    // Добавление звезды
    public void AddStar()
    {
        if (_starCount >= 3) return; // Максимум 3 звезды

        _starCount++;
        Debug.Log($"Текущее количество звезд: {_starCount}");
        CheckPanelVisibility();
    }

    // Проверка видимости панели и управление префабами
    private void CheckPanelVisibility()
    {
        bool isPanelVisible = gameOverPanel?.activeInHierarchy ?? false;

        if (isPanelVisible)
        {
            Debug.Log("Панель проигрыша видна");
            InstantiatePrefabs();
        }
        else if (!isPanelVisible && _instantiatedPrefabs.Count > 0)
        {
            Debug.Log("Счётчик обнулён");
            _starCount = 0;
            Debug.Log($"Текущее количество звезд: {_starCount}");
            DestroyInstantiatedPrefabs();
        }
    }

    // Создание префабов в соответствии с количеством звезд
    private void InstantiatePrefabs()
    {
        if (prefabToInstantiate != null && instantiationTargets.Length > 0)
        {
            // Удаляем лишние префабы, если их стало больше, чем звезд
            while (_instantiatedPrefabs.Count > _starCount)
            {
                GameObject prefabToDestroy = _instantiatedPrefabs[_instantiatedPrefabs.Count - 1];
                Destroy(prefabToDestroy);
                _instantiatedPrefabs.RemoveAt(_instantiatedPrefabs.Count - 1);
            }

            // Создаём недостающие префабы
            while (_instantiatedPrefabs.Count < _starCount)
            {
                int index = _instantiatedPrefabs.Count;
                GameObject instantiatedObject = Instantiate(prefabToInstantiate, instantiationTargets[index]);
                _instantiatedPrefabs.Add(instantiatedObject);
                Debug.Log($"Создан префаб: {instantiatedObject.name} на позиции {index}");
            }
        }
        else
        {
            Debug.LogWarning("Префаб для отображения или RectTransform не заданы!");
        }
    }

    // Уничтожение всех созданных префабов
    private void DestroyInstantiatedPrefabs()
    {
        foreach (GameObject prefab in _instantiatedPrefabs)
        {
            if (prefab != null)
            {
                Destroy(prefab);
            }
        }
        _instantiatedPrefabs.Clear();
        Debug.Log("Уничтожены все созданные префабы.");
    }

    // Проверка видимости панели каждый кадр
    private void Update()
    {
        CheckPanelVisibility();
    }
}