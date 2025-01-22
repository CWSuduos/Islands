using UnityEngine;
using UnityEngine.UI;

public class MuzikController : MonoBehaviour
{
    [SerializeField] private AudioClip muzik;
    [SerializeField] public Sprite playSprite;
    [SerializeField] public Sprite pauseSprite;

    private static MuzikController instance;
    public bool isPlaying = false;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (muzik == null)
        {
            Debug.LogError("Аудиоклип не назначен!");
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = muzik;
        audioSource.loop = true;
        audioSource.Play();
        isPlaying = true;
    }

    public static MuzikController Instance
    {
        get { return instance; }
    }

    public void OnButtonClick(Button button)
    {
        if (isPlaying)
        {
            audioSource.Stop();
            button.image.sprite = playSprite;
            isPlaying = false;
            UpdateAllButtons();
        }
        else
        {
            audioSource.Play();
            button.image.sprite = pauseSprite;
            isPlaying = true;
            UpdateAllButtons();
        }
    }

    private void UpdateAllButtons()
    {
        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button button in allButtons)
        {
            MuzikButton muzikButton = button.GetComponent<MuzikButton>();
            if (muzikButton != null)
            {
                muzikButton.UpdateButtonState();
            }
        }
    }
}