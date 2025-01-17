using DG.Tweening;
using UnityEngine;

public class MoveDownToLine : MonoBehaviour
{
    public float speed = 1f; // �������� �������� ����
    public float bottomBoundary = -5f; // Y-���������� ������ �������
    public Color gizmoColor = Color.red; // ���� Gizmo
    [SerializeField] private GameObject starPrefab; // ������ ������ ��� ��������
    [SerializeField] private float animationDuration = 0.5f; // ������������ ��������
    [SerializeField] private float upwardMovement = 2f; // ����������, �� ������� ������ �����������
    void Update()
    {
        // ������� ������ ����
        transform.position += Vector3.down * speed * Time.deltaTime;

        // ���������, ������ �� ������ �������
        if (transform.position.y <= bottomBoundary)
        {
            // ������������� ��������, ����� ������ �� ��������� ��������� ����
            enabled = false; // ��������� ������, ����� ���������� Update
        }
    }


    void OnDrawGizmos()
    {
        // ������ ����� ������� � ���������
        Gizmos.color = gizmoColor;
        Gizmos.DrawLine(new Vector3(-100f, bottomBoundary, 0f), new Vector3(100f, bottomBoundary, 0f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. �������� LvlWinManager � ��������� ������
            FindObjectOfType<LvlWinManager>().AddStar();

            // 2. ������� � ��������� ������
            Vector3 starStartPosition = other.transform.position; // ������� ������
            GameObject star = Instantiate(starPrefab, starStartPosition, Quaternion.identity);

            // �������� �������� ����� � ���������
            star.transform.DOMoveY(starStartPosition.y + upwardMovement, animationDuration);
            star.transform.DOScale(Vector3.one * 2f, animationDuration); // ����������� ������ � 2 ����
            star.GetComponent<SpriteRenderer>().DOFade(0f, animationDuration)
                .OnComplete(() => Destroy(star)); // ���������� ������ ����� ��������


            // ��������� ��� (IgnoreCollision ��� Destroy) �� ������ ����������
            Physics2D.IgnoreCollision(other, GetComponent<Collider2D>());
        }
    }
}