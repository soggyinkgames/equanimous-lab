using System.IO;
using UnityEditor;
using UnityEngine;

namespace SoggyInkGames.Equanimous.Lab.Settings
{
    public class NamingConventions : MonoBehaviour
    {
        public string prefix;
        public string suffixType;
        public string path;

        public void Start()
        {

            if (!string.IsNullOrEmpty(path))
            {
                // Get all assets in the specified folder
                string[] files = Directory.GetFiles(path);
                // Rename all assets at once
                foreach (string file in files)
                {
                    string currentFullName = Path.GetFileName(file); //eg. "../../../../"
                    string fileName = Path.GetFileNameWithoutExtension(file); //eg. Fabric_Neon or T_Fabric_Neon
                    string fileExtension = Path.GetExtension(file); //eg. .jpg
                    string fullPath = $"{path}/anyFile"; //eg. "../../Textures/FabricNeon.jpg"
                    string assetType = Path.GetFileName(Path.GetDirectoryName(fullPath)); //todo: currently selecting via folder, should select via extension types

                    //todo: runing the script should add both prefix and sufix, sufix if available, prefix if available 
                    if (!string.IsNullOrEmpty(Path.GetExtension(currentFullName)) && !currentFullName.EndsWith(".meta"))
                    {
                        string newFilePath = file;
                        bool renamed = false;

                        if (!fileName.EndsWith(suffixType))
                        {
                            // Create the new file name with texture sufix
                            if (fileName.StartsWith("T_"))
                            {
                                string newFileName = ApplyTextureSufixNamingConventions(fileName, suffixType);
                                if (newFileName != fileName)
                                {
                                    // Update the new file path with texture suffixType
                                    newFilePath = Path.Combine(Path.GetDirectoryName(file), $"{fileName}{suffixType}{fileExtension}");
                                    renamed = true;
                                }
                            }
                            else
                            {   
                                // Create the new file name with sufix
                                string newFileName = ApplySufixNamingConventions(fileName, suffixType);
                                if (newFileName != fileName)
                                {
                                    // Update the new file path with suffixType
                                    newFilePath = Path.Combine(Path.GetDirectoryName(file), $"{fileName}{suffixType}{fileExtension}");
                                    renamed = true;
                                }
                            }
                        }
                        // Create the new file name with prefix
                        if (!fileName.StartsWith(prefix))
                        {
                            string newFileName = ApplyPrefixNamingConventions(fileName, assetType);
                            if (newFileName != fileName)
                            {
                                // Update the new file path with prefix
                                newFilePath = Path.Combine(Path.GetDirectoryName(file), $"{prefix}{fileName}{fileExtension}");
                                renamed = true;
                            }
                        }
        

                        if (renamed)
                        {
                            File.Move(file, newFilePath);
                            AssetDatabase.Refresh();
                            Debug.Log("Asset renaming complete.");
                        }

                    }

                }

            }
        }

        private string ApplyPrefixNamingConventions(string fileName, string assetType)
        {
            // Apply the naming conventions based on the asset type
            switch (assetType)
            {
                case "Textures":
                    return ApplyTextureNamingConventions(fileName);
                case "Materials":
                    return ApplyMaterialNamingConventions(fileName);
                case "SDF":
                    return ApplySignedDistanceFieldNamingConventions(fileName);
                case "SH":
                    return ApplyShaderNamingConventions(fileName);
                case "SHG":
                    return ApplyShaderGraphNamingConventions(fileName);
                case "PS":
                    return ApplyParticleSystemNamingConventions(fileName);
                case "VFX":
                    return ApplyVisualEffectsNamingConventions(fileName);
                // Add more cases for other asset types as needed
                default:
                    return fileName; // No naming conventions applied
            }
        }


        private string ApplyTextureNamingConventions(string fileName)
        {
            if (!fileName.StartsWith("T_"))
            {
                fileName = "T_" + fileName;
            }
            return fileName;
        }

        private string ApplyMaterialNamingConventions(string fileName)
        {
            if (!fileName.StartsWith("M_"))
            {
                fileName = "M_" + fileName;
            }
            return fileName;
        }

        private string ApplySignedDistanceFieldNamingConventions(string fileName)
        {
            if (!fileName.StartsWith("SDF_"))
            {
                fileName = "SDF_" + fileName;
            }
            return fileName;
        }

        private string ApplyShaderNamingConventions(string fileName)
        {
            if (!fileName.StartsWith("SH_"))
            {
                fileName = "SH_" + fileName;
            }
            return fileName;
        }

        private string ApplyShaderGraphNamingConventions(string fileName)
        {
            if (!fileName.StartsWith("SHG_"))
            {
                fileName = "SHG_" + fileName;
            }
            return fileName;
        }

        private string ApplyParticleSystemNamingConventions(string fileName)
        {
            if (!fileName.StartsWith("PS_"))
            {
                fileName = "PS_" + fileName;
            }
            return fileName;
        }

