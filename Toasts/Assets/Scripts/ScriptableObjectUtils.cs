using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static partial class ScriptableObjectUtils
{
    public static void CreateAsset(ScriptableObject instance, string className)
    {
#if UNITY_EDITOR
        string folderPath = Path.Combine("Assets", "Data");

        string directoryPath = Path.Combine(Application.dataPath, "DebugData");

        directoryPath = Path.Combine(directoryPath, "resources");

        if (!Directory.Exists(directoryPath))
        {
            AssetDatabase.CreateFolder(folderPath, "resources");
        }

        string assetPath = Path.Combine(folderPath, "resources");

        assetPath = Path.Combine(assetPath, string.Concat(className, ".asset"));

        AssetDatabase.CreateAsset(instance, assetPath);
#endif
    }

    public static string ColorToHex(Color32 color)
    {
        return string.Concat("#", color.r.ToString("X2"), color.g.ToString("X2"), color.b.ToString("X2"));
    }

}
