using UnityEngine;

namespace IRM.AlbumSystem
{
    internal sealed class PageController : MonoBehaviour
    {
        [SerializeField] private Transform frontSpawnPivot;
        [SerializeField] private Transform backSpawnPivot;

        public void ShowContent(AlbumPage page, bool showPageFront)
        {
            var spawnPivot = showPageFront ? frontSpawnPivot : backSpawnPivot;
            if (spawnPivot.childCount != 0)
                return;
            
            var pageContent = Instantiate(page.PageContentPrefab, spawnPivot);
            pageContent.Initialize(page);
        }

        public void CleanUp()
        {
            if (frontSpawnPivot.childCount != 0)
                Destroy(frontSpawnPivot.GetChild(0).gameObject);
            
            if (backSpawnPivot.childCount != 0)
                Destroy(backSpawnPivot.GetChild(0).gameObject);
        }
    }
}