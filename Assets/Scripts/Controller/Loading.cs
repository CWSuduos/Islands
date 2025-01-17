using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Transform centerPoint;
    [SerializeField] private GameObject objectToFlyPrefab;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float duration = 5f;
    [SerializeField] private bool clockwise = true;
    [SerializeField] private float spawnInterval = 0.1f;
    [SerializeField] private bool lookAtCenter = false;
    [SerializeField] private Text progressText; // ��������� ���� ��� ����������� ��������� (������ UI.Text)

    private float lastSpawnTime;
    private GameObject objectToFly;
    private Tweener movementTween;
    private Tweener rotationTween;
    private List<GameObject> spawnedCopies = new List<GameObject>();
    private float currentProgress = 0f;
    public GameObject PanelToShow;
    public GameObject PanelToHide;
    private void Start()
    {
        StartFlight();
       
       
    }
    private void Update()
    {
        // ��������� ���������� ������ � ��������� ������ ������ ���� ���
        if ( progressText.text == "100%")
        {
            PanelToHide.SetActive(false);
            PanelToShow.SetActive(true);
            progressText.text = "0";
        }
    }

    public void StartFlight()
    {
        // ��������� ������� ��������, ���� ��� ����
        if (movementTween != null && movementTween.IsActive())
        {
            movementTween.Kill();
        }
        if (rotationTween != null && rotationTween.IsActive())
        {
            rotationTween.Kill();
        }

        // ������� ������ ������, ���� �� ����
        if (objectToFly != null)
        {
            Destroy(objectToFly);
        }

        // ������������ ������ � ����� ������������� ��������� ������� �� ����������
        objectToFly = Instantiate(objectToFlyPrefab, (Vector2)centerPoint.position + new Vector2(radius, 0f), Quaternion.identity, transform);

        // ������� ������ ����� ��� �������� �� �����, ����� ���������� � Vector3
        Vector3[] path3D = new Vector3[360];
        for (int i = 0; i < 360; i++)
        {
            float angle = i * Mathf.Deg2Rad;
            path3D[i] = new Vector3(centerPoint.position.x + radius * Mathf.Cos(angle), centerPoint.position.y + radius * Mathf.Sin(angle), 0f);
        }

        // ����������� ����� ���� �����
        if (!clockwise) System.Array.Reverse(path3D);

        currentProgress = 0;
        UpdateProgressText();

        // ��������� �������� �������� �� ���� � ������� DOTween
        movementTween = objectToFly.transform.DOPath(path3D, duration, PathType.CatmullRom)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.Linear)
            .SetOptions(true)
            .OnUpdate(() => {
                SpawnCopy();
                currentProgress = movementTween.ElapsedPercentage(true);
                if (currentProgress <= 0.5f)
                {
                    UpdateProgressText();
                }
            })
            .OnStepComplete(() => {
                if (movementTween.CompletedLoops() == 1)
                {
                    movementTween.OnUpdate(null);
                }
            })
            .OnComplete(() => {
                Debug.Log("����� ��������");
                foreach (GameObject copy in spawnedCopies)
                {
                    Destroy(copy);
                }
                spawnedCopies.Clear();
                currentProgress = 0f;
                UpdateProgressText();
            });

        // �����������: ������� ������� �� ����������� ��������
        rotationTween = objectToFly.transform.DORotate(new Vector3(0, 0, clockwise ? -360 : 360), duration, RotateMode.FastBeyond360)
             .SetLoops(2, LoopType.Yoyo)
             .SetEase(Ease.Linear);

        lastSpawnTime = -spawnInterval;
    }

    private void SpawnCopy()
    {
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            GameObject newCopy = Instantiate(objectToFlyPrefab, objectToFly.transform.position, objectToFly.transform.rotation, transform);
            if (lookAtCenter)
                newCopy.transform.up = centerPoint.position - newCopy.transform.position;

            spawnedCopies.Add(newCopy);

            lastSpawnTime = Time.time;
        }
    }

    private void UpdateProgressText()
    {
        if (progressText != null)
        {
            progressText.text = $"{Mathf.RoundToInt(currentProgress * 200f)}%";
        }
    }
}
