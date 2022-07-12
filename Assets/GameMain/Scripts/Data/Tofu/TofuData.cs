

namespace Project.TofuGirl.Data
{
    public sealed class TofuData 
    {
        private DTTofu m_Tofu;
        /// <summary>
        /// 豆腐配置Id
        /// </summary>
        public int Id => m_Tofu.Id;
        /// <summary>
        /// 普通豆腐概率
        /// </summary>
        public int PuTong => m_Tofu.PuTong;
        /// <summary>
        /// 道具豆腐概率
        /// </summary>
        public int DaoJu => m_Tofu.DaoJu;
        /// <summary>
        /// 特殊豆腐触发最小值
        /// </summary>
        public int TeShuMin => m_Tofu.TeShuMin;
        /// <summary>
        /// 特殊豆腐触发临界值
        /// </summary>
        public int TeShuMax => m_Tofu.TeShuMax;
        /// <summary>
        /// 金色特殊豆腐触发最小值
        /// </summary>
        public int JinSheTeShuMin => m_Tofu.JinSheTeShuMin;
        /// <summary>
        /// 金色特殊豆腐触发临界值
        /// </summary>
        public int JinSheTeShuMax => m_Tofu.JinSheTeShuMax;
        public TofuData(DTTofu dTTofu)
        {
            m_Tofu = dTTofu;
        }
    }
}
