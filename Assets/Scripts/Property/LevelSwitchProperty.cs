using System.Collections.Generic;
using UnityEngine;

public class LevelSwitchProperty : MonoBehaviour
{
    private static Dictionary<GameObject, bool> cloneStates = new Dictionary<GameObject, bool>();
    [SerializeField] private GameObject prefabWhenTrue;
    [SerializeField] private GameObject prefabWhenFalse;

    private GameObject currentPrefab;


    private void Awake()
    {

        // ������������ ���� � ������� ��� ������
        if (!cloneStates.ContainsKey(gameObject))
        {
            cloneStates.Add(gameObject, false); // ��������� ��������� - false
        }
        UpdatePrefab();
    }

    public void SwitchState()
    {
        // ������ ��������� ��� �������� �����
        cloneStates[gameObject] = !cloneStates[gameObject];
        UpdatePrefab();
    }


    private void UpdatePrefab()
    {
        if (currentPrefab != null)
        {
            Destroy(currentPrefab);
        }

        GameObject prefabToInstantiate = cloneStates[gameObject] ? prefabWhenTrue : prefabWhenFalse;
        currentPrefab = Instantiate(prefabToInstantiate, transform.position, transform.rotation, transform);
    }


    private void OnDestroy()
    {
        // ������� ���� �� ������� ��� �����������
        cloneStates.Remove(gameObject);

        if (currentPrefab != null)
        {
            Destroy(currentPrefab);
        }
    }
}