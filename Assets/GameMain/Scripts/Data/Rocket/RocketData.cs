


namespace Project.TofuGirl.Data
{
    public sealed class RocketData 
    {
        private DTRocket m_Rocket;
        /// <summary>
        /// 飞行速度
        /// </summary>
        public float Speed => m_Rocket.BaseSpeed;
        /// <summary>
        /// 越过的豆腐数量
        /// </summary>
        public int TofuNum => m_Rocket.TofuNum;

        public RocketData(DTRocket dTRocket)
        {
            m_Rocket = dTRocket;
        }
    }
}
