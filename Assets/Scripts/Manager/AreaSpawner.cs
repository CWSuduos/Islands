using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class AreaSpawner : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;
    public float spawnInterval = 1f;
    public Rect spawnArea = new Rect(-5f, -5f, 10f, 10f);
    public int maxObjects = 10;
    public bool infiniteSpawn = true;
    public Color gizmoColor = Color.green;

    private int spawnedObjectsCount = 0;

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Vector3 center = transform.position + new Vector3(spawnArea.x, spawnArea.y, 0f);
        Vector3 size = new Vector3(spawnArea.width, spawnArea.height, 0f);
        Gizmos.DrawWireCube(center, size);
    }

    IEnumerator SpawnObjects()
    {
        while (infiniteSpawn || spawnedObjectsCount < maxObjects)
        {
            SpawnObject();

            if (!infiniteSpawn)
            {
                spawnedObjectsCount++;
                if (spawnedObjectsCount >= maxObjects)
                {
                    yield break;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObject()
    {
        if (prefabsToSpawn.Length == 0)
        {
            Debug.LogWarning("Нет префабов для спавна!");
            return;
        }

        int randomIndex = Random.Range(0, prefabsToSpawn.Length);
        GameObject prefab = prefabsToSpawn[randomIndex];

        if (prefab == null)
        {
            Debug.LogWarning("Префаб под индексом " + randomIndex + " не задан!");
            return;
        }

        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnArea.xMin, spawnArea.xMax),
            Random.Range(spawnArea.yMin, spawnArea.yMax),
            0f
        );
        spawnPosition += transform.position;

        GameObject clonedObject = Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
        clonedObject.transform.localScale = prefab.transform.localScale;
    }
    public void StopSpawning()
    {
        StopAllCoroutines();
    }
}