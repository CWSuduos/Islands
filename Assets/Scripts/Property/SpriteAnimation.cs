using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] sprites; // ������ �������� ��� ��������
    public float animationSpeed = 10f; // �������� �������� (�������� � �������)

    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;
    private float timer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Sprite Renderer not found!");
            enabled = false;
            return;
        }

        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogError("No sprites assigned!");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        // ��������� ������
        timer += Time.deltaTime * animationSpeed;

        // ����������� ������, ���� ������ �������� 1
        if (timer >= 1f)
        {
            // ��������� � ���������� �������
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length; // ���������� �������� %, ����� ���������� ���������� �������

            // ��������� ������ � Sprite Renderer
            spriteRenderer.sprite = sprites[currentSpriteIndex];

            // ���������� ������
            timer -= 1f;
        }
    }
}