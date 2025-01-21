using UnityEngine;

public class SetToForeground : MonoBehaviour
{
    void Start()
    {
        // Получаем компонент SpriteRenderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Проверяем, что компонент существует
        if (spriteRenderer != null)
        {
            // Устанавливаем максимальное значение порядка отрисовки
            spriteRenderer.sortingOrder = 32767;
        }
        else
        {
            // Выводим сообщение об ошибке, если компонент SpriteRenderer не найден
            Debug.LogError("SpriteRenderer component not found on this GameObject.");
        }
    }
}