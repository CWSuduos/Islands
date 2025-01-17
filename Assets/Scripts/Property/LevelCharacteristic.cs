using UnityEngine;
using UnityEngine.UI;

public class LevelCharacteristic : MonoBehaviour
{
    [Header("Уникальный идентификатор уровня")]
    [SerializeField]
    private int levelID;

    [Header("Статус прохождения уровня")]
    [SerializeField]
    private bool isCompleted;

    [Header("Имя дочернего объекта с Image")]
    [SerializeField]
    private string imageObjectName = "LevelImage";

    [Header("Имя дочернего объекта с Text")]
    [SerializeField]
    private string textObjectName = "LevelText";

    [Header("Спрайт для завершенного уровня")]
    [SerializeField]
    private Sprite completedSprite;

    [Header("Спрайт для незавершенного уровня")]
    [SerializeField]
    private Sprite incompleteSprite;

    [Header("Цвет текста для завершенного уровня")]
    [SerializeField]
    private Color completedTextColor = Color.green;

    [Header("Цвет текста для незавершенного уровня")]
    [SerializeField]
    private Color incompleteTextColor = Color.white;

    private Image levelImage;
    private Text levelText;


    public int LevelID
    {
        get => levelID;
        set => levelID = value;
    }

    public bool IsCompleted
    {
        get => isCompleted;
        set
        {
            isCompleted = value;
            UpdateVisuals();
        }
    }

    private void Start()
    {
        // Находим дочерние объекты по имени
        Transform imageTransform = transform.Find(imageObjectName);
        Transform textTransform = transform.Find(textObjectName);

        if (imageTransform == null)
        {
            Debug.LogError($"Дочерний объект с именем '{imageObjectName}' не найден!");
            return;
        }

        if (textTransform == null)
        {
            Debug.LogError($"Дочерний объект с именем '{textObjectName}' не найден!");
            return;
        }


        levelImage = imageTransform.GetComponent<Image>();
        levelText = textTransform.GetComponent<Text>();


        if (levelImage == null)
        {
            Debug.LogError($"Компонент Image не найден на объекте '{imageObjectName}'!");
            return;
        }

        if (levelText == null)
        {
            Debug.LogError($"Компонент Text не найден на объекте '{textObjectName}'!");
            return;
        }

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (isCompleted)
        {
            levelImage.sprite = completedSprite;
            levelText.color = completedTextColor;
        }
        else
        {
            levelImage.sprite = incompleteSprite;
            levelText.color = incompleteTextColor;
        }
    }
}