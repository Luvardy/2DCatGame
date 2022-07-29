using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{

    // 坐标点
    public Vector2 Point;

    // 世界坐标
    public Vector2 Position;

    // 是否靠近猫，如果是才可以放
    public bool NearKing;
    // 构造函数
    public Grid(Vector2 point, Vector2 position, bool nearKing)
    {
        Point = point;
        Position = position;
        NearKing = nearKing;
    }
}
