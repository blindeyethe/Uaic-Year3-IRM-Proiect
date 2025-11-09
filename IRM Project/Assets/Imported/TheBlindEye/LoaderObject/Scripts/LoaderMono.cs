using System.Collections.Generic;
using UnityEngine;
using TheBlindEye.Utility.LoaderObject;

namespace LoaderObject
{
    /// <summary>
    /// MonoBehaviour that can Load and Save Data to Json.
    /// </summary>
    /// <typeparam name="TObject">Type of the Scriptable Object that inherits from LoaderObjectData. <br /><br /> </typeparam>
    /// <typeparam name="TData">Type of the Data that will be passed to the Scriptable Object, indicated by "TObject". (e.g. class, float, struct, etc.)</typeparam>
    public abstract class LoaderMono<TObject, TData> : MonoBehaviour where TObject : LoaderObject<TData>
    {
        [SerializeField] private TObject loaderData;

        private TData[] _dataList;

        #region Protected Functions
        
        /// <summary>
        /// Use PassData function to pass the Data to the Scriptable Object. 
        /// </summary>
        protected abstract void Awake();

        /// <summary>
        /// Can be overridden but call base.Start(); at the bottom of the function.
        /// </summary>
        protected virtual void Start()
        {
            if (_dataList == null)
                new Error01(gameObject.name).Trow();
            
            Load();
        }
        
        /// <summary>
        /// Can be overridden but call base.OnDisable(); at the bottom of the function.
        /// </summary>
        protected virtual void OnDisable() => Save();

        /// <summary>
        /// Pass a single Data that will Load and Save to Json.
        /// </summary>
        /// <param name="dataElement">Type indicated by "TData".</param>
        protected void PassData(TData dataElement) => _dataList = new [] {dataElement};
        
        /// <summary>
        /// Pass an array of Data that will Load and Save to Json.
        /// </summary>
        /// <param name="dataArray">Type indicated by "TData".</param>
        protected void PassData(TData[] dataArray) => _dataList = dataArray;
        
        /// <summary>
        /// Pass a list of Data that will Load and Save to Json.
        /// </summary>
        /// <param name="dataList">Type indicated by "TData".</param>
        protected void PassData(List<TData> dataList) => _dataList = dataList.ToArray();
        
        #endregion

        #region Private Functions
        
        private void Save()
        {
            if (_dataList == null)
                return;
            
            foreach (var data in _dataList)
            {
                if (data != null)
                    loaderData.SaveData(data);
            }

            loaderData.SaveObject();
        }
        
        private void Load()
        {
            if (_dataList == null) 
                return;
            
            loaderData.LoadObject();

            foreach (var data in _dataList)
            {
                if (data != null)
                    loaderData.LoadData(data);
            }
        }
        
        #endregion
        
    }
}