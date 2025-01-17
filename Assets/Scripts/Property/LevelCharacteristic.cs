using UnityEngine;
using UnityEngine.UI;

public class LevelCharacteristic : MonoBehaviour
{
    [Header("���������� ������������� ������")]
    [SerializeField]
    private int levelID;

    [Header("������ ����������� ������")]
    [SerializeField]
    private bool isCompleted;

    [Header("��� ��������� ������� � Image")]
    [SerializeField]
    private string imageObjectName = "LevelImage";

    [Header("��� ��������� ������� � Text")]
    [SerializeField]
    private string textObjectName = "LevelText";

    [Header("������ ��� ������������ ������")]
    [SerializeField]
    private Sprite completedSprite;

    [Header("������ ��� �������������� ������")]
    [SerializeField]
    private Sprite incompleteSprite;

    [Header("���� ������ ��� ������������ ������")]
    [SerializeField]
    private Color completedTextColor = Color.green;

    [Header("���� ������ ��� �������������� ������")]
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
        // ������� �������� ������� �� �����
        Transform imageTransform = transform.Find(imageObjectName);
        Transform textTransform = transform.Find(textObjectName);

        if (imageTransform == null)
        {
            Debug.LogError($"�������� ������ � ������ '{imageObjectName}' �� ������!");
            return;
        }

        if (textTransform == null)
        {
            Debug.LogError($"�������� ������ � ������ '{textObjectName}' �� ������!");
            return;
        }


        levelImage = imageTransform.GetComponent<Image>();
        levelText = textTransform.GetComponent<Text>();


        if (levelImage == null)
        {
            Debug.LogError($"��������� Image �� ������ �� ������� '{imageObjectName}'!");
            return;
        }

        if (levelText == null)
        {
            Debug.LogError($"��������� Text �� ������ �� ������� '{textObjectName}'!");
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