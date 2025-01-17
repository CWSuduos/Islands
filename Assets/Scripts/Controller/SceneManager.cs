using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private GameObject panelToShow;
    [SerializeField] private GameObject panelToHide;

    public void OnButtonClick()
    {
        if (panelToShow != null)
        {
            panelToShow.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Panel to show is not assigned!");
        }

        if (panelToHide != null)
        {
            panelToHide.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Panel to hide is not assigned!");
        }
    }
}