using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite ItemSprite;
    public string ItemType;
    public int ItemAmount;
    public string ItemDescription;
}
