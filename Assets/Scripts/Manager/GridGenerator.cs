using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int numCellsX = 5;
    public int numCellsY = 3;
    public Vector2 gridSize = new Vector2(10f, 6f);
    public Color gridColor = Color.red;
    public Color rectColor = Color.blue;
    public float gizmoSphereRadius = 0.2f; // Радиус сферы Gizmo
    public Rect[] cellRects;
    public Vector2[] cellCenters; // Массив центров ячеек

    void Start()
    {
        cellCenters = new Vector2[numCellsX * numCellsY];

        // Вычисляем размер каждой ячейки (нужно для Gizmos)
        float cellWidth = gridSize.x / numCellsX;
        float cellHeight = gridSize.y / numCellsY;


        for (int y = 0; y < numCellsY; y++)
        {
            for (int x = 0; x < numCellsX; x++)
            {
                // Вычисляем и сохраняем центр ячейки
                cellCenters[y * numCellsX + x] = GetCellCenter(x, y);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gridColor;
        float cellWidth = gridSize.x / numCellsX;
        float cellHeight = gridSize.y / numCellsY;

        for (int x = 0; x < numCellsX + 1; x++)
        {
            Vector3 start = new Vector3(x * cellWidth - gridSize.x / 2, gridSize.y / 2, 0) + transform.position;
            Vector3 end = new Vector3(x * cellWidth - gridSize.x / 2, -gridSize.y / 2, 0) + transform.position;
            Gizmos.DrawLine(start, end);
        }

        for (int y = 0; y < numCellsY + 1; y++)
        {
            Vector3 start = new Vector3(-gridSize.x / 2, y * cellHeight - gridSize.y / 2, 0) + transform.position;
            Vector3 end = new Vector3(gridSize.x / 2, y * cellHeight - gridSize.y / 2, 0) + transform.position;
            Gizmos.DrawLine(start, end);
        }

        if (cellRects != null)
        {
            Gizmos.color = rectColor;
            foreach (Rect rect in cellRects)
            {
                Vector3 pos = new Vector3(rect.x, rect.y, 0) + transform.position;
                Vector3 size = new Vector3(rect.width, rect.height, 0);
                Gizmos.DrawWireCube(pos, size);
            }
        }
        // Рисуем сферы в центрах ячеек
        Gizmos.color = Color.yellow; // Цвет сфер
        if (cellCenters != null)
        {
            foreach (Vector2 center in cellCenters)
            {
                Gizmos.DrawSphere(center + (Vector2)transform.position, gizmoSphereRadius);
            }
        }
    }

    public Vector2 GetCellCenter(int cellX, int cellY)
    {
        float cellWidth = gridSize.x / numCellsX;
        float cellHeight = gridSize.y / numCellsY;

        float x = cellX * cellWidth - gridSize.x / 2 + cellWidth / 2;
        float y = cellY * cellHeight - gridSize.y / 2 + cellHeight / 2;

        return new Vector2(x, y);
    }
    // Функция для размещения объекта в центре указанной ячейки
    public void PlaceObjectInCell(GameObject obj, int cellX, int cellY)
    {
        if (cellX >= 0 && cellX < numCellsX && cellY >= 0 && cellY < numCellsY)
        {
            Vector2 center = GetCellCenter(cellX, cellY);
            obj.transform.position = center + (Vector2)transform.position;
        }
        else
        {
            Debug.LogError("Попытка разместить объект за пределами сетки!");
        }
    }
}