using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project.TofuGirl.Data
{
    /// <summary>
    /// 数据接口
    /// </summary>
    public interface IData
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }
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
    }
    /// <summary>
    /// 数据类
    /// </summary>
    public abstract class Data : IData
    {
        public virtual string Name
        {
            get
            {
                return this.GetType().ToString();
            }
        }
        public abstract void Init();     
        public abstract void Preload();
        public abstract void Load();
        public abstract void Unload();
    }
}
