using UnityEngine;

[CreateAssetMenu(fileName = "New CellData", menuName = "Cell/Cell Data", order = 130)]
public class CellData : ScriptableObject // класс где хранится информация о ячейке
{
    [SerializeField] private string identifier;

    [SerializeField] private Sprite sprite;

    [SerializeField] private float rotation;
    
    public string Identifier => identifier;
    public Sprite Sprite => sprite;
    public float Rotation => rotation;
}
