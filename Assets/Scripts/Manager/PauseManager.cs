using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; // Ссылка на панель паузы
    private LvlWinManager lvlWinManager;
    private bool isPaused = false; // Флаг, отслеживающий состояние паузы
    private GameObject gamePanel;
    private void Update()
    {
        lvlWinManager = FindObjectOfType<LvlWinManager>();
        gamePanel = lvlWinManager.gamePanel;
        // Пример: Пауза активируется по нажатию клавиши Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ClosePausePanel();
            }
            else
            {
                OpenPausePanel();
            }
        }
    }

 
    public void OpenPausePanel()
    {
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;
    }

 
    public void ClosePausePanel()
    {
        pausePanel.SetActive(false); 
        gamePanel.SetActive(true);
        Time.timeScale = 1f; 
        isPaused = false; 
    }
}