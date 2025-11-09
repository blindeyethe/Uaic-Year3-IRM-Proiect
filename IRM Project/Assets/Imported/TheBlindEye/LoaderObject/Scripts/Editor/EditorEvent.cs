using System;

namespace LoaderObject.EditorExtensions
{
    public static class EditorEvent
    {
        private static event Action Action;

        public static void AddListener(Action listener) => Action += listener;
        public static void Invoke() => Action?.Invoke();
    }
}
