using UnityEngine;

[System.Serializable]
public class ItemData {
    public Sprite icon;
    public string name;
    public string description;
    public int price;
    public Ability ability = new AbilityShieldBash();
}