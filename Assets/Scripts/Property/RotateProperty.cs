using UnityEngine;

public class RotateProperty : MonoBehaviour
{
    public GameObject[] prefabs;
    public int initialPrefabIndex = 0;
    private int currentPrefabIndex;
    private GameObject spawnedObject;


    void Start()
    {
        currentPrefabIndex = initialPrefabIndex;
        SpawnPrefab(currentPrefabIndex);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.transform.IsChildOf(transform))
            {
                SpawnNextPrefab();
            }
        }
    }

    private void SpawnNextPrefab()
    {
        if (prefabs.Length == 0)
        {
            Debug.LogError("Массив префабов пуст!");
            return;
        }

        currentPrefabIndex++;

        if (currentPrefabIndex >= prefabs.Length)
        {
            currentPrefabIndex = 0;
        }

        SpawnPrefab(currentPrefabIndex);
    }
    private void SpawnPrefab(int index)
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }

        spawnedObject = Instantiate(prefabs[index], transform.position, Quaternion.identity);
        spawnedObject.transform.parent = transform;
    }
}