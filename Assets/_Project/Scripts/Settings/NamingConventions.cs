using System.IO;
using UnityEditor;
using UnityEngine;

namespace SoggyInkGames.Equanimous.Lab.Settings
{
    public class NamingConventions : MonoBehaviour
    {
        public string prefix;
        public string suffix;
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
                    string currentFullName = Path.GetFileName(file);
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string fileExtension = Path.GetExtension(file);
                    string fullPath = $"{path}/anyFile";
                    string assetType = Path.GetFileName(Path.GetDirectoryName(fullPath));


                    if (!string.IsNullOrEmpty(Path.GetExtension(currentFullName)) && !currentFullName.EndsWith(".meta"))
                    {
                        if (!fileName.StartsWith(prefix) && !fileName.EndsWith(suffix))
                        {
                            // Create the new file name
                            string newFileName = ApplyNamingConventions(fileName, fileExtension, assetType);
                            if (newFileName != fileName)
                            {//todo: should only add suffix if available
                                string newFilePath = Path.Combine(Path.GetDirectoryName(file), $"{prefix}{fileName}{suffix}{fileExtension}");

                                // Move the file to the new location
                                File.Move(file, newFilePath);
                                AssetDatabase.Refresh();
                            }
                        }

                        Debug.Log("Asset renaming complete.");

                    }

                }

            }
        }

        private string ApplyNamingConventions(string fileName, string fileExtension, string assetType)
        {
            // Apply the naming conventions based on the asset type
            switch (assetType)
            {
                case "Textures":
                    return ApplyTextureNamingConventions(fileName);
                case "Materials":
                    return ApplyMaterialNamingConventions(fileName);
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
    }
}
// todo: add all prefixes for all files in assets that should have a prefix as stated in the readme
// todo: run as separate class, add to editor guid, menubutton instead of on start
