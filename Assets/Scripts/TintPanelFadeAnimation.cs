using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TintPanelFadeAnimation : MonoBehaviour
{
    [SerializeField] private Image image;
    
    private void OnEnable()
    {
        Sequence fade = DOTween.Sequence();
        fade.Append(image.DOColor(new Color(0, 0, 0, 0), 0));
        fade.Append(image.DOColor(new Color(0, 0, 0, .5f), .5f));
    }
}
