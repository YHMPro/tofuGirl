

using GameFramework;
using Project.TofuGirl.Data;
namespace Project.TofuGirl
{

    public partial class GameManager
    {
        public static GameManager Create(LevelData levelData)
        {
            return Init(ReferencePool.Acquire<GameManager>(), levelData);
        }
        private static GameManager Init(GameManager gm, LevelData levelData)
        {
            gm.m_LData = levelData;
            //事件注册
            gm.GMEventSubscribe();
            //数据初始化
            gm.DataInit();
            //实体初始化
            gm.EntityInit();            
            return gm;
        }

        public void Clear()
        {
            GMEventUnsubscribe();
            m_BBData = null;
            m_CBData = null;
            m_GBData = null;
            m_LData = null;
            m_SBData = null;
            m_TBData = null;
            m_RBData = null;
            m_ELoader.Clear();
            m_ELoader = null;
        }
    }
}
