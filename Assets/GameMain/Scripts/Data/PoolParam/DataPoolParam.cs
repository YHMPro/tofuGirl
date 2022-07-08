


using GameFramework.DataTable;
using UnityGameFramework.Runtime;
using System.Collections.Generic;
namespace Project.TofuGirl.Data
{
    public sealed class DataPoolParam : DataBase
    {
        private IDataTable<DTPoolParam> m_DT;

        private Dictionary<int, PoolParamData> m_PPDDic;


        protected override void OnInit()
        {
            base.OnInit();

        }

        protected override void OnPreload()
        {
            base.OnPreload();
            LoadDataTable("PoolParam");
        }

        protected override void OnLoad()
        {
            m_DT = GameEntry.DataTable.GetDataTable<DTPoolParam>();
            m_PPDDic = new Dictionary<int, PoolParamData>();
            foreach (DTPoolParam dTPoolParam in m_DT)
            {
                if (!m_PPDDic.ContainsKey(dTPoolParam.Id))
                {
                    m_PPDDic.Add(dTPoolParam.Id, new PoolParamData(dTPoolParam));
                }
            }
        }

        protected override void OnUnLoad()
        {
            base.OnUnLoad();

        }

        public PoolParamData GetPoolParamData(int poolParamId)
        {
            if(m_PPDDic.ContainsKey(poolParamId))
            {
                return m_PPDDic[poolParamId];
            }
            return null;
        }
    }
}
