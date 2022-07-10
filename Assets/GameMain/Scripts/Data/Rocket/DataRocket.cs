
using GameFramework.DataTable;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
namespace Project.TofuGirl.Data
{
    public class DataRocket : DataBase
    {
        private IDataTable<DTRocket> m_DT;
        private Dictionary<int, RocketData> m_RDDic;
        protected override void OnPreload()
        {
            LoadDataTable("Rocket");
        }

        protected override void OnLoad()
        {       
            m_DT = GameEntry.DataTable.GetDataTable<DTRocket>();
            m_RDDic = new Dictionary<int, RocketData>();
            foreach (DTRocket dTRocket in m_DT)
            {
                if (!m_RDDic.ContainsKey(dTRocket.Id))
                {
                    m_RDDic.Add(dTRocket.Id, new RocketData(dTRocket));
                }
            }
        }

        public RocketData GetRocketData(int rocketId)
        {
            if(m_RDDic.ContainsKey(rocketId))
            {
                return m_RDDic[rocketId];
            }
            return null;
        }

        protected override void OnUnLoad()
        {
            base.OnUnLoad();
            m_DT.RemoveAllDataRows();
            m_DT = null;
            m_RDDic = null;
        }
    }
}
