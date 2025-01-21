using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;

public class PickupEffect : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    [Header("Circle Effect")]
    [SerializeField] private float circleExpandDuration = 0.5f;
    [SerializeField] private float circleRadius = 2f;
    [SerializeField] private Color circleColor = Color.yellow;
    [SerializeField] private int circleSegments = 32;

    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int projectileCount = 20;
    [SerializeField] private float projectileForce = 5f;
    [SerializeField] private float projectileLifespan = 1f;
    private LvlWinManager StarCollector;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            StartCoroutine(PlayEffect(collision.transform.position));
            StarCollector.AddStar();
            Destroy(gameObject); // Мгновенно уничтожаем объект
        }
    }

    private IEnumerator PlayEffect(Vector3 playerPosition)
    {
        // 1. Расширяющийся круг
        GameObject circle = new GameObject("Circle");
        circle.transform.position = playerPosition;
        LineRenderer lr = circle.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default")); // Используем стандартный спрайтовый шейдер
        lr.startColor = circleColor;
        lr.endColor = circleColor;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.loop = true;
        lr.positionCount = circleSegments + 1;


        float currentRadius = 0f;
        DOTween.To(() => currentRadius, x => currentRadius = x, circleRadius, circleExpandDuration) // Исправлено
         .OnUpdate(() =>
         {
                for (int i = 0; i <= circleSegments; i++)
                {
                    float angle = i * 2f * Mathf.PI / circleSegments;
                    Vector3 pos = playerPosition + new Vector3(Mathf.Cos(angle) * currentRadius, Mathf.Sin(angle) * currentRadius, 0f);
                    lr.SetPosition(i, pos);
                }
            })
            .OnComplete(() => Destroy(circle));


        // 2. Выстрел префабами
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * 360f / projectileCount;
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            GameObject projectile = Instantiate(projectilePrefab, playerPosition, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(direction * projectileForce, ForceMode2D.Impulse);
            }

            Destroy(projectile, projectileLifespan);
        }

        yield return new WaitForSeconds(circleExpandDuration); // Ждем завершения анимации круга (не обязательно)
    }
}


public class Star : MonoBehaviour
{
    public event Action onDestroyed;

    private void OnDestroy()
    {
        onDestroyed?.Invoke();
    }
}