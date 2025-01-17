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
        // ��������, ��� ��������� ��������
        if (muzik == null)
        {
            Debug.LogError("��������� �� ��������!");
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
        // ������������� ��� �����, ��������������� ����� PlayClipAtPoint
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop();
        }
        isPlaying = false;
    }

    public void LoopMuzik()
    {
        // ���� ����� �� ����� ���� ���������� �������� ����� PlayClipAtPoint,
        // ��� ��� �� �� ������������ ��������� ���������������.
        Debug.LogWarning("Loop �� �������������� ��� ������������� PlayClipAtPoint.");
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