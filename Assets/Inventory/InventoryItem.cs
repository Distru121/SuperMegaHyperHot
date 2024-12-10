using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public int restoreValue;
    public Mesh itemmesh;
    [Tooltip ("tipo dell'oggetto")]
    public ItemType itemType;
}

public enum ItemType
{
    Health,
    Mana
}
