


using GameFramework.DataTable;
using UnityGameFramework.Runtime;
using System.Collections.Generic;
namespace Project.TofuGirl.Data
{
    public class DataGirl : DataBase
    {
        private IDataTable<DTGirl> m_DT;

        private Dictionary<int, GirlData> m_GDDic;

        protected override void OnPreload()
        {
            base.OnPreload();
            LoadDataTable("Girl");
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            m_DT = GameEntry.DataTable.GetDataTable<DTGirl>();
            m_GDDic = new Dictionary<int, GirlData>();
            foreach (DTGirl dTGirl in m_DT)
            {
                if (!m_GDDic.ContainsKey(dTGirl.Id))
                {
                    m_GDDic.Add(dTGirl.Id, new GirlData(dTGirl));
                }
            }

        }

        public GirlData GetGirlData(int girlId)
        {
            if (m_GDDic.ContainsKey(girlId))
            {
                return m_GDDic[girlId];
            }
            return null;
        }

    }
}
