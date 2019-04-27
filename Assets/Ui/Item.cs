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
    public Sprite icon;
    public string name;
    public string description;
    public int price;
    public ItemType type;
}