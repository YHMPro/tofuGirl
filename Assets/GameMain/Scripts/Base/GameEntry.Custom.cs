using UnityEngine;
using UnityGameFramework.Runtime;
using Project.TofuGirl.Data;
using Project.TofuGirl;
namespace Project
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static DataComponent Data { get; private set; }

        public static CoroutineComponent Coroutine { get; private set; }
        private static void InitCustomComponents()
        {
            Data =FindObjectOfType<DataComponent>();
            Coroutine= FindObjectOfType<CoroutineComponent>();
        }
    }
}
