using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class WaveEffectNoPrefab : MonoBehaviour
{
    public int numberOfSegments = 20; // ���������� ��������� ��� ������������� ��������������
    public float waveRadius = 0.5f; // ������ ��������������
    public float orbitRadius = 1.5f;
    public float orbitSpeed = 2f;
    public Gradient waveGradient;
    public float pulseInterval = 0.5f;

    private List<GameObject> activeWaves = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("CreateWavePulse", 0f, pulseInterval);
    }

    void CreateWavePulse()
    {
        // ������� GameObject ��� �����
        GameObject waveObject = new GameObject("Wave");
        waveObject.transform.position = transform.position;
        waveObject.transform.SetParent(transform);

        // ��������� MeshFilter � MeshRenderer
        MeshFilter meshFilter = waveObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = waveObject.AddComponent<MeshRenderer>();

        // ������� �������� (����� ������������ ��������� ��� ������� ����)
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // ���������� Mesh ��� ��������������
        Mesh mesh = GenerateSemiCircleMesh();
        meshFilter.mesh = mesh;

        // ������������ ��������� ���� ��� ���������� ��������� �����
        float startAngle = Random.Range(0f, 360f);
        Vector3 startPosition = CalculatePositionOnCircle(startAngle);
        waveObject.transform.localPosition = startPosition;

        // �������� �������� �� �����
        float duration = 360f / orbitSpeed;
        waveObject.transform.DOLocalPath(GenerateCirclePath(), duration, PathType.CatmullRom)
            .SetOptions(true)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);

        // �������� ����� � ����������
        float colorStartTime = Random.value; // ��������� �������� ��� ���������
        DOTween.To(() => 0f, x => {
            Color waveColor = waveGradient.Evaluate((colorStartTime + x) % 1f);
            meshRenderer.material.color = waveColor;
        }, 1f, duration)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);

        activeWaves.Add(waveObject);
    }

    // ������� ��� ��������� Mesh ��������������
    Mesh GenerateSemiCircleMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[numberOfSegments + 2]; // +2 ��� ������ � ��������� �����
        int[] triangles = new int[numberOfSegments * 3];
        Vector2[] uv = new Vector2[numberOfSegments + 2];

        // ����������� �������
        vertices[0] = Vector3.zero;
        uv[0] = new Vector2(0.5f, 0f);

        // ������� ��������������
        for (int i = 0; i <= numberOfSegments; i++)
        {
            float angle = (float)i / numberOfSegments * 180f; // 180 �������� ��� ��������������
            float radians = Mathf.Deg2Rad * angle;
            vertices[i + 1] = new Vector3(Mathf.Cos(radians) * waveRadius, Mathf.Sin(radians) * waveRadius, 0f);
            uv[i + 1] = new Vector2((float)i / numberOfSegments, 1f);
        }

        // ������������
        for (int i = 0; i < numberOfSegments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        return mesh;
    }

    // ������� ��� ������� ������� �� ����������
    Vector3 CalculatePositionOnCircle(float angleDegrees)
    {
        float angleRadians = Mathf.Deg2Rad * angleDegrees;
        float x = orbitRadius * Mathf.Cos(angleRadians);
        float y = orbitRadius * Mathf.Sin(angleRadians);
        return new Vector3(x, y, 0f);
    }

    // ������� ��� ��������� ���� ����� ��� DoTween
    Vector3[] GenerateCirclePath()
    {
        int segments = 50;
        Vector3[] path = new Vector3[segments];
        for (int i = 0; i < segments; i++)
        {
            float angle = (float)i / segments * 360f;
            path[i] = CalculatePositionOnCircle(angle);
        }
        return path;
    }
}