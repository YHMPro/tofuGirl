using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project.TofuGirl
{
    public static class RandRateTool
    {
        /// <summary>
        /// 概率分配计算
        /// </summary>
        /// <param name="rate">概率数据</param>
        /// <param name="total">总概率</param>
        /// <returns></returns>
        public static int RandRate(int[] rate, int total)
        {
            int rand = Random.Range(0, total + 1);
            for (int i = 0; i < rate.Length; i++)
            {
                rand -= rate[i];
                if (rand <= 0)
                {
                    return i;
                }
            }
            return 0;
        }
        /// <summary>
        /// 木条移动方向概率计算
        /// </summary>
        /// <param name="leftRate">左概率</param>
        /// <param name="rightRate">右概率</param>
        /// <returns></returns>
        public static EnumBattenMove BattenMoveRandRate(int leftRate,int rightRate)
        {
            if(RandRate(new int[] { leftRate,rightRate },leftRate+ rightRate)==0)
            {
                return EnumBattenMove.Left;
            }
            return EnumBattenMove.Right;         
        }
    }
}
