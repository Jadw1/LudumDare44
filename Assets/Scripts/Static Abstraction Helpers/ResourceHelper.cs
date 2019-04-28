using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResourceHelper {
    
    private static Hashtable resources = CreateResourceTable();

    private static Hashtable CreateResourceTable() {
        Hashtable table = new Hashtable();

        table.Add("ability_shield_bash", AssetDatabase.LoadAssetAtPath("Assets/Textures/ability icons/shield-bash.png", typeof(Sprite)));

        return table;
    }
    
    public static Sprite GetSprite(string name) {
        return resources[name] as Sprite;
    }
}