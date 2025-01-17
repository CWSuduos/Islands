using UnityEngine;

public class LvlManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panelPrefabs;
    [SerializeField] private string parentTag; // Тег для поиска родительского объекта
    [SerializeField] private GameObject panelToHide; // Панель, которую нужно удалить
    
    private Transform parentForPanel; // Ссылка на объект родителя
    private LvlCreator lvlCreator;

    private void Start()
    {
        lvlCreator = FindObjectOfType<LvlCreator>();

        if (lvlCreator == null)
        {
            Debug.LogWarning("[LvlManager] LvlCreator нет на сцене!");
        }

        // Ищем родительский объект по тегу
        GameObject parentObject = GameObject.FindGameObjectWithTag(parentTag);
        if (parentObject != null)
        {
            parentForPanel = parentObject.transform;
        }
        else
        {
            Debug.LogError($"[LvlManager] Не найден объект с тегом '{parentTag}'!");
        }
    }

    public void CreatePanel()
    {
        if (panelPrefabs != null && parentForPanel != null && lvlCreator != null)
        {
            int levelID = lvlCreator.GetCurrentLevelID();
            if (levelID >= 0 && levelID < panelPrefabs.Length)
            {
                GameObject panelInstance = Instantiate(panelPrefabs[levelID], parentForPanel);
            }
            else
            {
                Debug.LogWarning("[LvlManager] Выбранный ID панели нет!");
            }
        }
        else
        {
            Debug.LogWarning("[LvlManager] panelPrefabs или parentForPanel не назначены, или LvlCreator не найден!");
        }
    }

    public void OnButtonClick()
    {
        CreatePanel();
    }

    // Новый метод для скрытия указанной панели
    public void HideSpecifiedPanel()
    {
        if (panelToHide != null)
        {
            Destroy(panelToHide); // Уничтожаем указанную панель
            panelToHide = null;   // Обнуляем ссылку, чтобы избежать повторного вызова
        }
        else
        {
            Debug.LogWarning("[LvlManager] Панель для скрытия не назначена или уже удалена!");
        }
    }
}