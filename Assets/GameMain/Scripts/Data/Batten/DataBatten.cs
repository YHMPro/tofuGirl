using GameFramework.DataTable;
using UnityGameFramework.Runtime;
using System.Collections.Generic;
namespace Project.TofuGirl.Data
{
    public class DataBatten : DataBase
    {
        private IDataTable<DTBatten> m_DT;
        private Dictionary<int, BattenData> m_BDDic;

        protected override void OnPreload()
        {
            base.OnPreload();
            LoadDataTable("Batten");
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            m_DT = GameEntry.DataTable.GetDataTable<DTBatten>();
            m_BDDic = new Dictionary<int, BattenData>();
            foreach (DTBatten dTBatten in m_DT)
            {
                if (!m_BDDic.ContainsKey(dTBatten.Id))
                {
                    m_BDDic.Add(dTBatten.Id, new BattenData(dTBatten));                  
                }
            }
        }
            
        public BattenData GetBattenData(int battenId)
        {
            if (m_BDDic.ContainsKey(battenId))
            {
                return m_BDDic[battenId];
            }
            return null;
        }
    }
}
