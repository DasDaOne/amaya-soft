using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Loader : MonoBehaviour
{
    [SerializeField] private GameObject levelLoaderPrefab;
    [SerializeField] private Vector2Int[] levelDimensions;
    [SerializeField] private GameObject restartButtonCanvas;
    [SerializeField] private GameObject overlapCollider;
    [SerializeField] private LoadingScreenAnimation loadingScreenAnimationController;
    [SerializeField] private UnityEvent playStartAnimationEvent;
    private GameObject _levelLoaderGo;
    private LevelLoader _levelLoaderScript;
    private int _level;
    
    private void Start()
    {
        InstantiateLevelLoader();
    }

    public void AddStartAnimationListener(UnityAction action)
    {
        playStartAnimationEvent.AddListener(action);
    }

    public void LoadNextLevel()
    {
        if (_level < levelDimensions.Length)
        {
            _levelLoaderScript.LoadLevel(levelDimensions[_level]);
            if (_level == 0)
            {
                playStartAnimationEvent.Invoke();
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

    private IEnumerator RestartCoroutine()
    {
        _level = 0;
        restartButtonCanvas.SetActive(false);
        yield return new DOTweenCYInstruction.WaitForCompletion(loadingScreenAnimationController.FadeIn());
        yield return StartCoroutine(ReloadLevelLoader());
        yield return new DOTweenCYInstruction.WaitForCompletion(loadingScreenAnimationController.FadeOut());
        overlapCollider.SetActive(false);
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
        LoadNextLevel();
    }
    
}
