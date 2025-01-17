using DG.Tweening;
using UnityEngine;

public class MoveDownToLine : MonoBehaviour
{
    public float speed = 1f; // Скорость движения вниз
    public float bottomBoundary = -5f; // Y-координата нижней границы
    public Color gizmoColor = Color.red; // Цвет Gizmo
    [SerializeField] private GameObject starPrefab; // Префаб звезды для анимации
    [SerializeField] private float animationDuration = 0.5f; // Длительность анимации
    [SerializeField] private float upwardMovement = 2f; // Расстояние, на которое звезда поднимается
    void Update()
    {
        // Двигаем объект вниз
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Проверяем, достиг ли объект границы
        if (transform.position.y <= bottomBoundary)
        {
            // Останавливаем движение, чтобы объект не продолжал двигаться вниз
            enabled = false; // Отключаем скрипт, чтобы остановить Update
        }
    }


    void OnDrawGizmos()
    {
        // Рисуем линию границы в редакторе
        Gizmos.color = gizmoColor;
        Gizmos.DrawLine(new Vector3(-100f, bottomBoundary, 0f), new Vector3(100f, bottomBoundary, 0f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Сообщаем LvlWinManager о получении звезды
            FindObjectOfType<LvlWinManager>().AddStar();

            // 2. Создаем и анимируем звезду
            Vector3 starStartPosition = other.transform.position; // Позиция игрока
            GameObject star = Instantiate(starPrefab, starStartPosition, Quaternion.identity);

            // Анимация движения вверх и затухания
            star.transform.DOMoveY(starStartPosition.y + upwardMovement, animationDuration);
            star.transform.DOScale(Vector3.one * 2f, animationDuration); // Увеличиваем размер в 2 раза
            star.GetComponent<SpriteRenderer>().DOFade(0f, animationDuration)
                .OnComplete(() => Destroy(star)); // Уничтожаем звезду после анимации


            // Остальной код (IgnoreCollision или Destroy) по вашему усмотрению
            Physics2D.IgnoreCollision(other, GetComponent<Collider2D>());
        }
    }
}