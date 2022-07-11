using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.TofuGirl.Entity;
using UnityGameFramework.Runtime;
using GameFramework.Event;
using UnityEngine.Events;
using GameFramework;
namespace Project
{
    /// <summary>
    /// 实体加载器
    /// </summary>
    public class EntityLoader:IReference
    {
        private Dictionary<int, UnityAction<Entity>> m_ShowCallbackDic;

        public EntityLoader()
        {
            m_ShowCallbackDic = new Dictionary<int, UnityAction<Entity>>();
        }

        public static EntityLoader Create()
        {
            EntityLoader entityLoader = ReferencePool.Acquire<EntityLoader>();
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, entityLoader.OnShowEntitySuccess);
            GameEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, entityLoader.OnShowEntityFailure);
            return entityLoader;
        }

        public virtual void Clear()
        {
            m_ShowCallbackDic.Clear();
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, this.OnShowEntitySuccess);
            GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, this.OnShowEntityFailure);
        }

        public int ShowEntity<T>(int serialId, int entityId,UnityAction<Entity> showCallback=null, object userData = null)where T:EntityLogic
        {
            if (showCallback != null)
            {
                m_ShowCallbackDic.Add(serialId, showCallback);
            }
            GameEntry.Entity.ShowEntity<T>(serialId, entityId, userData);
            return serialId;
        }
        private void OnShowEntitySuccess(object sender,GameEventArgs gEArgs)
        {
            ShowEntitySuccessEventArgs args = gEArgs as ShowEntitySuccessEventArgs;
            if(args==null)
            {
                return;
            }
            if(m_ShowCallbackDic.ContainsKey(args.Entity.Id))
            {
                m_ShowCallbackDic[args.Entity.Id]?.Invoke(args.Entity);
                m_ShowCallbackDic.Remove(args.Entity.Id);
            }

        }
        private void OnShowEntityFailure(object sender, GameEventArgs gEArgs)
        {
            ShowEntityFailureEventArgs args = gEArgs as ShowEntityFailureEventArgs;
            if (args == null)
            {
                return;
            }
            if (m_ShowCallbackDic.ContainsKey(args.EntityId))
            {
                m_ShowCallbackDic[args.EntityId]?.Invoke(null);
                m_ShowCallbackDic.Remove(args.EntityId);
            }
            Log.Warning("实体显示失败:{0}", args.ErrorMessage);
        }
    }
}
