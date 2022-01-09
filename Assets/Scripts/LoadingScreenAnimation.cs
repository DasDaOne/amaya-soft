using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenAnimation : MonoBehaviour
{
    [SerializeField] private Image loadingScreen;
    [SerializeField] private TextMeshProUGUI loadingScreenText;
    
    public Sequence FadeIn()
    {
        Sequence fadeInPanel = DOTween.Sequence();
        Sequence fadeInText = DOTween.Sequence();
        fadeInPanel.Append(loadingScreen.DOColor(new Color(255, 255, 255, 0), 0));
        fadeInPanel.Append(loadingScreen.DOColor(new Color(255, 255, 255, 1), 1));
        fadeInText.Append(loadingScreenText.DOColor(new Color(0, 0, 0, 0), 0));
        fadeInText.Append(loadingScreenText.DOColor(new Color(0, 0, 0, 1), 1));
        return fadeInPanel;
    }

    public Sequence FadeOut()
    {
        Sequence fadeOutPanel = DOTween.Sequence();
        Sequence fadeOutText = DOTween.Sequence();
        fadeOutPanel.Append(loadingScreen.DOColor(new Color(255, 255, 255, 1), 0));
        fadeOutPanel.Append(loadingScreen.DOColor(new Color(255, 255, 255, 0), .5f));
        fadeOutText.Append(loadingScreenText.DOColor(new Color(0, 0, 0, 1), 0));
        fadeOutText.Append(loadingScreenText.DOColor(new Color(0, 0, 0, 0), .5f));
        return fadeOutPanel;
    }
}
