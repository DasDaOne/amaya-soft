using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    public void PlayAnimation()
    {
        Sequence fadeIn = DOTween.Sequence();
        fadeIn.Append(text.DOColor(new Color(255,255,255,0), 0));
        fadeIn.Append(text.DOColor(new Color(255,255,255,1), .5f));
    }
}
