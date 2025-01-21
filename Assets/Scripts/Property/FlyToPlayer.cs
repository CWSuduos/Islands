using UnityEngine;
using DG.Tweening;
using System;


public class FlyToPlayer : MonoBehaviour
{
    public float speed = 5f;
    public float bottomBoundary = -10f;
    public Color gizmoColor = Color.red;
    private GameObject losePanel; // ������ ���������
    public AreaSpawner spawner; // ������ �� �������
    public GameObject player; // ������ �� ������
    public float destroyDelay = 0f; // �������� ����� ������������ �������
    private TimerManager timerManager;
    private LvlWinManager lvlWinManager;

    void Start()
    {
        timerManager = GameObject.FindObjectOfType<TimerManager>();
        float distance = transform.position.y - bottomBoundary;
        float duration = distance / speed;
        lvlWinManager = GetComponent<LvlWinManager>();

        if (lvlWinManager == null)
        {
            
        }
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

    private Timer timer;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            timer = FindObjectOfType<Timer>();
            timer.StopTimer();
            FindObjectOfType<LvlWinManager>()?.SetLosePanel();
   
        }
    }
}

