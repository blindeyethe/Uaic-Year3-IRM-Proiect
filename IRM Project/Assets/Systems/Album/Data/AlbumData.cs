#if UNITY_EDITOR
    using UnityEditor;
#endif
    
using UnityEngine;

namespace IRM.AlbumSystem
{
    [CreateAssetMenu(fileName = "Album Data", menuName = "Album System/Album Data")]
    internal sealed class AlbumData : ScriptableObject
    {
        [SerializeField] private AlbumPage[] pages;
        
        public AlbumPage[] Pages => pages;
        public int OpenCount => pages.Length / 2;

        public (AlbumPage left, AlbumPage right) GetPages(int openIndex)
        {
            var leftPageIndex = openIndex * 2;
            return (pages[leftPageIndex], pages[leftPageIndex + 1]);
        }
        
        #if UNITY_EDITOR
            private void OnValidate() =>
                Editor_CalculatePagesPhotoObjects();
        
            [ContextMenu("Calculate Pages Photo Objects")]
            private void Editor_CalculatePagesPhotoObjects()
            {
                foreach (var page in pages)
                    page.Editor_CalculatePagePhotoObjects();

                EditorUtility.SetDirty(this);
            }
        #endif
    }
}