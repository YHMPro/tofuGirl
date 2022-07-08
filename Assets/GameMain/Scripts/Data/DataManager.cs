
using System.Collections.Generic;
using System;
using UnityGameFramework.Runtime;
namespace Project.TofuGirl.Data
{
    /// <summary>
    /// 数据管理接口
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// 数据的数量
        /// </summary>
        int DataCount { get; }
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool HasData<T>() where T : Data;
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetData<T>() where T : Data;
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        Data[] GetAllData();
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void AddData<T>() where T : Data;
        /// <summary>
        /// 移除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void RemoveData<T>() where T : Data;
        /// <summary>
        /// 初始化所有数据
        /// </summary>
        void InitAllData();
        /// <summary>
        /// 预加载所有数据
        /// </summary>
        void PreLoadAllData();
        /// <summary>
        /// 加载所有数据
        /// </summary>
        void LoadAllData();
        /// <summary>
        /// 卸载所有数据
        /// </summary>
        void UnLoadAllData();

    }
    /// <summary>
    /// 数据管理
    /// </summary>
    public sealed class DataManager : IDataManager
    {
        public int DataCount => m_DataDic.Count;
        /// <summary>
        /// 数据容器
        /// </summary>
        private Dictionary<Type, Data> m_DataDic;

        public DataManager()
        {
            m_DataDic = new Dictionary<Type, Data>();
        }

        public bool HasData<T>() where T : Data
        {
            return m_DataDic.ContainsKey(typeof(T));
        }

        public void AddData<T>() where T : Data
        {
            Type type = typeof(T);
          
            if(!m_DataDic.ContainsKey(type))
            {
                m_DataDic.Add(type, Activator.CreateInstance<T>());
            }
        }

        public Data[] GetAllData()
        {
            Data[] datas= new Data[m_DataDic.Count];
            int index = 0;
            foreach(Data data in m_DataDic.Values)
            {
                datas[index] = data;
                index++;
            }
            return datas;        
        }

        public T GetData<T>() where T : Data
        {
            Type type =typeof(T);
            if(m_DataDic.ContainsKey(type))
            {
                return m_DataDic[type] as T;
            }
            return null;
        }

        public void InitAllData()
        {
            foreach(Data data in m_DataDic.Values)
            {
                data.Init();
            }
        }

        public void PreLoadAllData()
        {
            foreach (Data data in m_DataDic.Values)
            {
                data.Preload();
            }
        }

        public void LoadAllData()
        {
            foreach (Data data in m_DataDic.Values)
            {
                data.Load();
            }
        }

        public void RemoveData<T>() where T : Data
        {
            Type type =typeof(T);
            if (m_DataDic.ContainsKey(type))
            {
                m_DataDic[type].Unload();
                m_DataDic.Remove(type);
            }
        }

        public void UnLoadAllData()
        {
            foreach (Data data in m_DataDic.Values)
            {
                data.Unload();
            }
            m_DataDic.Clear();
        }
    }
}
