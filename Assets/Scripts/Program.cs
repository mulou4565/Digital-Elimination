using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console2048
{
    class Program
    {
        //消除类游戏做法
        //数据(类，工具，位置) 逻辑控制(核心算法) 与 显示(界面代码)
        //松散耦合
        //面向对象思想：分而治之，高内聚低耦合
        static void Main(string[] args)
        {
            GameCore core = new GameCore();

            Location? loc;
            int? number;
            core.GenerateNumber(out loc, out number);
            core.GenerateNumber(out loc, out number);

            //显示界面
            DrawMap(core.Map);
            while(true)
            {
                //移动
                KeyDown(core);
                //判断map中的数据有无变化
                if(core.IsChange)
                {
                    core.GenerateNumber(out loc, out number);
                    DrawMap(core.Map);
                    //如果游戏结束。。。。
                }
            }
        }
        private static void DrawMap(int[,] map)
        {
            Console.Clear();
            for(int r=0;r<map.GetLength(0);r++)
            {
                for(int c=0;c<map.GetLength(1);c++)
                {
                    Console.Write(map[r,c]+"\t");
                }
                Console.WriteLine();
            }
        }
        private static void KeyDown(GameCore core)
        {
            switch(Console.ReadLine())
            {
                case "w":
                    core.Move(MoveDirection.Up);
                    break;
                case "s":
                    core.Move(MoveDirection.Down);
                    break;
                case "a":
                    core.Move(MoveDirection.Left);
                    break;
                case "d":
                    core.Move(MoveDirection.Right);
                    break;
            }
        }
        
    }
}
