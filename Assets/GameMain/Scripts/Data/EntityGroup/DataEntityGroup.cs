
using GameFramework.DataTable;
using System.Collections.Generic;
namespace Project.TofuGirl.Data
{
    public sealed class DataEntityGroup : DataBase
    {
        private IDataTable<DTEntityGroup> m_DT;

        private Dictionary<int, EntityGroupData> m_EGDDic;

        protected override void OnInit()
        {
            base.OnInit();

        }

        protected override void OnPreload()
        {
            base.OnPreload();
            LoadDataTable("EntityGroup");
        }

        protected override void OnLoad()
        {
            m_DT = GameEntry.DataTable.GetDataTable<DTEntityGroup>();
            m_EGDDic = new Dictionary<int, EntityGroupData>();

            foreach(DTEntityGroup dTEntityGroup  in m_DT)
            {
                if(!m_EGDDic.ContainsKey(dTEntityGroup.Id))
                {
                    EntityGroupData entityGroupData = new EntityGroupData
                        (
                          dTEntityGroup,
                          GameEntry.Data.GetData<DataPoolParam>().GetPoolParamData(dTEntityGroup.PoolParamId)
                        );
                    m_EGDDic.Add(dTEntityGroup.Id, entityGroupData);
                }
            }
        }

        protected override void OnUnLoad()
        {
            base.OnUnLoad();

        }

        public EntityGroupData GetEntityGroupData(int entityGroupId)
        {
            if(m_EGDDic.ContainsKey(entityGroupId))
            {
                return m_EGDDic[entityGroupId];
            }
            return null;
        }
    }
}
