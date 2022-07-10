
using GameFramework.DataTable;
using System.Collections.Generic;
namespace Project.TofuGirl.Data
{  
    public sealed class DataEntity : DataBase
    {
        private IDataTable<DTEntity> m_DT;

        private Dictionary<int, EntityData> m_EDDic;

        protected override void OnInit()
        {
            base.OnInit();    
        }

        protected override void OnPreload()
        {
            LoadDataTable("Entity");
        }
        protected override void OnLoad()
        {
            //提取预加载的数据
            m_DT=GameEntry.DataTable.GetDataTable<DTEntity>();
            m_EDDic = new Dictionary<int, EntityData>();

            foreach(DTEntity dTEntity in m_DT)
            {
                if(!m_EDDic.ContainsKey(dTEntity.Id))
                {
                    EntityData entityData = new EntityData(
                          dTEntity,
                          GameEntry.Data.GetData<DataAssetsPath>().GetAssetsPathData(dTEntity.AssetId),
                          GameEntry.Data.GetData<DataEntityGroup>().GetEntityGroupData(dTEntity.EntityGroupId)
                        );
                    m_EDDic.Add(dTEntity.Id, entityData);
                }
            }


        }

        protected override void OnUnLoad()
        {
            base.OnUnLoad();

        }

        public EntityData GetEntityData(int entityId)
        {
            if(m_EDDic.ContainsKey(entityId))
            {
                return m_EDDic[entityId];
            }
            return null;
        }
    }
}
