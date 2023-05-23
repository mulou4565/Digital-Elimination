using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console2048
{
    /// <summary>
    /// 位置
    /// </summary>
    struct Location
    {
        /// <summary>
        /// 行索引
        /// </summary>
        public int RIndex{get;set;}
        /// <summary>
        /// 列索引
        /// </summary>
        public int CIndex{get;set;}
        /// <summary>
        /// 创建一个新位置
        /// </summary>
        /// <param name="rIndex">行索引</param>
        /// <param name="cIndex">列索引</param>
        public Location(int rIndex, int cIndex):this()
        {
            this.RIndex = rIndex;
            this.CIndex = cIndex;
        }
    }
}
