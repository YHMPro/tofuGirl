

using GameFramework.Procedure;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;
using GameFramework.Event;
using Project.TofuGirl.Data;
using UnityEngine;
using System;
using GameFramework.DataTable;

namespace Project.TofuGirl
{
    /// <summary>
    /// 预加载流程
    /// </summary>
    public class ProcedurePreload : ProcedureBase
    {  
        private DataBase[] m_DataBases;
        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Data.Data[] datas = GameEntry.Data.GetAllData();
            m_DataBases = new DataBase[datas.Length];
            for (int index = 0; index < datas.Length; index++)
            {
                if (datas[index] is DataBase)
                {
                    m_DataBases[index] = datas[index] as DataBase;
                }
            }
            PreLoadData();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            #region 其它情况

            #endregion

            #region 数据表加载情况
            if (m_DataBases == null)
            {
                return;
            }
            foreach (DataBase dataBase in m_DataBases)
            {
                if (!dataBase.LoadCondition)
                {
                    return;
                }
            }
            #endregion        
            LoadData();
            ChangeState<ProcedureMenu>(procedureOwner);
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

        }


        private void OnLoadConfigSuccess(object sender,GameEventArgs args)
        {

        }

        private void OnLoadConfigFailure(object sender,GameEventArgs args)
        {

        }

        /// <summary>
        /// 预加载数据
        /// </summary>
        private void PreLoadData()
        {
            GameEntry.Data.PreLoadAllData();//预加载数据
        }

        private void LoadData()
        {
            GameEntry.Data.LoadAllData();//提取预加载的数据
        }
    }

}
