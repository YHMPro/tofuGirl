

using GameFramework.Entity;
using UnityGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Project.TofuGirl.Event;
using GameFramework.Event;
namespace Project.TofuGirl.Entity
{
    public abstract class GOEntityLogic : EntityLogic, IPause
    {
        private GOEntityData m_EntityData;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }
        protected override void OnShow(object userData)
        {
            
            base.OnShow(userData);           
            m_EntityData = userData as GOEntityData;
            if(m_EntityData==null)
            {
                return;
            }
            transform.position = m_EntityData.Position;
            transform.eulerAngles = m_EntityData.Rotation;
        }
        protected override void OnHide(bool isShutdown, object userData)
        {            
            base.OnHide(isShutdown, userData);
        }        
        public abstract void Pause(object userData);
        /// <summary>
        /// 延迟(会受到TimeScale的影响)
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="callback"></param>
        protected Coroutine Delay(float delay, UnityAction callback)
        {
            return StartCoroutine(IEDelay(delay, callback));
        }
        /// <summary>
        /// 延迟(会受到TimeScale的影响)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="delay"></param>
        /// <param name="info"></param>
        /// <param name="callback"></param>
        protected Coroutine Delay<T>(float delay,T info, UnityAction<T> callback)
        {
            return StartCoroutine(IEDelay(delay,info, callback));
        }
        private IEnumerator IEDelay(float delay,UnityAction callback)
        {
            yield return new WaitForSeconds(delay);
            callback?.Invoke();
        }
        private IEnumerator IEDelay<T>(float delay,T info, UnityAction<T> callback)
        {
            yield return new WaitForSeconds(delay);
            callback?.Invoke(info);
        }
       
    }
}
