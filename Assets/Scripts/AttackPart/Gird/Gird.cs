using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{

    // 坐标点
    public Vector2 Point;

    // 世界坐标
    public Vector2 Position;

    public bool HaveEnemy;
    // 构造函数
    public Grid(Vector2 point, Vector2 position, bool haveEnemy)
    {
        Point = point;
        Position = position;
        HaveEnemy = haveEnemy;
    }
}
