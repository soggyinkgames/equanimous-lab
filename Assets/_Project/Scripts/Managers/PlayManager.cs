using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

// an example script to visualize some element data in the play manager inspector drawer to edit the ui in ui builder
namespace SoggyInkGames.Equanimous.Lab.Managers
{
    [RequireComponent(typeof(UIDocument))]
    public class PlayManager : MonoBehaviour
    {

        // [Header("This is a Header")]
        // public float m_ElementRandomRange = 20;
        // [Range(10, 40)] public float m_ElementForce = 25;
        // public int m_ElementCount = 10;
        // public float m_ElementDelay = 0.1f;
        // public GameObject m_ElementPrefab;

        // public ElementManager[] m_Elements;


        [HideInInspector] public List<string> m_Prefix = new() { "T_", "M_", "SDF_", "VFX_", "SH_", "SHG_", "PS_", "TER_", "MESH_", "none" };
        public List<string> m_SuffixChoices = new() { "_E", "_I", "_A", "_F", "none" };
        [HideInInspector] public List<string> textureSuffixChoices = new() { "_D", "_Normal", "_Roughness", "_AlphaOpacity", "_AmbientOcclusion", "_Bump", "_Emissive", "_Mask", "_Specular", "_Particle", "none" };
        [HideInInspector] public static List<string> styleList;
        [HideInInspector] public Object m_SelectedAsset;

        // [HideInInspector] public Object[] m_SelectedAssets;

        // todo: add tests
        //todo: replace dubugs with logic to rename assets using paths
        //todo: add ability to rename multiple at same time/use list of objects instead


        static void AllPathsForType(string type)
        {
            var allPaths = AssetDatabase.FindAssets($"t: {type}");

            foreach (var guid in allPaths)
            {
                AssetDatabase.GUIDToAssetPath(guid);
                // Debug.Log($"AllPathsForType ==>>{path}");
            }

        }

        static string PathForSelected(string selected)
        {

            string[] selectedAssetGuids = AssetDatabase.FindAssets(selected);
            if (selectedAssetGuids.Length == 0) return null;

            string selectedAssetPath = AssetDatabase.GUIDToAssetPath(selectedAssetGuids[0]);

            return selectedAssetPath;
        }

        private string ValidateSelectedAssetType(object m_SelectedAsset, string newPrefix, string extension)
        {
            var assetTypeValidator = new Services.AssetTypeValidator();

            var validatedPrefix = assetTypeValidator.GetValidPrefix(m_SelectedAsset, extension);
            if (newPrefix != validatedPrefix)
            {
                Debug.LogError($"PREFIX SELECTED-`{newPrefix}` IS NOT A VALID SUFFIX TYPE CHOICE FOR THE SELECTED ASSET-!! TRY AGAIN");
                return null;
            }
            return validatedPrefix;
        }


        private string ValidateSelectedSuffix(List<string> m_SuffixChoices, List<string> m_TextureSuffixChoices, string suffixValue, string validatedPrefix)
        {
            var suffixChoicesValidator = new Services.AssetTypeValidator();

            var validatedSuffix = suffixChoicesValidator.GetValidSuffix(m_SuffixChoices, m_TextureSuffixChoices, suffixValue, validatedPrefix);
            if (suffixValue != validatedSuffix)
            {
                Debug.LogError($"SUFFIX SELECTED-`{suffixValue}` IS NOT A VALID SUFFIX TYPE CHOICE FOR THE SELECTED ASSET!! TRY AGAIN");
                return null;
            }
            return validatedSuffix;
        }


        string RemoveFolderPrefixInAssetName(string currentAssetName)
        {
            if (currentAssetName.StartsWith("Shader Graphs/"))
            {
                string[] parts = currentAssetName.Split('/');
                string newAssetName = parts[^1]; // index operator
                return newAssetName;
            }
            return currentAssetName;
        }


