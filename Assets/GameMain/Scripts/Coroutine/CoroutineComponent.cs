using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Project
{
    /// <summary>
    /// 协程
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Coroutine")]
    public class CoroutineComponent : MonoBehaviour
    {
        public Coroutine Delay(float delayTime, UnityAction callback, bool realtime = false)
        {
            return StartCoroutine(IEDelay(delayTime, callback, realtime));
        }
        public Coroutine Delay<T>(float delayTime,T info, UnityAction<T> callback, bool realtime = false)
        {
            return StartCoroutine(IEDelay(delayTime,info,callback, realtime));
        }
        private IEnumerator IEDelay(float delayTime, UnityAction callback, bool realtime = false)
        {
            if(realtime)
            {
                yield return new WaitForSecondsRealtime(delayTime);
            }
            else
            {
                yield return new WaitForSeconds(delayTime);
            }
            callback?.Invoke();
        }
        private IEnumerator IEDelay<T>(float delayTime,T info, UnityAction<T> callback, bool realtime = false)
        {
            if (realtime)
            {
                yield return new WaitForSecondsRealtime(delayTime);
            }
            else
            {
                yield return new WaitForSeconds(delayTime);
            }
            callback?.Invoke(info);
        }
    }
}
