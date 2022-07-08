


namespace Project.TofuGirl
{
    public interface IGame
    {       
        /// <summary>
        /// 初始化
        /// </summary>
        void Init();
        /// <summary>
        /// 预加载
        /// </summary>
        void Preload();
        /// <summary>
        /// 加载
        /// </summary>
        void Load();
        /// <summary>
        /// 卸载
        /// </summary>
        void Unload();
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        void Update(float elapseSeconds, float realElapseSeconds);
    }
    /// <summary>
    /// 暂停接口
    /// </summary>
    public interface IPause
    {
        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="userData"></param>
        void Pause(object userData);
    }
}