using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private CellDataBundle cellDataBundle;
    private SquareGrid _squareGrid;
    private List<string> _identifiers;
    private string _correctIdentifier;
    private GameObject _grid;
    private AnswerChecker _answerChecker;
    

    private void Awake()
    {
        _identifiers = GetAllIdentifiers();
        _answerChecker = GameObject.Find("GameManager").GetComponent<AnswerChecker>();
    }

    public void LoadLevel(Vector2Int levelDimensions, int levelId)
    {
        Destroy(_grid);
        
        int index = Random.Range(0, _identifiers.Count - 1);
        _correctIdentifier = _identifiers[index];
        _identifiers.RemoveAt(index);

        _answerChecker.SetIdentifier(_correctIdentifier);
        
        _grid = Instantiate(gridPrefab);
        _squareGrid = _grid.GetComponent<SquareGrid>();
        _squareGrid.SetParameters(levelDimensions.x, levelDimensions.y, cellDataBundle, GetAllIdentifiers(), levelId, _correctIdentifier);
        _squareGrid.Init();
    }

    public List<string> GetAllIdentifiers()
    {
        List<string> identifiers = new List<string>();
        
        foreach (var cellData in cellDataBundle.CellData)
        {
            identifiers.Add(cellData.Identifier);
        }

        return identifiers;
    }

    private void OnDestroy()
    {
        Destroy(_grid);
    }
}
