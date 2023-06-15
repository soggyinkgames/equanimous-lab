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
        public string assetType;

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


                    if (!string.IsNullOrEmpty(Path.GetExtension(currentFullName)) && !currentFullName.EndsWith(".meta"))
                    {
                        if (!fileName.StartsWith(prefix) && !fileName.EndsWith(suffix))
                        {
                            // Create the new file name
                            string newFileName = ApplyNamingConventions(fileName, fileExtension);
                            if(newFileName != fileName)
                            {//todo: check this
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

        private string ApplyNamingConventions(string fileName, string fileExtension)
        {
            // Apply the naming conventions based on the asset type
            //todo: you should get the asset type from the path not the exposed var
            switch (assetType)
            {
                case "Texture":
                    return ApplyTextureNamingConventions(fileName);
                case "Material":
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
// todo: invoke function when you open the editor if anything in mentioned folders has been updated
// not onstart()
