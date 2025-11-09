using System.Collections.Generic;
using UnityEngine;
using TheBlindEye.Utility;

#if UNITY_EDITOR
    using LoaderObject.EditorExtensions;
#endif

namespace LoaderObject
{
    /// <summary>
    /// Scriptable Object that can Load and Save Data to Json.
    /// </summary>
    /// <typeparam name="TData">Type of the Data that is passed from LoaderMono of this Scriptable Object. <br /><br /> </typeparam>
    /// <typeparam name="TValue">Type of the value that will be saved and loaded into the Json file. (e.g. class, float, struct, etc.) <br /><br /></typeparam>
    /// <remarks>"TValue" needs to be Serializable in order to save into Json. <br /><br /> 
    /// [System.Serializable] - for classes, structs
    /// [SerializeField] - for fields</remarks>
    public abstract class LoaderObjectData<TData, TValue> : LoaderObject<TData>
    {
        [SerializeField] private List<Data> dataList = new List<Data>();
        
        #region Editor Function

        #if UNITY_EDITOR
            private void OnValidate() => EditorEvent.AddListener(DeleteData);
        #endif
        
        #endregion
        
        #region Protected Functions

        /// <summary>
        /// Set the value of the Data, indicated by "dataName". If the Data is not found, a new one is created.
        /// (This can happen whether the object is not found in the Scriptable Object or the Json file was not generated)
        /// </summary>
        /// <param name="dataName">Name of the data.</param>
        /// <param name="dataValue">Value of the data.</param>
        protected void SetValue(string dataName, TValue dataValue)
        {
            var data = GetData(dataName);
            if (data != null)
            {
                data.SetValue(dataValue);
                return;
            } 
            
            AddData(dataName, dataValue);
        }
        
        /// <summary>
        /// Get the value of the Data, indicated by "dataName". If the Data is not found, a new one is created with the "defaultValue" value.
        /// (This can happen whether the object is not found in the Scriptable Object or the Json file was not generated)
        /// </summary>
        /// <param name="dataName">Name of the data.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns></returns>
        protected TValue GetValue(string dataName, TValue defaultValue = default)
        {
            var data = GetData(dataName);
            if (data != null)
                return data.GetValue();

            AddData(dataName, defaultValue);
            Debug.Log("DEFAULT");
            return defaultValue;
        }

        #endregion
        
        #region Internal Function

        internal override void LoadObject()
        {
            if (dataList.Count == 0)
                base.LoadObject();
        }

        #endregion
        
        #region Private Functions

        private void AddData(string dataName, TValue dataValue) => dataList.Add(new Data(dataName, dataValue));

        private Data GetData(string dataName)
        {
            foreach (var data in dataList)
            {
                if (data.IsSameName(dataName))
                    return data;
            }

            return null;
        }
        
        private void DeleteData()
        {
            DeleteObject();
            dataList.Clear();
        }
        
        #endregion

        #region Data Class
        
        [System.Serializable]
        private class Data
        {
            [SerializeField, ReadOnly] private string name;
            [SerializeField, ReadOnly] private TValue value;

            internal Data(string newName, TValue newValue)
            {
                name = newName;
                value = newValue;
            }
            
            internal void SetValue(TValue newValue) => value = newValue;
            internal TValue GetValue() => value;
            
            internal bool IsSameName(string passedName) => name.Equals(passedName);
        }
        
        #endregion
        
    }
}