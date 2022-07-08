
using GameFramework;
namespace Project.TofuGirl
{
    public abstract class Game : IGame
    {     
        public abstract void Init();
        public abstract void Load();
        public abstract void Preload();
        public abstract void Unload();
        public abstract void Update(float elapseSeconds, float realElapseSeconds);
    }
}
