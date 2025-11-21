using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace TheBlindEyeEditor.Utility
{
    public sealed class Helpers : Editor
    {
        public static string Clipboard
        {
            get => EditorGUIUtility.systemCopyBuffer;
            set => EditorGUIUtility.systemCopyBuffer = value;
        }
        
        /// <summary>
        /// Get the first <see cref="T"/> file in the <paramref name="folderDirectory"/>.
        /// </summary>
        public static T GetFile<T>(string searchFilter = null, string folderDirectory = "Assets") where T : Object
        {
            var directories = GetDirectories<T>(searchFilter, folderDirectory);
            return directories.Length == 0 ? default : AssetDatabase.LoadAssetAtPath<T>(directories[0]);
        }

        /// <summary>
        /// Get an array of <see cref="T"/> files in the <paramref name="folderDirectory"/>.
        /// </summary>
        public static T[] GetFiles<T>(string searchFilter = null, string folderDirectory = "Assets") where T : Object
        {
            var directories = GetDirectories<T>(searchFilter, folderDirectory);
            
            int length = directories.Length;
            var files = new T[length];

            for (int i = 0; i < length; i++)
                files[i] = AssetDatabase.LoadAssetAtPath<T>(directories[i]);

            return files;
        }
        
        /// <summary>
        /// Save the current Scene.
        /// </summary>
        public static void SaveCurrentScene() => EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        
        /// <summary>
        /// Get all directories in the <paramref name="folderDirectory"/> with type of <see cref="T"/>.
        /// </summary>
        private static string[] GetDirectories<T>(string searchFilter, string folderDirectory)
        {
            var folders = new[] { folderDirectory };
            string filter = $"t:{typeof(T).Name} {searchFilter}";
            
            var fileDirectories = AssetDatabase.FindAssets(filter, folders);

            for (int i = 0; i < fileDirectories.Length; i++)
                fileDirectories[i] = AssetDatabase.GUIDToAssetPath(fileDirectories[i]);
            
            return fileDirectories;
        }
    }
}