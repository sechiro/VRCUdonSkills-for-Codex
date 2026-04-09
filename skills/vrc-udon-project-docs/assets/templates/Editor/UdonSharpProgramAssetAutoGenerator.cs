// MIT License
//
// This template is distributed with MIT-compatible attribution guidance.
//
// Reference source URL:
// https://gist.github.com/nemurigi/dea7c0a1fb94f7b9cf1c36481a459ded
//
// Original author shown on the source page:
// nemurigi
//
// Bundled license text for redistribution:
// ../LICENSES/UdonSharpProgramAssetAutoGenerator.MIT.txt
//
// This repository provides a folder-scoped adaptation template based on that idea.

using System;
using System.IO;
using System.Reflection;
using UdonSharp;
using UdonSharp.Compiler;
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Automatically creates a matching UdonSharp program asset when a
/// UdonSharpBehaviour script is imported under the configured target folders.
/// </summary>
public class UdonSharpProgramAssetAutoGenerator : AssetPostprocessor
{
    // Adjust these roots in the destination project.
    private static readonly string[] TargetRootFolders =
    {
        "Assets/[PATH_TO_MODULE]",
    };

    private static bool IsTargetPath(string assetPath)
    {
        if (string.IsNullOrWhiteSpace(assetPath))
            return false;

        string normalizedPath = assetPath.Replace('\\', '/');

        foreach (string root in TargetRootFolders)
        {
            if (string.IsNullOrWhiteSpace(root))
                continue;

            string normalizedRoot = root.Replace('\\', '/').TrimEnd('/');

            if (normalizedPath.StartsWith(normalizedRoot + "/", StringComparison.Ordinal) ||
                string.Equals(normalizedPath, normalizedRoot, StringComparison.Ordinal))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Reads UdonSharpSettings.autoCompileOnModify via reflection.
    /// </summary>
    private static bool GetAutoCompileOnModify()
    {
        try
        {
            Assembly udonSharpEditorAssembly = typeof(UdonSharpEditorUtility).Assembly;
            Type settingsType = udonSharpEditorAssembly.GetType("UdonSharpEditor.UdonSharpSettings");
            if (settingsType == null)
                return false;

            MethodInfo getSettingsMethod = settingsType.GetMethod("GetSettings", BindingFlags.Public | BindingFlags.Static);
            if (getSettingsMethod == null)
                return false;

            object settingsInstance = getSettingsMethod.Invoke(null, null);
            if (settingsInstance == null)
                return false;

            FieldInfo autoCompileField = settingsType.GetField("autoCompileOnModify", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (autoCompileField == null)
                return false;

            object fieldValue = autoCompileField.GetValue(settingsInstance);
            return fieldValue is bool enabled && enabled;
        }
        catch (Exception ex)
        {
            Debug.LogError($"UdonSharpProgramAssetAutoGenerator: Failed to read UdonSharpSettings.autoCompileOnModify via reflection.\n{ex}");
            return false;
        }
    }

    private static void OnPostprocessAllAssets(
        string[] importedAssets,
        string[] deletedAssets,
        string[] movedAssets,
        string[] movedFromAssetPaths,
        bool didDomainReload)
    {
        if (!didDomainReload)
            return;

        bool createdAnyProgramAsset = false;

        foreach (string importedAssetPath in importedAssets)
        {
            if (!IsTargetPath(importedAssetPath))
                continue;

            if (!importedAssetPath.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                continue;

            MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(importedAssetPath);
            if (script == null)
                continue;

            Type scriptClass = script.GetClass();
            if (scriptClass == null || scriptClass.IsAbstract || !typeof(UdonSharpBehaviour).IsAssignableFrom(scriptClass))
                continue;

            if (UdonSharpEditorUtility.GetUdonSharpProgramAsset(scriptClass) != null)
                continue;

            string programAssetPath = Path.ChangeExtension(importedAssetPath, ".asset")?.Replace('\\', '/');
            if (string.IsNullOrEmpty(programAssetPath) || !programAssetPath.StartsWith("Assets/", StringComparison.Ordinal))
                continue;

            if (AssetDatabase.LoadMainAssetAtPath(programAssetPath) != null)
                continue;

            UdonSharpProgramAsset programAsset = ScriptableObject.CreateInstance<UdonSharpProgramAsset>();
            programAsset.sourceCsScript = script;

            try
            {
                AssetDatabase.CreateAsset(programAsset, programAssetPath);
                AssetDatabase.ImportAsset(programAssetPath, ImportAssetOptions.ForceSynchronousImport);

                if (AssetDatabase.LoadAssetAtPath<UdonSharpProgramAsset>(programAssetPath) == null)
                {
                    Debug.LogError($"UdonSharpProgramAssetAutoGenerator: Failed to create program asset at '{programAssetPath}' for '{importedAssetPath}'.");
                    continue;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"UdonSharpProgramAssetAutoGenerator: Exception while creating program asset at '{programAssetPath}' for '{importedAssetPath}'.\n{ex}");
                continue;
            }

            createdAnyProgramAsset = true;
        }

        if (!createdAnyProgramAsset)
            return;

        AssetDatabase.Refresh();

        if (!GetAutoCompileOnModify())
            return;

        try
        {
            UdonSharpCompilerV1.CompileSync();
        }
        catch (Exception ex)
        {
            Debug.LogError($"UdonSharpProgramAssetAutoGenerator: Compile failed after generating program assets.\n{ex}");
        }
    }
}