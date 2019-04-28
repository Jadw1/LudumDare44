using UnityEngine;

[System.Serializable]
public class ItemData {
    public Sprite icon;
    public string name;
    public string description;
    public int price;
    public string ability;

    public ItemData() {

    }

    public ItemData(Sprite icon, string name, string description, int price, string ability) {
        this.icon = icon;
        this.name = name;
        this.description = description;
        this.price = price;
        this.ability = ability;
    }
}