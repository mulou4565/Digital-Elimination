using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console2048
{
    /// <summary>
    /// 资源管理类，负责加载资源到内存，仅加载一次
    /// </summary>
    public class ResourceManager
    {
        //存储数据的容器最好使用字典集合
        private static Dictionary<int, Sprite> spriteDic;
        private static Sprite[] spriteArray;

        //类被加载时调用
        static ResourceManager()
        {
            spriteDic = new Dictionary<int, Sprite>();
            //读取单个精灵用Load，读取精灵图集用LoadAll
            spriteArray = Resources.LoadAll<Sprite>("2048");
            foreach(var item in spriteArray) 
            {
                int intSpriteName = int.Parse(item.name);
                spriteDic.Add(intSpriteName, item);
            }
        }

        /// <summary>
        /// 读取数字精灵
        /// </summary>
        /// <param name="number">精灵表示的数字</param>
        /// <returns>精灵</returns>
        public static Sprite LoadSprite(int number)
        {
            return spriteDic[number];
        }
    }
}
