using System.IO;
using UnityEngine;

namespace LoaderObject
{
    public abstract class LoaderObjectBase : ScriptableObject
    {
        private const string LOADER_FOLDER = "/Loader Objects/";
        private const string FILE_NAME_SUFFIX = ".json";
        
        private string FolderDirectory => Application.persistentDataPath + LOADER_FOLDER;
        private string FileDirectory => FolderDirectory + FileName + FILE_NAME_SUFFIX;
        
        /// <summary>
        /// The unique name of the Json File (e.g: PostProcessing, PlayerPositions)
        /// </summary>
        protected abstract string FileName { get; }
        
        #region Internal Functions
        
        internal void SaveObject()
        {
            if(!Directory.Exists(FolderDirectory))
                Directory.CreateDirectory(FolderDirectory);
            
            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(FileDirectory, json);
        }

        internal virtual void LoadObject()
        {
            if (!File.Exists(FileDirectory))
                return;

            string json = File.ReadAllText(FileDirectory);
            JsonUtility.FromJsonOverwrite(json, this);
        }

        internal void DeleteObject()
        {
            if (File.Exists(FileDirectory))
                File.Delete(FileDirectory);
        }
        
        #endregion
        
    }
}