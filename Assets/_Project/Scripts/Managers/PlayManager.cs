using System.Collections.Generic;
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

        public Object m_SelectedAsset;

        //TODO: RENAME DROPDOWN CHOICES
        //TODO: ADD TOOLTIPS WITH FULL VALUE NAMES
        //TODO: CHECK PREFIC MATCHES ASSET TYPE
        //todo: add ability to rename multiple at same time


        public void RenameAsset(string suffixValue, string newPrefix)
        {
            var currentAssetName = m_SelectedAsset.name;
            var assetType = m_SelectedAsset.GetType();

            Debug.Log($"INSIDE SELECTIONS ASsET newPrefix: {newPrefix} name:{currentAssetName} suffixValue: {suffixValue} type: {assetType}");

            if (newPrefix == null || newPrefix == "none")
            {
                if (!string.IsNullOrEmpty(suffixValue) && suffixValue != "none")
                {
                    if (!currentAssetName.EndsWith(suffixValue))
                    {
                        string newName = $"{currentAssetName}{suffixValue}";
                        Debug.Log($"NEW NAME SUFFIX ONLY{newName}");
                    }
                }
            }

            if (suffixValue == null || suffixValue == "none")
            {
                if (!string.IsNullOrEmpty(newPrefix) && newPrefix != "none")
                {
                    if (!currentAssetName.StartsWith(newPrefix))
                    {
                        string newName = $"{newPrefix}{currentAssetName}";
                        Debug.Log($"NEW NAME PREFIX ONLY {newName}");
                    }
                }
            }

        }
    }

}
