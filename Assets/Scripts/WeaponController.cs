using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponController
{
    [MenuItem("Assets/Create/Weapon Object")]
    public static void Create()
    {
        NewGuns asset = ScriptableObject.CreateInstance<NewGuns>();
        AssetDatabase.CreateAsset(asset, "Assets/Weapons/NewWeapon.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

}
