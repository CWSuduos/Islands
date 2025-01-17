using UnityEngine;
using DG.Tweening;

public class CollisionAnimation : MonoBehaviour
{
    public Sprite animationSprite; // ������ ��������
    public float animationDuration = 0.5f; // ����������������� ��������
    public float spriteScale = 2f; // ������� ������� ��������
    public float circleRadius = 2f; // ������ �����
    public Color startColor = Color.red; // ��������� ���� ���������
    public Color endColor = Color.yellow; // �������� ���� ���������


    private GameObject animationObject; // ������ ��� ��������


    void OnTriggerEnter2D(Collider2D other)
    {
        // ������� ������ ��� �������� � ����� ��������
        Vector3 contactPoint = other.ClosestPoint(transform.position); //other.transform.position; //transform.position; //other.bounds.center;
        animationObject = new GameObject("Collision Animation");
        animationObject.transform.position = contactPoint;

        // ��������� Sprite Renderer � ������������� ������
        SpriteRenderer spriteRenderer = animationObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = animationSprite;
        spriteRenderer.sortingOrder = 10; // ������������� sortingOrder, ����� �������� ���� ����� ������ ������ ��������


        // ������� ���� (����� ������������ ��������� ������ ��� LineRenderer)
        GameObject circleObject = new GameObject("Circle Animation");
        circleObject.transform.position = contactPoint;
        circleObject.transform.localScale = Vector3.zero;
        SpriteRenderer circleSpriteRenderer = circleObject.AddComponent<SpriteRenderer>();
        circleSpriteRenderer.color = startColor;
        circleObject.transform.parent = animationObject.transform;
        circleSpriteRenderer.sortingOrder = 5; // sortingOrder ������, ��� � ������� ��������

        // �������� �������
        animationObject.transform.localScale = Vector3.zero;
        animationObject.transform.DOScale(spriteScale, animationDuration).SetEase(Ease.OutBack);


        // �������� �����
        circleObject.transform.DOScale(circleRadius * 2, animationDuration).SetEase(Ease.OutBack);


        // �������� ����� ����� (������ � ���������� ����� �� �������� � �������)
        circleSpriteRenderer.DOColor(endColor, animationDuration);

        // ���������� ������ �������� ����� ����������
        Destroy(animationObject, animationDuration);

    }


}