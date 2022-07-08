
using UnityGameFramework.Runtime;
using GameFramework.Event;
using System.Collections.Generic;
namespace Project.TofuGirl.Data
{   
    /// <summary>
    /// 数据基类
    /// </summary>
    public abstract class DataBase : Data
    {
        private Dictionary<string, bool> m_LoadConditionDic;//加载情况  
        /// <summary>
        /// 加载情况  提供给预加载流程当中调用
        /// </summary>
        public bool LoadCondition
        {
            get
            {
                foreach(bool condition in m_LoadConditionDic.Values)
                {
                    if(!condition)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public sealed override void Init()
        {
            m_LoadConditionDic = new Dictionary<string, bool>();
            OnInit();
        }
        public sealed override void Preload()
        {
            GameEntry.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            GameEntry.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
            OnPreload();
        }

        public sealed override void Load()
        {
            GameEntry.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            GameEntry.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
            OnLoad();
        }

        public sealed override void Unload()
        {
            OnUnLoad();
        }
        /// <summary>
        /// 监听初始化
        /// </summary>
        protected virtual void OnInit()
        {

        }
        /// <summary>
        /// 监听预加载
        /// </summary>
        protected virtual void OnPreload()
        {

        }
        /// <summary>
        /// 监听加载
        /// </summary>
        protected virtual void OnLoad()
        {

        }
        /// <summary>
        /// 监听卸载
        /// </summary>
        protected virtual void OnUnLoad()
        {

        }
        /// <summary>
        /// 加载数据表
        /// </summary>
        /// <param name="dataTableName">数据表名称</param>
        protected void LoadDataTable(string dataTableName)
        {
            string dataTableAssetsName = AssetUtility.GetDataTableAsset(dataTableName);
            m_LoadConditionDic.Add(dataTableAssetsName, false);
            GameEntry.DataTable.LoadDataTable(dataTableName, dataTableAssetsName,this);
        }

        private void OnLoadDataTableSuccess(object sender,GameEventArgs gEArgs)
        {
            LoadDataTableSuccessEventArgs args= (LoadDataTableSuccessEventArgs)gEArgs;
            if(args.UserData!=this)//用于剔除掉不属于自身触发的回调事件
            {
                return;    
            }
            m_LoadConditionDic[args.DataTableAssetName] = true;
            Log.Info("数据表加载成功:" + args.DataTableAssetName);
        }
        private void OnLoadDataTableFailure(object sender, GameEventArgs gEArgs)
        {
            LoadDataTableFailureEventArgs args = (LoadDataTableFailureEventArgs)gEArgs;
            if (args.UserData != this)//用于剔除掉不属于自身触发的回调事件
            {
                return;
            }
            Log.Info("数据表加载失败:" + args.DataTableAssetName);
        }
    }

}
