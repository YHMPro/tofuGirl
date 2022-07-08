using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project.TofuGirl.Data
{
    /*
     * 后续封装成模块接入GF框架中
     */
    [AddComponentMenu("Game Framework/Data")]
    public sealed class DataComponent : MonoBehaviour
    {
        private IDataManager m_DataManager;

        public int DataCount => m_DataManager.DataCount;

        private void Awake()
        {
            m_DataManager=new DataManager();
        }

        private void Start()
        {
            m_DataManager.AddData<DataGirl>();
            m_DataManager.AddData<DataBatten>();
            m_DataManager.AddData<DataPoolParam>();
            m_DataManager.AddData<DataAssetsPath>();
            m_DataManager.AddData<DataEntityGroup>();
            m_DataManager.AddData<DataEntity>();
            m_DataManager.AddData<DataLevel>();
            m_DataManager.InitAllData();
        }
        private void OnDestroy()
        {        
            //数据清除需要看实际项目运行情况，暂未接入GF框架流程中进行管理，所有通过暴露的接口在外部进行调用
        }
        public void AddData<T>()where T:Data
        {
            m_DataManager.AddData<T>();
        }

        public T GetData<T>() where T : Data
        {
            return m_DataManager.GetData<T>();
        }

        public void RemoveData<T>()where T:Data
        {
            m_DataManager.RemoveData<T>();
        }

        public bool HasData<T>() where T:Data
        {
            return m_DataManager.HasData<T>();
        }

        public Data[] GetAllData()
        {
            return m_DataManager.GetAllData();
        }

        public void InitAllData()
        {
            m_DataManager.InitAllData();
        }

        public void PreLoadAllData()
        {
            m_DataManager.PreLoadAllData();      
        }

        public void LoadAllData()
        {
            m_DataManager.LoadAllData();
        }

        public void UnLoadAllData()
        {
            m_DataManager.UnLoadAllData();
        }
    }
}
