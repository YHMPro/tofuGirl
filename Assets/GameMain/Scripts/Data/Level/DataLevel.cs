



using GameFramework.DataTable;
using UnityGameFramework.Runtime;
using System.Collections.Generic;
namespace Project.TofuGirl.Data
{
    public class DataLevel :DataBase
    {
        private IDataTable<DTLevel> m_DT;

        private Dictionary<int, LevelData> m_LDDic;

        protected override void OnPreload()
        {
            base.OnPreload();
            LoadDataTable("Level");
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            //提取预加载的数据
            m_DT = GameEntry.DataTable.GetDataTable<DTLevel>();
            m_LDDic = new Dictionary<int, LevelData>();
            foreach (DTLevel dTLevel in m_DT)
            {
                if (!m_LDDic.ContainsKey(dTLevel.Id))
                {
                    LevelData levelData = new LevelData(
                        dTLevel,                       
                        GameEntry.Data.GetData<DataGirl>().GetGirlData(dTLevel.GirlDataId),
                        GameEntry.Data.GetData<DataBatten>().GetBattenData(dTLevel.BattenDataId),
                        GameEntry.Data.GetData<DataRocket>().GetRocketData(dTLevel.RocketDataId),
                        GameEntry.Data.GetData<DataTofu>().GetTofuData(dTLevel.TofuDataId)
                        );              
                    m_LDDic.Add(dTLevel.Id, levelData);
                }
            }
        }

        public LevelData GetLevelData(int levelId)
        {
            if (m_LDDic.ContainsKey(levelId))
            {
                return m_LDDic[levelId];
            }
            return null;
        }
    }
}
