
namespace Project.TofuGirl.Data
{
    public sealed class BattenData
    {
        private DTBatten m_Batten;
        public int Id => m_Batten.Id;
        /// <summary>
        /// 木条基础移速
        /// </summary>
        public float SpeedBase => m_Batten.SpeedBase;
        /// <summary>
        /// 火箭附加移速
        /// </summary>
        public float RocketAppendSpeed => m_Batten.RocketAppendSpeed;
        /// <summary>
        /// 右方向概率
        /// </summary>
        public int RightDir => m_Batten.RightDir;
        /// <summary>
        /// 左方向概率
        /// </summary>
        public int LeftDir => m_Batten.LeftDir;
        /// <summary>
        /// 基础创建时间
        /// </summary>
        public float CreateTimeBase => m_Batten.CreateTimeBase;
        /// <summary>
        /// 火箭影响下的创建时间
        /// </summary>
        public float RocketCreateTime => m_Batten.RocketCreateTime;
        /// <summary>
        /// 木条之间的创建间隔
        /// </summary>
        public float Interval => m_Batten.Interval;
        public BattenData(DTBatten dTBatten)
        {
            m_Batten = dTBatten;

        }
    }
}
