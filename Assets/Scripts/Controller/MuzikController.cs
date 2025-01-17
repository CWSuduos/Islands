using UnityEngine;
using UnityEngine.UI;

public class MuzikController : MonoBehaviour
{
    [SerializeField] private AudioClip muzik;
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Sprite pauseSprite;

    private bool isPlaying = false;

    private void Start()
    {
        // Убедимся, что аудиоклип назначен
        if (muzik == null)
        {
            Debug.LogError("Аудиоклип не назначен!");
        }
    }

    public void PlayMuzik()
    {
        if (!isPlaying)
        {
            AudioSource.PlayClipAtPoint(muzik, Camera.main.transform.position);
            isPlaying = true;
        }
    }

    public void StopMuzik()
    {
        // Останавливаем все аудио, воспроизводимое через PlayClipAtPoint
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop();
        }
        isPlaying = false;
    }

    public void LoopMuzik()
    {
        // Этот метод не может быть реализован напрямую через PlayClipAtPoint,
        // так как он не поддерживает цикличное воспроизведение.
        Debug.LogWarning("Loop не поддерживается при использовании PlayClipAtPoint.");
    }

    public void OnButtonClick(Button button)
    {
        if (isPlaying)
        {
            StopMuzik();
            button.image.sprite = playSprite;
        }
        else
        {
            PlayMuzik();
            button.image.sprite = pauseSprite;
        }
    }
}