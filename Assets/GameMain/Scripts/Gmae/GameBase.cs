

using GameFramework;
namespace Project.TofuGirl
{
    public abstract class GameBase :Game    
    {
        protected float m_ElapseSeconds = 0f;

        /// <summary>
        /// 游戏结束
        /// </summary>
        public bool GameOver
        {
            get;
            protected set;
        }
        /// <summary>
        /// 游戏模式
        /// </summary>
        public EnumGameModel GameModel
        {
            get;
            protected set;
        }
        /// <summary>
        /// 相机Id
        /// </summary>
        public int CameraSerialId { get; protected set; }
        /// <summary>
        /// 女孩Id
        /// </summary>
        public int GirlSerialId { get; protected set; }
        /// <summary>
        /// 当前豆腐Id
        /// </summary>
        public int NowTufoSerialId { get; protected set; }
        /// <summary>
        /// 当前木条Id
        /// </summary>
        public int NowBattenSerialId { get; protected set; }
        public sealed override void Init()
        {
            GameOver = false;
            OnInit();
        }

        public sealed override void Load()
        {
            OnLoad();
        }

        public sealed override void Preload()
        {
            OnPreload();
        }

        public sealed override void Unload()
        {
            OnUnload();
        }

        public sealed override void Update(float elapseSeconds, float realElapseSeconds)
        {
            OnUpdate(elapseSeconds, realElapseSeconds);
        }

        protected virtual void OnInit()
        {

        }

        protected virtual void OnLoad()
        {

        }

        protected virtual void OnPreload()
        {

        }

        protected virtual void OnUnload()
        {

        }

        protected virtual void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {

        }

        
    }
}