        public void RenameAsset(string suffixValue, string newPrefix)
        {
            var currentAssetName = m_SelectedAsset.name;
            string assetNameAlone = RemoveFolderPrefixInAssetName(currentAssetName);

            string file = PathForSelected(assetNameAlone);
            var _extension = Path.GetExtension(file);

            string validatedPrefix = ValidateSelectedAssetType(m_SelectedAsset, newPrefix, _extension);
            string validatedSuffix = ValidateSelectedSuffix(m_SuffixChoices, textureSuffixChoices, suffixValue, validatedPrefix);

            NothingSelected(assetNameAlone, validatedPrefix, validatedSuffix);
            RequiresPrefixAndSuffix(file, assetNameAlone, validatedPrefix, validatedSuffix, _extension);
            RequiresPrefixOnly(file, assetNameAlone, validatedPrefix, validatedSuffix, _extension);
            RequiresSuffixOnly(file, assetNameAlone, validatedPrefix, validatedSuffix, _extension);
        }

        private string NothingSelected(string assetNameAlone, string validatedPrefix, string validatedSuffix)
        {
            if ((validatedPrefix == "none" && validatedSuffix == "none") || (string.IsNullOrEmpty(validatedPrefix) && string.IsNullOrEmpty(validatedSuffix)))
            {
                Debug.Log("Please select a prefix or suffix or both to rename asset YOU SILLY BITCH!");
            }
            return assetNameAlone;
        }

        private string RequiresPrefixAndSuffix(string file, string assetNameAlone, string validatedPrefix, string validatedSuffix, string _extension)
        {
            if (validatedPrefix != "none" && validatedSuffix != "none")
            {
                if (!assetNameAlone.StartsWith(validatedPrefix) && !assetNameAlone.EndsWith(validatedSuffix))
                {
                    AddPrefixAndSuffix(file, assetNameAlone, validatedPrefix, validatedSuffix, _extension);
                }
                return assetNameAlone;
            }
            return assetNameAlone;
        }

        private string RequiresPrefixOnly(string file, string assetNameAlone, string validatedPrefix, string validatedSuffix, string _extension)
        {
            if (!string.IsNullOrEmpty(validatedPrefix) && validatedSuffix == "none")
            {
                if (!assetNameAlone.StartsWith(validatedPrefix) && (assetNameAlone.EndsWith(validatedSuffix) || validatedSuffix == "none"))
                {
                    AddPrefix(file, validatedPrefix, assetNameAlone, _extension);
                }
                if (validatedSuffix == "none" && assetNameAlone.StartsWith(validatedPrefix))
                {
                    Debug.Log($"REMOVE SUFFIX {assetNameAlone}");
                    // fix here
                    return assetNameAlone;
                }
            }
            return assetNameAlone;
        }

        private string RequiresSuffixOnly(string file, string assetNameAlone, string validatedPrefix, string validatedSuffix, string _extension)
        {
            if (!string.IsNullOrEmpty(validatedSuffix) && validatedSuffix != "none")
            {
                if ((assetNameAlone.StartsWith(validatedPrefix) && !assetNameAlone.EndsWith(validatedSuffix)) || validatedPrefix == "none")
                {
                    AddSuffix(file, validatedSuffix, assetNameAlone, _extension);
                }
                return assetNameAlone;
            }
            return assetNameAlone;
        }


        private void AddPrefixAndSuffix(string file, string assetNameAlone, string validatedPrefix, string validatedSuffix, string _extension)
        {
            string newFilePath = Path.Combine(Path.GetDirectoryName(file), $"{validatedPrefix}{assetNameAlone}{validatedSuffix}{_extension}");
            Debug.Log($"ADDED BOTH-{newFilePath}");
            Renamed(file, newFilePath);
            AssetDatabase.Refresh();
        }

        private void AddPrefix(string file, string newPrefix, string assetNameAlone, string _extension)
        {
            string newFilePath = Path.Combine(Path.GetDirectoryName(file), $"{newPrefix}{assetNameAlone}{_extension}");
            Debug.Log($"ADDED PREFIX ONLY-{newFilePath}");
            Renamed(file, newFilePath);
            AssetDatabase.Refresh();
        }

        private void AddSuffix(string file, string validatedSuffix, string assetNameAlone, string _extension)
        {
            string newFilePath = Path.Combine(Path.GetDirectoryName(file), $"{assetNameAlone}{validatedSuffix}{_extension}");
            Debug.Log($"ADDED SUFFIX ONLY-{newFilePath}");
            Renamed(file, newFilePath);
            AssetDatabase.Refresh();
        }

        private static void Renamed(string file, string newFilePath)
        {
            File.Move(file, newFilePath);
            AssetDatabase.Refresh();
            Debug.Log("Asset renaming complete.");
        }

    }
}
