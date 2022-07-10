
using GameFramework;
using UnityEngine;
namespace Project.TofuGirl.Entity
{
    public abstract class GOEntityData : IReference
    {
        public int OrderInLayer { get; protected set; }
        public object UserData { get; protected set; }
        public EnumEntity EntityType { get; protected set; }
        public Vector3 Position { get; protected set; }
        public Vector3 Rotation { get; protected set; }
        public virtual void Clear()
        {
            UserData = null;
        }        
    }
}
