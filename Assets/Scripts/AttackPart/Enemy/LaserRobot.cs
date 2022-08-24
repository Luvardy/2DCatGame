using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRobot : Enemy
{
    protected Vector2 enemyPos;
    public bool moveUp;

    void Start()
    {
        HP = 20;
    }

    protected override void MoveEnemy()
    {

    }
}