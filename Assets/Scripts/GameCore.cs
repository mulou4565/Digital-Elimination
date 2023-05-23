using System;
using System.Collections.Generic;

namespace Console2048
{
    /// <summary>
    /// 游戏核心类，负责处理游戏的核心算法，与界面无关
    /// </summary>
    class GameCore
    {
        #region 成员变量
        //把参数都提取为成员变量，避免每次移动都产生垃圾
        private int[,] map, originalMap;
        private int[] mergeArray;
        private int[] removeZeroArray;
        private List<Location> emptyLocationList;
        private Random random;
        public bool IsChange {get;set;}
        public int[,] Map
        {
            get{
                return map;
            }
        }
        //初始化成员变量
        public GameCore()
        {
            map = new int[4,4];
            originalMap = new int[4,4];
            mergeArray = new int[4];
            removeZeroArray = new int[4];
            emptyLocationList = new List<Location>(16);
            random = new Random();
        }
        #endregion
        #region 合并
        private void RemoveZero()
        {
            //每次去零操作，都先“清空”去零数组元素，即元素归零
            Array.Clear(removeZeroArray, 0, 4);
            int index=0;
            for(int i=0; i<mergeArray.Length; i++){
                if(mergeArray[i] != 0)
                    removeZeroArray[index++] = mergeArray[i];
            }
            removeZeroArray.CopyTo(mergeArray, 0);
        }
        private void Merge()
        {
            RemoveZero();
            for(int i=0; i<mergeArray.Length-1; i++)
            {
                if(mergeArray[i] != 0 && mergeArray[i] == mergeArray[i+1])
                {
                    mergeArray[i] += mergeArray[i+1];
                    mergeArray[i+1] = 0;
                    //积分
                    //之后统计合并位置（r和c），为后续显示动画做准备

                }
            }
            RemoveZero(); 
        }
        #endregion
        #region 移动
        private void MoveUp()
        {
            for(int c=0; c<map.GetLength(1); c++)
            {
                for(int r=0; r<map.GetLength(0); r++)
                    mergeArray[r] = map[r, c];
                Merge();
                for(int r=0; r<map.GetLength(0); r++)
                    map[r, c] = mergeArray[r];
            }
        }
        private void MoveDown()
        {
            for(int c=0; c<map.GetLength(1); c++)
            {
                for(int r=0; r<map.GetLength(0); r++)
                    mergeArray[map.GetLength(0)-1-r] = map[r, c];
                Merge();
                for(int r=0; r<map.GetLength(0); r++)
                    map[r, c] = mergeArray[map.GetLength(0)-1-r];
            }
        }
        private void MoveLeft()
        {
            for(int r=0; r<map.GetLength(0); r++)
            {
                for(int c=0; c<map.GetLength(1); c++)
                    mergeArray[c] = map[r, c];
                Merge();
                for(int c=0; c<map.GetLength(1); c++)
                    map[r, c] = mergeArray[c];
            }
        }
        private void MoveRight()
        {
            for(int r=0; r<map.GetLength(0); r++)
            {
                for(int c=0; c<map.GetLength(1); c++)
                    mergeArray[map.GetLength(1)-1-c] = map[r, c];
                Merge();
                for(int c=0; c<map.GetLength(1); c++)
                    map[r, c] = mergeArray[map.GetLength(1)-1-c];
            }
        }
        public void Move(MoveDirection direction) 
        {
            //移动前记录map --> originalMap
            Array.Copy(map,originalMap,map.Length);
            IsChange = false;
            switch(direction)
            {
                case MoveDirection.Up:
                    MoveUp();
                    break;
                case MoveDirection.Down:
                    MoveDown();
                    break;
                case MoveDirection.Left:
                    MoveLeft();
                    break;
                case MoveDirection.Right:
                    MoveRight();
                    break;
            }
            //移动后对比map
            for(int r=0;r<map.GetLength(0);r++)
            {
                for(int c=0;c<map.GetLength(1);c++)
                {
                    if(map[r,c] != originalMap[r,c])
                    {
                        IsChange = true;
                    }
                }
                
            }
        }
        #endregion
        #region 产生随机数
        //需求：在空白位置随机产生一个2(90%)或4(10%)!!!
        //分析：先计算所有空白位置
        private void CalculateEmpty()
        {
            //每次统计空位置前先“清空”空位置列表
            emptyLocationList.Clear();
            for(int r=0;r<map.GetLength(0);r++)
            {
                for(int c=0;c<map.GetLength(1);c++)
                {
                    if(map[r,c] == 0)
                    {
                        //记录 r c ，一个空位置
                        //类[数据类型] 类数据成员[多个类型]
                        //将多个基本数据类型，封装为一个自定义类型Location
                        emptyLocationList.Add(new Location(r,c));
                    }
                }
            }
        }
        public void GenerateNumber(out Location? loc, out int? number)
        {
            CalculateEmpty();
            //再随机选择一个位置
            if(emptyLocationList.Count > 0)
            {
                int randomIndex =  random.Next(0,emptyLocationList.Count);
                loc = emptyLocationList[randomIndex];
                //产生一个2(90%)或4(10%)
                number = map[loc.Value.RIndex, loc.Value.CIndex] = random.Next(0,10)==1?4:2;
            }
            else
            {
                loc = null;
                number = null;
            }
        }
        #endregion
        #region 游戏结束
        //没有空位置
        //不能合并了
        #endregion
    }
    
}
