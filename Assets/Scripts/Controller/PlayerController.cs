using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found!");
            enabled = false;
        }
    }

    public void MoveRight()
    {
        rb.AddForce(Vector2.right * moveSpeed);
        rb.AddTorque(-rotationSpeed); // Вращение по часовой стрелке
    }

    public void MoveLeft()
    {
        rb.AddForce(Vector2.left * moveSpeed);
        rb.AddTorque(rotationSpeed); // Вращение против часовой стрелки
    }
}