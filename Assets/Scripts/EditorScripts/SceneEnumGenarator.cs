using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

public static class SceneEnumGenerator
{
    private const string enumName = "SceneList";
    private const string filePath = "Assets/Scripts/Generated/SceneListEnum.cs"; // change path as needed

    [MenuItem("Tools/Generate Scene Enum")]
    public static void Generate()
    {
        // Grab all enabled scenes from build settings
        var scenes = EditorBuildSettings.scenes
            .Where(s => s.enabled)
            .Select(s => Path.GetFileNameWithoutExtension(s.path))
            .Distinct()
            .ToList();

        if (scenes.Count == 0)
        {
            Debug.LogWarning("No scenes found in Build Settings.");
            return;
        }

        // Build the enum as text
        var enumCode = "public enum " + enumName + "\n{\n";
        foreach (var scene in scenes)
        {
            // Make sure name is valid identifier
            var safeName = scene.Replace(" ", "_");
            enumCode += $"    {safeName},\n";
        }
        enumCode += "}";

        // Ensure directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        // Write file
        File.WriteAllText(filePath, enumCode);

        // Refresh Unity
        AssetDatabase.Refresh();

        Debug.Log($"Scene enum generated at {filePath}");
    }
}
