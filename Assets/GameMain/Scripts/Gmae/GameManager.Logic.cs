


namespace Project.TofuGirl
{
    /// <summary>
    /// 游戏管理器(逻辑)
    /// </summary>
    public partial class GameManager
    {

        private float m_ElapseSeconds = 0;
        /// <summary>
        /// 台阶构建
        /// </summary>
        private bool m_StairGenerate = false;
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
        /// 台阶构建
        /// </summary>
        /// <param name="elapseSeconds"></param>
        /// <param name="realElapseSeconds"></param>
        private void StairGenerate(float elapseSeconds, float realElapseSeconds)
        {
            m_ElapseSeconds += elapseSeconds;
            if (m_ElapseSeconds <= StairGenerateTime())//满足创建时间即可创建,受女孩死亡事件影响
            {
                return;
            }
            m_ElapseSeconds = 0;
            #region 台阶桥接数据修改      
            BattenBridgeDataUpdate();
            TofuBridgeDataUpdate();
            #endregion

            #region 台阶生成
            BuilderStairEntity();
            #endregion
        }
    }
}
