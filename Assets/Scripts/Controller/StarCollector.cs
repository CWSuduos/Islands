using UnityEngine;
using System.Collections.Generic;

public class StarCollector : MonoBehaviour
{
    private int _starCount = 0;
    public GameObject gameOverPanel; // ������ ���������
    public GameObject prefabToInstantiate; // ������ ��� �����������
    public RectTransform[] instantiationTargets; // ������ RectTransform ��� ���������������� �����
    private List<GameObject> _instantiatedPrefabs = new List<GameObject>();

    // ���������� ������
    public void AddStar()
    {
        if (_starCount >= 3) return; // �������� 3 ������

        _starCount++;
        Debug.Log($"������� ���������� �����: {_starCount}");
        CheckPanelVisibility();
    }

    // �������� ��������� ������ � ���������� ���������
    private void CheckPanelVisibility()
    {
        bool isPanelVisible = gameOverPanel?.activeInHierarchy ?? false;

        if (isPanelVisible)
        {
            Debug.Log("������ ��������� �����");
            InstantiatePrefabs();
        }
        else if (!isPanelVisible && _instantiatedPrefabs.Count > 0)
        {
            Debug.Log("������� ������");
            _starCount = 0;
            Debug.Log($"������� ���������� �����: {_starCount}");
            DestroyInstantiatedPrefabs();
        }
    }

    // �������� �������� � ������������ � ����������� �����
    private void InstantiatePrefabs()
    {
        if (prefabToInstantiate != null && instantiationTargets.Length > 0)
        {
            // ������� ������ �������, ���� �� ����� ������, ��� �����
            while (_instantiatedPrefabs.Count > _starCount)
            {
                GameObject prefabToDestroy = _instantiatedPrefabs[_instantiatedPrefabs.Count - 1];
                Destroy(prefabToDestroy);
                _instantiatedPrefabs.RemoveAt(_instantiatedPrefabs.Count - 1);
            }

            // ������ ����������� �������
            while (_instantiatedPrefabs.Count < _starCount)
            {
                int index = _instantiatedPrefabs.Count;
                GameObject instantiatedObject = Instantiate(prefabToInstantiate, instantiationTargets[index]);
                _instantiatedPrefabs.Add(instantiatedObject);
                Debug.Log($"������ ������: {instantiatedObject.name} �� ������� {index}");
            }
        }
        else
        {
            Debug.LogWarning("������ ��� ����������� ��� RectTransform �� ������!");
        }
    }

    // ����������� ���� ��������� ��������
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
        Debug.Log("���������� ��� ��������� �������.");
    }

    // �������� ��������� ������ ������ ����
    private void Update()
    {
        CheckPanelVisibility();
    }
}