using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] sprites; // Массив спрайтов для анимации
    public float animationSpeed = 10f; // Скорость анимации (спрайтов в секунду)

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
        // Обновляем таймер
        timer += Time.deltaTime * animationSpeed;

        // Переключаем спрайт, если таймер превысил 1
        if (timer >= 1f)
        {
            // Переходим к следующему спрайту
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length; // Используем оператор %, чтобы циклически перебирать спрайты

            // Обновляем спрайт в Sprite Renderer
            spriteRenderer.sprite = sprites[currentSpriteIndex];

            // Сбрасываем таймер
            timer -= 1f;
        }
    }
}