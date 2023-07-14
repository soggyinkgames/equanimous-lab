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
        [HideInInspector] public List<string> m_TextureSuffixChoices = new() { "_D", "_Normal", "_Roughness", "_AlphaOpacity", "_AmbientOcclusion", "_Bump", "_Emissive", "_Mask", "_Specular", "_Particle", "none" };
        [HideInInspector] public Object m_SelectedAsset;
        // [HideInInspector] public Object[] m_SelectedAssets;

        // TODO: use validation to style uss error ui
        // TODO: add suffix validation on textures or other
        //todo: add ability to rename multiple at same time/use list of objects instead
        //todo: replace dubugs with logic to rename assets using paths
        //TODO: ADD TOOLTIPS WITH FULL VALUE NAMES


        static void AllPathsForType(string type)
        {
            var allPaths = AssetDatabase.FindAssets($"t: {type}");

            foreach (var guid in allPaths)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                Debug.Log($"AllPathsForType ==>>{path}");
            }

        }

        static string PathForSelected(string selected)
        {

            string[] selectedAssetGuids = AssetDatabase.FindAssets(selected);
            if (selectedAssetGuids.Length == 0) return null;

            string selectedAssetPath = AssetDatabase.GUIDToAssetPath(selectedAssetGuids[0]);

            Debug.Log($"PathForSelected----->>{selectedAssetPath}");
            return selectedAssetPath;
        }

        private static string ValidateSelectedAssetType(object m_SelectedAsset, string newPrefix, string extension)
        {
            var assetTypeValidator = new Services.AssetTypeValidator();

            var validatedPrefix = assetTypeValidator.GetValidPrefix(m_SelectedAsset, extension);
            if (newPrefix != validatedPrefix)
            {
                Debug.LogError($"PREFIX SELECTED-{newPrefix}DOES NOT MATCH ASSET TYPE-{validatedPrefix}!! TRY AGAIN");
                return null;
            }
            return validatedPrefix;
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
            Debug.Log($"----- fileExtension-----> {_extension}");

            string validatedPrefix = ValidateSelectedAssetType(m_SelectedAsset, newPrefix, _extension);

            NothingSelected(assetNameAlone, validatedPrefix, suffixValue);
            RequiresPrefixAndSuffix(assetNameAlone, validatedPrefix, suffixValue);
            RequiresPrefixOnly(assetNameAlone, validatedPrefix, suffixValue);
            RequiresSuffixOnly(assetNameAlone, validatedPrefix, suffixValue);
        }

        private string NothingSelected(string assetNameAlone, string validatedPrefix, string suffixValue)
        {
            if ((validatedPrefix == "none" && suffixValue == "none") || (string.IsNullOrEmpty(validatedPrefix) && string.IsNullOrEmpty(suffixValue)))
            {
                Debug.Log("Please select a prefix or suffix or both to rename asset YOU SILLY BITCH!");
            }
            return assetNameAlone;
        }

        private string RequiresPrefixAndSuffix(string assetNameAlone, string validatedPrefix, string suffixValue)
        {
            if (validatedPrefix != "none" && suffixValue != "none")
            {
                if (!assetNameAlone.StartsWith(validatedPrefix) && !assetNameAlone.EndsWith(suffixValue))
                {
                    AddPrefixAndSuffix(assetNameAlone, validatedPrefix, suffixValue);
                }
                return assetNameAlone;
            }
            return assetNameAlone;
        }

        private string RequiresPrefixOnly(string assetNameAlone, string validatedPrefix, string suffixValue)
        {
            if (!string.IsNullOrEmpty(validatedPrefix) && suffixValue == "none")
            {
                if (!assetNameAlone.StartsWith(validatedPrefix) && (assetNameAlone.EndsWith(suffixValue) || suffixValue == "none"))
                {
                    AddPrefix(validatedPrefix, assetNameAlone);
                }
                if(suffixValue == "none" && assetNameAlone.StartsWith(validatedPrefix))
                {
                    Debug.Log($"ALREADY PREFIXED DO NOTHING {assetNameAlone}");
                    return assetNameAlone;
                }
            }
            return assetNameAlone;
        }

        private string RequiresSuffixOnly(string assetNameAlone, string validatedPrefix, string suffixValue)
        {
            if (!string.IsNullOrEmpty(suffixValue) && suffixValue != "none")
            {
                if ((assetNameAlone.StartsWith(validatedPrefix) && !assetNameAlone.EndsWith(suffixValue)) || validatedPrefix == "none")
                {
                    AddSuffix(suffixValue, assetNameAlone);
                }
                return assetNameAlone;
            }
            return assetNameAlone;
        }


        private string AddPrefixAndSuffix(string assetNameAlone, string validatedPrefix, string suffixValue)
        {
            Debug.Log($"ADDED BOTH{string.Concat(validatedPrefix, assetNameAlone, suffixValue)}");
            return string.Concat(validatedPrefix, assetNameAlone, suffixValue);
        }

        private string AddPrefix(string newPrefix, string assetNameAlone)
        {
            Debug.Log($"ADDED PREFIX ONLY-{string.Concat(newPrefix, assetNameAlone)}");
            return string.Concat(newPrefix, assetNameAlone);
        }

        private string AddSuffix(string suffixValue, string assetNameAlone)
        {
            Debug.Log($"ADDED SUFFIX ONLY-{string.Concat(assetNameAlone, suffixValue)}");
            return string.Concat(assetNameAlone, suffixValue);
        }

    }
}
