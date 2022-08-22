using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot1 : Enemy
{
    protected Vector2 enemyPos;
    public bool moveLeft;
    public float moveDistance;

    void Start()
    {
        HP = 20;
        enemyPos = transform.position;
    }

    protected override void MoveEnemy()
    {
        if(!moveLeft && transform.position.x - enemyPos.x <= moveDistance)
        {
            animator.SetBool("WalkRight",true);
            transform.Translate(Vector2.right * Time.deltaTime);
        }
        else
        {
            animator.SetBool("WalkRight", false);
            moveLeft = true;
        }

        if(moveLeft && transform.position.x - enemyPos.x >= -moveDistance)
        {
            animator.SetBool("WalkLeft",true);
            transform.Translate(Vector2.left * Time.deltaTime);
        }
        else
        {
            moveLeft = false;
            animator.SetBool("WalkLeft", false);
        }
    }
}
