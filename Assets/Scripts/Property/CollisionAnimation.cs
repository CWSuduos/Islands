using UnityEngine;
using DG.Tweening;

public class CollisionAnimation : MonoBehaviour
{
    public Sprite animationSprite; // Спрайт анимации
    public float animationDuration = 0.5f; // Продолжительность анимации
    public float spriteScale = 2f; // Масштаб спрайта анимации
    public float circleRadius = 2f; // Радиус круга
    public Color startColor = Color.red; // Начальный цвет градиента
    public Color endColor = Color.yellow; // Конечный цвет градиента


    private GameObject animationObject; // Объект для анимации


    void OnTriggerEnter2D(Collider2D other)
    {
        // Создаем объект для анимации в точке контакта
        Vector3 contactPoint = other.ClosestPoint(transform.position); //other.transform.position; //transform.position; //other.bounds.center;
        animationObject = new GameObject("Collision Animation");
        animationObject.transform.position = contactPoint;

        // Добавляем Sprite Renderer и устанавливаем спрайт
        SpriteRenderer spriteRenderer = animationObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = animationSprite;
        spriteRenderer.sortingOrder = 10; // Устанавливаем sortingOrder, чтобы анимация была видна поверх других объектов


        // Создаем круг (можно использовать отдельный спрайт или LineRenderer)
        GameObject circleObject = new GameObject("Circle Animation");
        circleObject.transform.position = contactPoint;
        circleObject.transform.localScale = Vector3.zero;
        SpriteRenderer circleSpriteRenderer = circleObject.AddComponent<SpriteRenderer>();
        circleSpriteRenderer.color = startColor;
        circleObject.transform.parent = animationObject.transform;
        circleSpriteRenderer.sortingOrder = 5; // sortingOrder меньше, чем у спрайта анимации

        // Анимация спрайта
        animationObject.transform.localScale = Vector3.zero;
        animationObject.transform.DOScale(spriteScale, animationDuration).SetEase(Ease.OutBack);


        // Анимация круга
        circleObject.transform.DOScale(circleRadius * 2, animationDuration).SetEase(Ease.OutBack);


        // Градиент цвета круга (пример с изменением цвета от красного к желтому)
        circleSpriteRenderer.DOColor(endColor, animationDuration);

        // Уничтожаем объект анимации после завершения
        Destroy(animationObject, animationDuration);

    }


}