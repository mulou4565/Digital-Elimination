using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Console2048;
using MoveDirection = Console2048.MoveDirection;

/// <summary>
/// 游戏控制器
/// </summary>
public class GameController : MonoBehaviour, IPointerDownHandler, IDragHandler 
{
    private GameCore core;
    private NumberSprite action;
    private NumberSprite[,] spriteActionArray;
    private void Start()
    {
        Init();
        core = new GameCore();
        spriteActionArray = new NumberSprite[4,4];

        GenerateNewNumber();
        GenerateNewNumber();
    }

    private void Init()
    {
        for(int r = 0; r < 4; r++) 
        {
            for(int c = 0; c < 4; c++) 
            {
                CreateSprite(r,c);    
            }
        }
    }

    private void CreateSprite(int r, int c)
    {
        //创建UI元素
        GameObject go = new GameObject(r.ToString() + c.ToString());
        //读取精灵0 2 4 8 16 ...
        //设置图片
        go.AddComponent<Image>();
        action = go.AddComponent<NumberSprite>();
        //将引用存储在二维数组中
        spriteActionArray[r,c] = action;
        action.SetImage(0);
        //创建的游戏对象，scale应默认为相对父物体的1，false不使用世界坐标系
        go.transform.SetParent(this.transform, false);
    }

    //生成一个新数字，以精灵UI显示在界面中
    private void GenerateNewNumber()
    {
        Location? loc;
        int? number;
        //生成一个新数字
        core.GenerateNumber(out loc, out number);
        //根据位置获取精灵行为脚本对象引用，根据生成的数字设置相应的精灵UI
        spriteActionArray[loc.Value.RIndex, loc.Value.CIndex].SetImage(number.Value);
        spriteActionArray[loc.Value.RIndex, loc.Value.CIndex].CreateEffect();
    }

    private void Update() 
    {
        if(core.IsChange)
        {
            UpdateMap();
            GenerateNewNumber();
            //判断游戏是否结束
            
            core.IsChange = false;
        }    
    }

    private void UpdateMap()
    {
        for(int r = 0; r < 4; r++) 
        {
            for(int c = 0; c < 4; c++) 
            {
                spriteActionArray[r, c].SetImage(core.Map[r,c]);    
            }    
        }
    }
    
    private Vector2 startPoint;
    private bool isDown;
    public void OnPointerDown(PointerEventData eventData)
    {
        startPoint = eventData.position;
        isDown = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!isDown)
        {
            return;
        }
        Vector2 offset = eventData.position - startPoint;
        float x = MathF.Abs(offset.x);
        float y = MathF.Abs(offset.y);

        MoveDirection? dir = null;         
        //水平
        if(x > y && x >= 50)
        {
            dir = offset.x > 0 ? MoveDirection.Right : MoveDirection.Left;
        }
        //垂直
        if(x < y && y <= 50)
        {
            dir = offset.y > 0 ? MoveDirection.Up : MoveDirection.Down;
        }

        if(dir != null)
        {
            core.Move(dir.Value);
            isDown = false;
        }
    }
}
