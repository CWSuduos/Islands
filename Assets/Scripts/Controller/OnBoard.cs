using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OnBoard : MonoBehaviour
{
    [SerializeField] private List<GameObject> onboardingPanels;
    [SerializeField] private Text panelIndicator;

    private int currentIndex = 0;

    private void Start()
    {
        if (onboardingPanels != null)
        {
            foreach (var panel in onboardingPanels)
            {
                panel.SetActive(true);
            }
        }
    }

    private void ShowPanel(int index)
    {
        if (index >= 0 && index < onboardingPanels.Count)
        {
            var panelToShow = onboardingPanels[index];
            panelToShow.SetActive(true);
            panelToShow.transform.localPosition = Vector3.zero;
            if (panelIndicator != null)
                panelIndicator.text = (index + 1) + "/" + onboardingPanels.Count;
            
        }
    }

    public void NextPanel()
    {
        if (currentIndex < onboardingPanels.Count)
        {
            int oldIndex = currentIndex;
            currentIndex++;
            if (currentIndex < onboardingPanels.Count)
                ShowPanel(currentIndex);
            else
                if (panelIndicator != null && panelIndicator.transform.parent != null)
                    panelIndicator.transform.parent.gameObject.SetActive(false);
            GameObject oldPanel = onboardingPanels[oldIndex];
            Animate(oldPanel, 1f, -2000f);
        }
    }

    public static void Animate(
        GameObject panel,
        float duration = 0.1f,
        float flyOffset = -2000f,
        Action onComplete = null)
    {
        if (panel == null) return;

        panel.transform.DOLocalMoveX(flyOffset, duration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                panel.SetActive(false);
                onComplete?.Invoke();
            });
    }
}