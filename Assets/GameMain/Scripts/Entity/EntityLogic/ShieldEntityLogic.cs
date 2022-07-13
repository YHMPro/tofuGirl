
using UnityEngine;
using Project.TofuGirl.Event;
namespace Project.TofuGirl.Entity
{
    /// <summary>
    /// 盾牌实体逻辑
    /// </summary>
    public class ShieldEntityLogic :GOAnimatorEntityLogic
    {
        private ShieldEntityData m_EntityData;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);


        }


        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_EntityData = userData as ShieldEntityData;
            if (m_EntityData == null)
            {
                return;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameEntry.Event.Fire(this,ShieldWithGirlDetachEventArgs .Create());
            GameEntry.Entity.HideEntity(Entity);
        }
    }
}
