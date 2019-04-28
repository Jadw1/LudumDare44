using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateAudioLibrary {

    [MenuItem("Assets/Create/AudioLibrary")]
    public static void CreateAsset() {
        AudioLibrary asset = ScriptableObject.CreateInstance<AudioLibrary>();

        AssetDatabase.CreateAsset(asset, "Assets/AudioLibrary/AudioLibrary.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}