

using UnityGameFramework.Runtime;
using Project.TofuGirl.Event;
namespace Project.TofuGirl
{
    /// <summary>
    /// 游戏管理器(逻辑)
    /// </summary>
    public partial class GameManager
    {
        private float m_ElapseSeconds = 0;       
        /// <summary>
        /// 台阶创建的时间
        /// </summary>
        private float m_StairGenerateTime = 0;       
        /// <summary>
        /// 火箭触发的台阶构建数量(每次火箭使用结束后清零，最大值为配置表中的越过数量)
        /// </summary>
        private int m_RocketTriggerTofuNum = 0;
        /// <summary>
        /// 状态轮询时调用(游戏更新)
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public void LogicUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (!m_StairGenerate)
            {
                return;
            }
            //台阶创建
            StairGenerate(elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// 台阶构建(同步构建)
        /// </summary>
        /// <param name="elapseSeconds"></param>
        /// <param name="realElapseSeconds"></param>
        private void StairGenerate(float elapseSeconds, float realElapseSeconds)
        {
            m_ElapseSeconds += realElapseSeconds;        
            if (m_ElapseSeconds <= m_StairGenerateTime)//满足创建时间即可创建 实际台阶构建所花费的时间不一定等于m_StairGenerateTime,受手机性能影响
            {
                return;
            }
            if(!m_StairGenerateSuccess)//只有上一个台阶构建完成才会进行下一个台阶的构建
            {
                return;
            }
            if(m_RocketBindGirl)
            {
                ++m_RocketTriggerTofuNum;//每次累计加1
                if(m_RocketTriggerTofuNum>m_LData.RData.TofuNum)
                {
                    return;
                }
                //给火箭发送构建事件
                GameEntry.Event.Fire(this, RocketTriggerStairGenerateEventArgs.Create(m_RocketTriggerTofuNum));
            }
            //暂时放这里进行豆腐的回收操作  后续会考虑在特定情况下进行回收  现阶段每构建一个台阶都将触发回收检查操作
            Log.Info("台阶构建重置，进入新一轮构建");
            m_StairGenerateSuccess = false;//进入新一轮台阶构建  重置该字段
            //m_ElapseSeconds = 0; 已在台阶构建成功的事件回调中处理这个字段的值
            #region 台阶桥接数据修改      
            BattenBridgeDataUpdate();
            TofuBridgeDataUpdate();
            #endregion
            #region 台阶创建时间修改
            StairGenerateTimeUpdate();//后续会放在事件中调用
            #endregion
            #region 台阶生成
            BuilderStairEntity();
            #endregion
            TofuEntityAutoRecycle();
        }
    }
}
