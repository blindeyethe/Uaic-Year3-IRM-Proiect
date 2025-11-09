using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace LoaderObject.EditorExtensions
{
    internal class BuildProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder { get; }
        public void OnPreprocessBuild(BuildReport report) => EditorEvent.Invoke();
    }
}