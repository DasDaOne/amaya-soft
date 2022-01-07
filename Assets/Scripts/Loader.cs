using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField] private GameObject levelLoaderPrefab;
    [SerializeField] private Vector2Int[] levelDimensions;
    [SerializeField] private GameObject restartButtonCanvas;
    [SerializeField] private GameObject overlapCollider;
    [SerializeField] private Image loadingScreen;
    [SerializeField] private TextMeshProUGUI loadingScreenText;
    [SerializeField] private UnityEvent createdLevelLoaderEvent;
    [SerializeField] private UnityEvent playAnimationEvent;
    private GameObject _levelLoaderGo;
    private LevelLoader _levelLoaderScript;

    private int _level;
    
    private void Start()
    {
        InstantiateLevelLoader();
    }

    public void LoadNextLevel()
    {
        if (_level < levelDimensions.Length)
        {
            _levelLoaderScript.LoadLevel(levelDimensions[_level], _level);
            StartCoroutine(WaitUntilNextFrame());
            if (_level == 0)
            {
                playAnimationEvent.Invoke();
            }

            _level += 1;
        }
        else
        {
            restartButtonCanvas.SetActive(true);
            overlapCollider.SetActive(true);
        }
    }

    public void Restart()
    {
        StartCoroutine(RestartCoroutine());
    }

    public IEnumerator RestartCoroutine()
    {
        _level = 0;
        restartButtonCanvas.SetActive(false);
        yield return new DOTweenCYInstruction.WaitForCompletion(FadeIn());
        yield return StartCoroutine(ReloadLevelLoader());
        yield return new DOTweenCYInstruction.WaitForCompletion(FadeOut());
        overlapCollider.SetActive(false);
    }

    private Sequence FadeIn()
    {
        Sequence fadeInPanel = DOTween.Sequence();
        Sequence fadeInText = DOTween.Sequence();
        fadeInPanel.Append(loadingScreen.DOColor(new Color(255, 255, 255, 0), 0));
        fadeInPanel.Append(loadingScreen.DOColor(new Color(255, 255, 255, 1), 1));
        fadeInText.Append(loadingScreenText.DOColor(new Color(0, 0, 0, 0), 0));
        fadeInText.Append(loadingScreenText.DOColor(new Color(0, 0, 0, 1), 1));
        return fadeInPanel;
    }

    private Sequence FadeOut()
    {
        Sequence fadeOutPanel = DOTween.Sequence();
        Sequence fadeOutText = DOTween.Sequence();
        fadeOutPanel.Append(loadingScreen.DOColor(new Color(255, 255, 255, 1), 0));
        fadeOutPanel.Append(loadingScreen.DOColor(new Color(255, 255, 255, 0), .5f));
        fadeOutText.Append(loadingScreenText.DOColor(new Color(0, 0, 0, 1), 0));
        fadeOutText.Append(loadingScreenText.DOColor(new Color(0, 0, 0, 0), .5f));
        return fadeOutPanel;
    }

    private IEnumerator ReloadLevelLoader()
    {
        Destroy(_levelLoaderGo);
        InstantiateLevelLoader();
        yield return null;
    }

    private void InstantiateLevelLoader()
    {
        _levelLoaderGo = Instantiate(levelLoaderPrefab);
        _levelLoaderScript = _levelLoaderGo.GetComponent<LevelLoader>();
        StartCoroutine(WaitUntilNextFrame());
        LoadNextLevel();
    }

    private IEnumerator WaitUntilNextFrame()
    {
        yield return new WaitForEndOfFrame();
        createdLevelLoaderEvent.Invoke();
    }
}
