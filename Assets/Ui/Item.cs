using UnityEngine;

public enum ItemType {
    BAUBLE,
    WEAPON,
    SHIELD,
    ARMOR,
    HELMET,
    RING
}

[System.Serializable]
public class Item {
    public int ID;
    public Sprite icon;
    public string name;
    public string description;
    public int buyPrice;
    public int sellPrice;
    public ItemType type;
}