using UnityEngine;

[CreateAssetMenu(fileName = "New CellDataBundle", menuName = "Cell/Cell Bundle Data", order = 120)]
public class CellDataBundle : ScriptableObject
{
    [SerializeField] private CellData[] cellData;
    
    public CellData[] CellData => cellData;
}
