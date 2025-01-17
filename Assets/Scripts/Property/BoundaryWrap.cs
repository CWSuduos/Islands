using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryWrap : MonoBehaviour
{
    public float leftBoundary = -10f;
    public float rightBoundary = 10f;
    public float topBoundary = 5f;
    public float bottomBoundary = -5f;

    void Update()
    {
        Vector3 position = transform.position;

        if (position.x > rightBoundary)
        {
            position.x = leftBoundary;
        }
        else if (position.x < leftBoundary)
        {
            position.x = rightBoundary;
        }

        if (position.y > topBoundary)
        {
            position.y = bottomBoundary;
        }
        else if (position.y < bottomBoundary)
        {
            position.y = topBoundary;
        }

        transform.position = position;
    }
}
