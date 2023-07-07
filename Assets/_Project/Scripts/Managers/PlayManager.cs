using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// an example script to visualize some element data in the play manager inspector drawer to edit the ui in ui builder
namespace SoggyInkGames.Equanimous.Lab.Managers
{
    [RequireComponent(typeof(UIDocument))]
    public class PlayManager : MonoBehaviour
    {
        [Header("This is a Header")]
        public float m_ElementRandomRange = 20;
        [Range(10, 40)] public float m_ElementForce = 25;
        public int m_ElementCount = 10;
        public float m_ElementDelay = 0.1f;
        public GameObject m_ElementPrefab;

        public ElementManager[] m_Elements;

        // public List<string> m_TexturePrefix = new() { "T_", "M_", "SDF_", "VFX_", "SH_", "SHG_", "PS_", "none" };
        [HideInInspector] public Object m_SelectedAsset;

        //todo: turn object into object array 
        //todo: once conditions pass, replace dubugs with logic to rename assets using paths
        //TODO: CHECK PREFIx MATCHES ASSET TYPE
        //todo: add ability to rename multiple at same time/use list of objects instead
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


        public void RenameAsset(string suffixValue, string newPrefix)
        {
            var currentAssetName = m_SelectedAsset.name;
            // var assetType = m_SelectedAsset.GetType();

            PathForSelected(currentAssetName);


            NothingSelected(currentAssetName, newPrefix, suffixValue);
            RequiresPrefixAndSuffix(currentAssetName, newPrefix, suffixValue);
            RequiresPrefixOnly(currentAssetName, newPrefix, suffixValue);
            RequiresSuffixOnly(currentAssetName, newPrefix, suffixValue);

        }

        private string NothingSelected(string currentAssetName, string newPrefix, string suffixValue)
        {
            if((newPrefix == "none" && suffixValue == "none") || (string.IsNullOrEmpty(newPrefix) && string.IsNullOrEmpty(suffixValue)))
            {
                Debug.Log("Please select a prefix or suffix or both to rename asset YOU SILLY BITCH!");
            }
            return currentAssetName;
        }
        
        private string RequiresPrefixAndSuffix(string currentAssetName, string newPrefix, string suffixValue)
        {
            if (newPrefix != "none" && suffixValue != "none")
            {
                if (!currentAssetName.StartsWith(newPrefix) && !currentAssetName.EndsWith(suffixValue))
                {
                    AddPrefixAndSuffix(currentAssetName, newPrefix, suffixValue);
                }
                return currentAssetName;
            }
            return currentAssetName;
        }

        private string RequiresPrefixOnly(string currentAssetName, string newPrefix, string suffixValue)
        {
            if (!string.IsNullOrEmpty(newPrefix) && newPrefix != "none")
            {
                if ((!currentAssetName.StartsWith(newPrefix) && currentAssetName.EndsWith(suffixValue)) || suffixValue == "none")//add second check
                {
                    AddPrefix(newPrefix, currentAssetName);
                }
                return currentAssetName;
            }
            return currentAssetName;
        }

        private string RequiresSuffixOnly(string currentAssetName, string newPrefix, string suffixValue)
        {
            if (!string.IsNullOrEmpty(suffixValue) && suffixValue != "none")
            {
                if ((currentAssetName.StartsWith(newPrefix) && !currentAssetName.EndsWith(suffixValue)) || newPrefix == "none")
                {
                    AddSuffix(suffixValue, currentAssetName);
                }
                return currentAssetName;
            }
            return currentAssetName;
        }


        private string AddPrefixAndSuffix(string currentAssetName, string newPrefix, string suffixValue)
        {
            Debug.Log($"ADDED BOTH{string.Concat(newPrefix, currentAssetName, suffixValue)}");
            return string.Concat(newPrefix, currentAssetName, suffixValue);
        }

        private string AddPrefix(string newPrefix, string currentAssetName)
        {
            Debug.Log($"ADDED PREFIX ONLY-{string.Concat(newPrefix, currentAssetName)}");
            return string.Concat(newPrefix, currentAssetName);
        }

        private string AddSuffix(string suffixValue, string currentAssetName)
        {
            Debug.Log($"ADDED SUFFIX ONLY-{string.Concat(currentAssetName, suffixValue)}");
            return string.Concat(currentAssetName, suffixValue);
        }

    }
}
