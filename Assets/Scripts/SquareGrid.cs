using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SquareGrid : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private float cellSize = 1;

    private int _width;
    private int _height;
    private CellDataBundle _cellDataBundle;
    private string _correctIdentifier;
    private List<string> _identifiers;

    public void SetParameters(int width, int height, CellDataBundle cellDataBundle, List<string> identifiers, string correctIdentifier)
    {
        _width = width;
        _height = height;
        _cellDataBundle = cellDataBundle;
        _correctIdentifier = correctIdentifier;
        _identifiers = identifiers;
    }

    public void Init()
    {
        int indexOfCorrectCell = Random.Range(0, _height * _width - 1);
        
        _identifiers.Remove(_correctIdentifier);
        
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                GameObject cell = Instantiate(cellPrefab, transform);
                cell.transform.position = new Vector3(j * cellSize, i * cellSize, 0);
                Cell cellScript = cell.GetComponent<Cell>();
                if (i * _width + j == indexOfCorrectCell)
                {
                    cellScript.SetCellParameters(Array.Find(_cellDataBundle.CellData, x => x.Identifier == _correctIdentifier)); 
                }
                else
                {
                    int index = Random.Range(0, _identifiers.Count);
                    cellScript.SetCellParameters(Array.Find(_cellDataBundle.CellData, x => x.Identifier == _identifiers[index]));
                    _identifiers.RemoveAt(index); 
                }
                cellScript.Init();
            }
        }

        if (_height > 1)
            transform.position = new Vector3(-_width * cellSize / 2f + cellSize / 2, -_height * cellSize / 2f + cellSize / 2, 0);
        else
            transform.position = new Vector3(-_width * cellSize / 2f + cellSize / 2, 0, 0);
    }
}
