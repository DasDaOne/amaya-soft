using UnityEngine;

[CreateAssetMenu(fileName = "New CellDataBundle", menuName = "Cell/Cell Bundle Data", order = 120)]
public class CellDataBundle : ScriptableObject // класс представляющий собой набор из CellData
{
    [SerializeField] private CellData[] cellData;
    
    public CellData[] CellData => cellData;
}
