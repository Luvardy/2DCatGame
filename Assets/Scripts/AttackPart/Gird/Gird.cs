using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{

    // 坐标点
    public Vector2 Point;

    // 世界坐标
    public Vector2 Position;

    // 是否有猫，如果有则不能召唤猫
    public bool HaveKing;
    // 构造函数
    public Grid(Vector2 point, Vector2 position, bool haveKing)
    {
        Point = point;
        Position = position;
        HaveKing = haveKing;
    }
}
