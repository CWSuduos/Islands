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
        
        if (muzik == null)
        {
            Debug.LogError("Аудиоклип не назначен!");
        }
        PlayMuzik();
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
        
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop();
        }
        isPlaying = false;
    }

    public void LoopMuzik()
    {
      
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