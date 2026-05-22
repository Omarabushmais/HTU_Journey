using UnityEngine;

// This tells the compiler to only compile what's inside if we are in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;

public class EnableGPUInstancing : EditorWindow
{
    [MenuItem("Tools/Enable GPU Instancing On All Materials")]
    static void EnableInstancing()
    {
        string[] guids = AssetDatabase.FindAssets("t:Material");
        int count = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat != null && !mat.enableInstancing)
            {
                mat.enableInstancing = true;
                count++;
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"Enabled GPU Instancing on {count} materials!");
    }
}
#endif