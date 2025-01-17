using UnityEngine;
using DG.Tweening;

public class FlyToPlayer : MonoBehaviour
{
    public float speed = 5f;
    public float bottomBoundary = -10f;
    public Color gizmoColor = Color.red;
    public GameObject losePanel; // Панель проигрыша
    public AreaSpawner spawner; // Ссылка на спавнер
    public GameObject player; // Ссылка на игрока
    public float destroyDelay = 0f; // Задержка перед уничтожением объекта
    private TimerManager timerManager;

    void Start()
    {
        timerManager = GameObject.FindObjectOfType<TimerManager>();
        float distance = transform.position.y - bottomBoundary;
        float duration = distance / speed;

        transform.DOMoveY(bottomBoundary, duration).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawLine(new Vector3(-100f, bottomBoundary, 0f), new Vector3(100f, bottomBoundary, 0f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject losePanel = GameObject.FindWithTag("Lose");
            if (losePanel != null)
            {
                losePanel.SetActive(true);
            }
            else
            {
                Debug.LogError("Lose panel not found! Check the tag.");
            }


            if (spawner != null)
            {
                spawner.StopSpawning();
            }
            else
            {
                Debug.LogError("Spawner not assigned!");
            }

            if (player != null)
            {
                Destroy(player); // Или player.SetActive(false);
            }
            else
            {
                Debug.LogError("Player not assigned!");
            }

            if (spawner != null)
            {
                foreach (Transform child in spawner.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            transform.DOKill();
            Destroy(gameObject, destroyDelay);
            timerManager.OnTimerEnd();
        }
    }
}