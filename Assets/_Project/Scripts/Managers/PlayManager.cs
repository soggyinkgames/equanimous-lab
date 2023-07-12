using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

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
        [HideInInspector] public Object m_SelectedAsset;
        // [HideInInspector] public Object[] m_SelectedAssets;

        // TODO: use validation to style uss error ui
        // TODO: move validation logic to its own services folder/ class
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

        static void PathForSelected(string selected)
        {

            string[] selectedAssetGuids = AssetDatabase.FindAssets(selected);
            if (selectedAssetGuids.Length == 0) return;

            string selectedAssetPath = AssetDatabase.GUIDToAssetPath(selectedAssetGuids[0]);

            Debug.Log($"PathForSelected----->>{selectedAssetPath}");

        }

        private static string ValidateSelectedAssetType(object m_SelectedAsset, string newPrefix)
        {
            // ValidateSelectedAssetType(m_SelectedAsset, newPrefix);
            var assetTypeValidator = new AssetTypeValidator();

            var validatedPrefix = assetTypeValidator.GetValidPrefix(m_SelectedAsset);
            if (newPrefix != validatedPrefix)
            {
                Debug.LogError("PREFIX SELECTED DOES NOT MATCH ASSET TYPE!! TRY AGAIN");
                return null;
            }
            return validatedPrefix;
        }
        public class AssetTypeValidator
        {
            private readonly Dictionary<string, string> _validPrefixes = new()
            {
                {"UnityEngine.Material", "M_"},
                {"UnityEngine.Texture2D", "T_"},
                {"UnityEngine.ParticleSystem", "PS_"},
                {"UnityEngine.Texture3D", "SDF_"},
                {"UnityEngine.VFX.VissualEffect", "VFX_"},
                {"UnityEngine.Mesh", "MESH_"},
                {"UnityEngine.TerrainData", "TER_"},
            };
            public string GetValidPrefix(object assetType)
            {
                var assetTypeStr = assetType.GetType().ToString();
                Debug.Log($"NEW VALIDATION CODE--{_validPrefixes.GetValueOrDefault(assetTypeStr, "")}");
                return _validPrefixes.GetValueOrDefault(assetTypeStr, "");
            }
        }


        public void RenameAsset(string suffixValue, string newPrefix)
        {
            var currentAssetName = m_SelectedAsset.name;


            PathForSelected(currentAssetName);
            string validatedPrefix = ValidateSelectedAssetType(m_SelectedAsset, newPrefix);
            Debug.Log($"NEW validatedPrefix CODE INSIDE RENAME--{validatedPrefix}");

            NothingSelected(currentAssetName, validatedPrefix, suffixValue);
            RequiresPrefixAndSuffix(currentAssetName, validatedPrefix, suffixValue);
            RequiresPrefixOnly(currentAssetName, validatedPrefix, suffixValue);
            RequiresSuffixOnly(currentAssetName, validatedPrefix, suffixValue);
        }

        private string NothingSelected(string currentAssetName, string validatedPrefix, string suffixValue)
        {
            if ((validatedPrefix == "none" && suffixValue == "none") || (string.IsNullOrEmpty(validatedPrefix) && string.IsNullOrEmpty(suffixValue)))
            {
                Debug.Log("Please select a prefix or suffix or both to rename asset YOU SILLY BITCH!");
            }
            return currentAssetName;
        }

        private string RequiresPrefixAndSuffix(string currentAssetName, string validatedPrefix, string suffixValue)
        {
            if (validatedPrefix != "none" && suffixValue != "none")
            {
                if (!currentAssetName.StartsWith(validatedPrefix) && !currentAssetName.EndsWith(suffixValue))
                {
                    AddPrefixAndSuffix(currentAssetName, validatedPrefix, suffixValue);
                }
                return currentAssetName;
            }
            return currentAssetName;
        }

        private string RequiresPrefixOnly(string currentAssetName, string validatedPrefix, string suffixValue)
        {
            if (!string.IsNullOrEmpty(validatedPrefix) && validatedPrefix != "none")
            {
                if ((!currentAssetName.StartsWith(validatedPrefix) && currentAssetName.EndsWith(suffixValue)) || suffixValue == "none")//add second check
                {
                    AddPrefix(validatedPrefix, currentAssetName);
                }
                return currentAssetName;
            }
            return currentAssetName;
        }

        private string RequiresSuffixOnly(string currentAssetName, string validatedPrefix, string suffixValue)
        {
            if (!string.IsNullOrEmpty(suffixValue) && suffixValue != "none")
            {
                if ((currentAssetName.StartsWith(validatedPrefix) && !currentAssetName.EndsWith(suffixValue)) || validatedPrefix == "none")
                {
                    AddSuffix(suffixValue, currentAssetName);
                }
                return currentAssetName;
            }
            return currentAssetName;
        }


        private string AddPrefixAndSuffix(string currentAssetName, string validatedPrefix, string suffixValue)
        {
            Debug.Log($"ADDED BOTH{string.Concat(validatedPrefix, currentAssetName, suffixValue)}");
            return string.Concat(validatedPrefix, currentAssetName, suffixValue);
        }

        private string AddPrefix(string validatedPrefix, string currentAssetName)
        {
            Debug.Log($"ADDED PREFIX ONLY-{string.Concat(validatedPrefix, currentAssetName)}");
            return string.Concat(validatedPrefix, currentAssetName);
        }

        private string AddSuffix(string suffixValue, string currentAssetName)
        {
            Debug.Log($"ADDED SUFFIX ONLY-{string.Concat(currentAssetName, suffixValue)}");
            return string.Concat(currentAssetName, suffixValue);
        }

    }
}
