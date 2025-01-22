using UnityEngine;
using UnityEngine.UI;

public class MuzikButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        MuzikController.Instance.OnButtonClick(button);
    }

    public void UpdateButtonState()
    {
        if (MuzikController.Instance.isPlaying)
        {
            button.image.sprite = MuzikController.Instance.pauseSprite;
        }
        else
        {
            button.image.sprite = MuzikController.Instance.playSprite;
        }
    }
}