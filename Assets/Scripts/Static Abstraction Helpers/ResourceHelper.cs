using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResourceHelper {
    
    private static Hashtable resources = CreateResourceTable();

    private static Hashtable CreateResourceTable() {
        Hashtable table = new Hashtable();

        #region Sprites
        table.Add("sprite_ability_shield_bash", AssetDatabase.LoadAssetAtPath("Assets/Textures/ability icons/shield-bash.png", typeof(Sprite)));
        #endregion

        #region AudioClips
        table.Add("audioclip_click", AssetDatabase.LoadAssetAtPath("Assets/Kenney/UI Sounds/click3.wav", typeof(AudioClip)));
        table.Add("audioclip_rollover", AssetDatabase.LoadAssetAtPath("Assets/Kenney/UI Sounds/rollover1.wav", typeof(AudioClip)));
        table.Add("audioclip_switch", AssetDatabase.LoadAssetAtPath("Assets/Kenney/UI Sounds/switch24.wav", typeof(AudioClip)));
        table.Add("audioclip_switch_2", AssetDatabase.LoadAssetAtPath("Assets/Kenney/UI Sounds/switch36.wav", typeof(AudioClip)));

        table.Add("audioclip_bow", AssetDatabase.LoadAssetAtPath("Assets/Sounds/Battle Sounds/Bow.wav", typeof(AudioClip)));
        table.Add("audioclip_swish_1", AssetDatabase.LoadAssetAtPath("Assets/Sounds/Battle Sounds/swish_2.wav", typeof(AudioClip)));
        table.Add("audioclip_swish_2", AssetDatabase.LoadAssetAtPath("Assets/Sounds/Battle Sounds/swish_3.wav", typeof(AudioClip)));
        table.Add("audioclip_swish_3", AssetDatabase.LoadAssetAtPath("Assets/Sounds/Battle Sounds/swish_4.wav", typeof(AudioClip)));
        #endregion

        return table;
    }
    
    public static Sprite GetSprite(string name) {
        return resources["sprite_" + name] as Sprite;
    }

    public static AudioClip GetAudioClip(string name) {
        return resources["audioclip_" + name] as AudioClip;
    }
}