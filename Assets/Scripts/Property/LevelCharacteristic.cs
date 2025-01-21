using UnityEngine;
using UnityEngine.UI;

public class LevelCharacteristic : MonoBehaviour
{
    [Header("���������� ������������� ������")]
    [SerializeField] private int levelID;

    [Header("������ ����������� ������")]
    [SerializeField] private bool isCompleted;

    [Header("��� ��������� ������� � Image")]
    [SerializeField] private string imageObjectName = "LevelImage";

    [Header("��� ��������� ������� � Text")]
    [SerializeField] private string textObjectName = "LevelText";

    [Header("������ ��� ������������ ������")]
    [SerializeField] private Sprite completedSprite;

    [Header("������ ��� �������������� ������")]
    [SerializeField] private Sprite incompleteSprite;

    [Header("���� ������ ��� ������������ ������")]
    [SerializeField] private Color completedTextColor = Color.green;

    [Header("���� ������ ��� �������������� ������")]
    [SerializeField] private Color incompleteTextColor = Color.white;

    [Header("������ ��� ������������ ������")]
    [SerializeField] private Sprite completedStarSprite;

    [Header("������ ��� �������������� ������")]
    [SerializeField] private Sprite incompleteStarSprite;


    private Image levelImage;
    private Text levelText;
    private Image[] starImages;

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

        starImages = GetComponentsInChildren<Image>(true);

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        levelImage.sprite = isCompleted ? completedSprite : incompleteSprite;
        levelText.color = isCompleted ? completedTextColor : incompleteTextColor;

        Sprite starSprite = isCompleted ? completedStarSprite : incompleteStarSprite;

        foreach (Image starImage in starImages)
        {
            if (starImage != levelImage && starImage.gameObject.name.Contains("Star"))
            {
                starImage.sprite = starSprite;
            }
        }
    }
}