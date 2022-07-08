



namespace Project.TofuGirl.Data
{
    public sealed class GirlData
    {
        private DTGirl m_Girl;

        public int Id => m_Girl.Id;

        public float BaseSpeed => m_Girl.BaseSpeed;

        public GirlData(DTGirl dTGirl)
        {
            m_Girl = dTGirl;
        }
    }
}
