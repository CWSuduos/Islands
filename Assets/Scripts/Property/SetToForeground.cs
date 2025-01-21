using UnityEngine;

public class SetToForeground : MonoBehaviour
{
    void Start()
    {
        // �������� ��������� SpriteRenderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // ���������, ��� ��������� ����������
        if (spriteRenderer != null)
        {
            // ������������� ������������ �������� ������� ���������
            spriteRenderer.sortingOrder = 32767;
        }
        else
        {
            // ������� ��������� �� ������, ���� ��������� SpriteRenderer �� ������
            Debug.LogError("SpriteRenderer component not found on this GameObject.");
        }
    }
}