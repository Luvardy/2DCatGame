using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{

    // 坐标点
    public Vector2 Point;

    // 世界坐标
    public Vector2 Position;

    public GameObject CanPlace;
    // 构造函数
    public Grid(Vector2 point, Vector2 position, GameObject canPlace)
    {
        Point = point;
        Position = position;
        CanPlace = canPlace;
    }
}
