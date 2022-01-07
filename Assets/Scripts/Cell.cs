using DG.Tweening;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject mainSpriteHolder;
    [SerializeField] private ParticleSystem particles;
    private CellData _cellData;
    private AnswerChecker _answerChecker;
    
    public void SetCellParameters(CellData cellData)
    {
        _cellData = cellData;
    }

    public void Init()
    {
        _answerChecker = FindObjectOfType<AnswerChecker>();
        spriteRenderer.sprite = _cellData.Sprite;
        spriteRenderer.transform.Rotate(new Vector3(0, 0, _cellData.Rotation));
    }

    public void PlayBounceAnimation()
    {
        Sequence bounce = DOTween.Sequence();
        bounce.Append(transform.DOScale(0, 0));
        bounce.Append(transform.DOScale(1.2f, .15f));
        bounce.Append(transform.DOScale(.95f, .15f));
        bounce.Append(transform.DOScale(1f, .15f));
    }

    private void PlayCorrectAnswerAnimation()
    {
        particles.Play();
        Sequence bounce = DOTween.Sequence();
        bounce.Append(mainSpriteHolder.transform.DOScale(1.2f, .15f));
        bounce.Append(mainSpriteHolder.transform.DOScale(.95f, .15f));
        bounce.Append(mainSpriteHolder.transform.DOScale(1f, .15f));
    }
    
    private void PlayIncorrectAnswerAnimation()
    {
        Sequence easeInBounce = DOTween.Sequence();
        easeInBounce.Append(mainSpriteHolder.transform.DOLocalMoveX(-.05f, .1f));
        easeInBounce.Append(mainSpriteHolder.transform.DOLocalMoveX(.1f, .1f));
        easeInBounce.Append(mainSpriteHolder.transform.DOLocalMoveX(-.15f, .1f));
        easeInBounce.Append(mainSpriteHolder.transform.DOLocalMoveX(.2f, .1f));
        easeInBounce.Append(mainSpriteHolder.transform.DOLocalMoveX(0f, .1f));
    }

    private void OnMouseDown()
    {
        if(_answerChecker.CheckAnswer(_cellData.Identifier))
            PlayCorrectAnswerAnimation();
        else
        {
            PlayIncorrectAnswerAnimation();
        }
    }
}
