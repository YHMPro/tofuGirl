
namespace Project.TofuGirl.Data
{
    public sealed class BattenData
    {
        private DTBatten m_Batten;
        public int Id => m_Batten.Id;
        public float SpeedBase => m_Batten.SpeedBase;
        public float SpeedMin => m_Batten.SpeedMin;
        public float SpeedMax => m_Batten.SpeedMax;

        public int DirBase => m_Batten.DirBase;
        public int DirMin => m_Batten.DirMin;
        public int DirMax => m_Batten.DirMax;

        public float CreateTimeBase => m_Batten.CreateTimeBase;
        public float CreateTimeMin => m_Batten.CreateTimeMin;
        public float CreateTimeMax => m_Batten.CreateTimeMax;

        public BattenData(DTBatten dTBatten)
        {
            m_Batten = dTBatten;
        }
    }
}
