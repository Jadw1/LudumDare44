using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRegistry : MonoBehaviour {
    private static ItemRegistry INSTANCE = null;

    public static ItemRegistry GetInstance() {
        if (INSTANCE == null) {
            INSTANCE = (ItemRegistry) FindObjectOfType(typeof(ItemRegistry));
        }

        return INSTANCE;
    }
    
    [SerializeField]
    private Item[] items;

    [SerializeField]
    private GameObject realObjectPrefab;

    public Item GetItem(int id) {
        if (id < 0 || id > items.Length) {
            return null;
        }
        
        return items[id];
    }

    public void 
}