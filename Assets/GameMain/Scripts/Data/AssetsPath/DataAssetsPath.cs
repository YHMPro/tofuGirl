
using GameFramework.DataTable;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
namespace Project.TofuGirl.Data
{  
    public sealed class DataAssetsPath : DataBase
    {
        private IDataTable<DTAssetsPath> m_DT;

        private Dictionary<int, AssetsPathData> m_APDDic;


        protected override void OnInit()
        {
            base.OnInit();
        }

        protected override void OnPreload()
        {
            base.OnPreload();
            LoadDataTable("AssetsPath");
        }
        protected override void OnLoad()
        {
            m_DT = GameEntry.DataTable.GetDataTable<DTAssetsPath>();
            m_APDDic = new Dictionary<int, AssetsPathData>();
            foreach(DTAssetsPath dTAssetsPath in m_DT)
            {
                if(!m_APDDic.ContainsKey(dTAssetsPath.Id))
                {
                    m_APDDic.Add(dTAssetsPath.Id, new AssetsPathData(dTAssetsPath));
                }
            }
        }

        protected override void OnUnLoad()
        {
            base.OnUnLoad();

        }


        public AssetsPathData GetAssetsPathData(int assetsId)
        {
            if(m_APDDic.ContainsKey(assetsId))
            {
                return m_APDDic[assetsId];
            }
            return null;
        }

    }
}
