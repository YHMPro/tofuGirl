
using GameFramework.DataTable;
using System.Collections.Generic;
using UnityGameFramework.Runtime;

namespace Project.TofuGirl.Data
{

    public class DataTofu :DataBase
    {
        private IDataTable<DTTofu> m_DT;
        private Dictionary<int, TofuData> m_TDData;
        protected override void OnPreload()
        {
            LoadDataTable("Tofu");
        }

        protected override void OnLoad()
        {
            m_DT = GameEntry.DataTable.GetDataTable<DTTofu>();
            m_TDData = new Dictionary<int, TofuData>();
            foreach (DTTofu dTTofu in m_DT)
            {
                if (!m_TDData.ContainsKey(dTTofu.Id))
                {
                    m_TDData.Add(dTTofu.Id, new TofuData(dTTofu));
                }
            }
        }

        public TofuData GetTofuData(int tofuId)
        {
            if (m_TDData.ContainsKey(tofuId))
            {
                return m_TDData[tofuId];
            }
            return null;
        }

        protected override void OnUnLoad()
        {
            base.OnUnLoad();
            m_DT.RemoveAllDataRows();
            m_DT = null;
            m_TDData = null;
        }
    }
}
