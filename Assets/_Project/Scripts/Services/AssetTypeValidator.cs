using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoggyInkGames.Equanimous.Lab.Services
{
        public class AssetTypeValidator
        {
            private readonly Dictionary<string, string> _validPrefixes = new()
            {
                {"UnityEngine.Texture2D", "T_"},
                {"UnityEngine.Material", "M_"},
                {"UnityEngine.Texture3D", "SDF_"},
                {"UnityEngine.VFX.VisualEffectAsset", "VFX_"},
                {"UnityEngine.Shader", "SH_"},
                {"UnityEngine.ParticleSystem", "PS_"},
                {"UnityEngine.TerrainData", "TER_"},
                {"UnityEngine.Mesh", "MESH_"},
            };

            private readonly Dictionary<string, string> _validPrefixesFromExtension = new()
            {
                {".shadergraph", "SHG_"},
            };
            public string GetValidPrefix(object assetType, string extension)
            {
                if (extension == ".shadergraph")
                {
                    var assetExtensionString = extension;
                    return _validPrefixesFromExtension.GetValueOrDefault(assetExtensionString, "");
                }
                else if (extension != ".shadergraph")
                {
                    var assetTypeStr = assetType.GetType().ToString();
                    return _validPrefixes.GetValueOrDefault(assetTypeStr, "");
                }
                else return null;
            }
        }
}
