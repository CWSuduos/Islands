using System.Linq;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;
    public float[] spawnChances;
    public float spawnAreaHeight = 5f;
    public float spawnInterval = 1f;
    public int maxSpawnedObjects = 10;
    public bool canSpawn = true;
    public float fixedZPosition = 0f;

    private int spawnedObjectsCount = 0;
    private float nextSpawnTime = 0f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Главная камера не найдена!");
            enabled = false;
            return;
        }

        if (spawnChances == null || spawnChances.Length != prefabsToSpawn.Length)
        {
            spawnChances = new float[prefabsToSpawn.Length];
            for (int i = 0; i < spawnChances.Length; i++)
            {
                spawnChances[i] = 1f;
            }
        }
    }

    void Update()
    {
        if (!canSpawn) return;
        if (spawnedObjectsCount >= maxSpawnedObjects) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnPrefab();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnPrefab()
    {
        if (prefabsToSpawn == null || prefabsToSpawn.Length == 0)
        {
            Debug.LogWarning("Массив префабов пуст!");
            return;
        }

        GameObject prefabToSpawn = ChoosePrefabByChance();

        float cameraWidth = GetCameraWidth();

        Vector3 randomPosition = transform.position + new Vector3(
            Random.Range(-cameraWidth / 2f, cameraWidth / 2f),
            Random.Range(-spawnAreaHeight / 2f, spawnAreaHeight / 2f),
            Random.Range(-5f, 5f)
        );

        GameObject spawnedObject = Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
        spawnedObjectsCount++;
        spawnedObject.transform.SetParent(transform);
    }

    private GameObject ChoosePrefabByChance()
    {
        float totalChance = spawnChances.Sum();
        float randomValue = Random.Range(0f, totalChance);
        float cumulativeChance = 0f;

        for (int i = 0; i < prefabsToSpawn.Length; i++)
        {
            cumulativeChance += spawnChances[i];
            if (randomValue <= cumulativeChance)
            {
                return prefabsToSpawn[i];
            }
        }

        return prefabsToSpawn[0];
    }

    private float GetCameraWidth()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Главная камера не найдена!");
            return 0f;
        }
        return 2f * mainCamera.orthographicSize * mainCamera.aspect;
    }

    private void OnDrawGizmosSelected()
    {
        if (mainCamera == null) return;
        float cameraWidth = GetCameraWidth();
        Vector3 gizmosPosition = transform.position;
        gizmosPosition.z = fixedZPosition;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(gizmosPosition, new Vector3(cameraWidth, spawnAreaHeight, 10f));
    }
}