        private string ApplyVisualEffectsNamingConventions(string fileName)
        {
            if (!fileName.StartsWith("VFX_"))
            {
                fileName = "VFX_" + fileName;
            }
            return fileName;
        }
        private string ApplyTextureSufixNamingConventions(string fileName, string suffixType)
        {
            // Apply the naming conventions based on the asset type
            //todo: add sufixType
            switch (suffixType)
            {
                case "_D":
                    return ApplyTextureDiffuseSufix(fileName);
                case "_Normal":
                    return ApplyTextureNormalSufix(fileName);
                case "_Roughness":
                    return ApplyTextureRoughnessSufix(fileName);
                case "_AlphaOpacity":
                    return ApplyTextureAlphaOpacitySufix(fileName);
                case "_AmbientOcclusion":
                    return ApplyTextureAmbientOcclusionSufix(fileName);
                case "_Bump":
                    return ApplyTextureBumpSufix(fileName);
                case "_Emissive":
                    return ApplyTextureEmissiveSufix(fileName);
                case "_Mask":
                    return ApplyTextureMaskSufix(fileName);
                case "_Specular":
                    return ApplyTextureSpecularSufix(fileName);
                case "_Particle":
                    return ApplyTextureParticleSufix(fileName);
                // Add more cases for other asset types as needed
                default:
                    return fileName; // No naming conventions applied
            }
        }

        private string ApplyTextureDiffuseSufix(string fileName)
        {
            if (!fileName.EndsWith("_D"))
            {
                fileName = fileName + "_D";
            }
            return fileName;
        }

        private string ApplyTextureNormalSufix(string fileName)
        {
            if (!fileName.EndsWith("_Normal"))
            {
                fileName = fileName + "_Normal";
            }
            return fileName;
        }

        private string ApplyTextureRoughnessSufix(string fileName)
        {
            if (!fileName.EndsWith("_Roughness"))
            {
                fileName = fileName + "_Roughness";
            }
            return fileName;
        }

        private string ApplyTextureAlphaOpacitySufix(string fileName)
        {
            if (!fileName.EndsWith("_Alpha"))
            {
                fileName = fileName + "_Alpha";
            }
            return fileName;
        }

        private string ApplyTextureAmbientOcclusionSufix(string fileName)
        {
            if (!fileName.EndsWith("_AO"))
            {
                fileName = fileName + "_AO";
            }
            return fileName;
        }

        private string ApplyTextureBumpSufix(string fileName)
        {
            if (!fileName.EndsWith("_Bump"))
            {
                fileName = fileName + "_Bump";
            }
            return fileName;
        }

        private string ApplyTextureEmissiveSufix(string fileName)
        {
            if (!fileName.EndsWith("_Emissive"))
            {
                fileName = fileName + "_Emissive";
            }
            return fileName;
        }

        private string ApplyTextureMaskSufix(string fileName)
        {
            if (!fileName.EndsWith("_Mask"))
            {
                fileName = fileName + "_Mask";
            }
            return fileName;
        }

        private string ApplyTextureSpecularSufix(string fileName)
        {
            if (!fileName.EndsWith("_Specular"))
            {
                fileName = fileName + "_Specular";
            }
            return fileName;
        }

        private string ApplyTextureParticleSufix(string fileName)
        {
            if (!fileName.EndsWith("_P"))
            {
                fileName = fileName + "_P";
            }
            return fileName;
        }

        private string ApplySufixNamingConventions(string fileName, string sufixType)
        {
            // Apply the naming conventions based on the asset type
            //todo: add sufixType
            switch (sufixType)
            {
                case "Elements":
                    return ApplyElementsSufix(fileName);
                case "Icon":
                    return ApplyIconsSufix(fileName);
                case "Alphabet":
                    return ApplyAlphabetSufix(fileName);
                case "Furniture":
                    return ApplyFurnitureSufix(fileName);
                // Add more cases for other asset types as needed
                default:
                    return fileName; // No naming conventions applied
            }
        }

        private string ApplyElementsSufix(string fileName)
        {
            if (!fileName.EndsWith("_E"))
            {
                fileName = fileName + "_E";
            }
            return fileName;
        }

        private string ApplyIconsSufix(string fileName)
        {
            if (!fileName.EndsWith("_I"))
            {
                fileName = fileName + "_I";
            }
            return fileName;
        }

        private string ApplyAlphabetSufix(string fileName)
        {
            if (!fileName.EndsWith("_A"))
            {
                fileName = fileName + "_A";
            }
            return fileName;
        }

        private string ApplyFurnitureSufix(string fileName)
        {
            if (!fileName.EndsWith("_F"))
            {
                fileName = fileName + "_F";
            }
            return fileName;
        }
    }
}
// todo: run as separate class, add to editor guid, menubutton instead of on start
