

namespace Project.TofuGirl.Animator
{
    public interface IAnimator
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="userData"></param>
        void Init(object userData);
        /// <summary>
        /// 继续播放
        /// </summary>
        void ContinuePlay();
        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="animName"></param>
        /// <param name="loop"></param>
        void Play(string animName,bool loop=false);
        /// <summary>
        /// 暂停
        /// </summary>
        void Pause();
        /// <summary>
        /// 设置衣服
        /// </summary>
        /// <param name="skinName"></param>
        void SetSkin(string skinName);
        /// <summary>
        /// 重置
        /// </summary>
        void Reset();
    }
    public abstract class Animator : IAnimator
    {
        public abstract void Init(object userData);
        public abstract void Pause();
        public abstract void ContinuePlay();
        public abstract void Play(string animName, bool loop = false);
        public abstract void SetSkin(string skinName);
        public abstract void Reset();
    }
}
