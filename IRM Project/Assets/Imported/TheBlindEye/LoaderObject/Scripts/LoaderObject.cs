namespace LoaderObject
{
    public abstract class LoaderObject<TData> : LoaderObjectBase
    {
        /// <summary>
        /// Called when the LoaderMono of this Scriptable Object is Disabled. Use SetValue function to save the Data.
        /// </summary>
        /// <param name="data">Type indicated by "TData".</param>
        public abstract void SaveData(TData data);
        
        /// <summary>
        /// Called when the LoaderMono of this Scriptable Object is Awakened. Use GetValue function to get the Data.
        /// </summary>
        /// <param name="data">Type indicated by "TData".</param>
        public abstract void LoadData(TData data);
    }
}