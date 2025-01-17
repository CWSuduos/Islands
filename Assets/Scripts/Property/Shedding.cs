using UnityEngine;
using DG.Tweening;
using System.Collections;
using Random = UnityEngine.Random;

public class Shedding : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;
    public float spawnInterval = 1f;
    public float ejectionForce = 5f; // Сила выброса
    public float scaleDuration = 0.5f;
    public float minScale = 0.5f;
    public float maxScale = 1.5f;
    public float fallSpeed = 5f;

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObject()
    {
        int randomIndex = Random.Range(0, prefabsToSpawn.Length);
        GameObject prefab = prefabsToSpawn[randomIndex];
        Vector3 spawnPosition = transform.position;
        GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = spawnedObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 1;
        float randomScale = Random.Range(minScale, maxScale);
        spawnedObject.transform.localScale = Vector3.one * randomScale;
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        rb.AddForce(randomDirection * ejectionForce, ForceMode2D.Impulse);
        spawnedObject.transform.DOScale(0f, scaleDuration).SetEase(Ease.InBack)
            .OnComplete(() => Destroy(spawnedObject));
    }
}