using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot2 : Enemy
{
    protected Vector2 enemyPos;
    public bool moveUp;
    public float moveDistance;

    void Start()
    {
        HP = 20;
        enemyPos = transform.position;
    }

    protected override void MoveEnemy()
    {
        if (!moveUp && transform.position.y - enemyPos.y >= -moveDistance)
        {
            animator.SetBool("WalkRight", true);
            transform.Translate(Vector2.down * Time.deltaTime);
        }
        else
        {
            animator.SetBool("WalkRight", false);
            moveUp = true;
        }

        if (moveUp && transform.position.y - enemyPos.y <= moveDistance)
        {
            animator.SetBool("WalkLeft", true);
            transform.Translate(Vector2.up * Time.deltaTime);
        }
        else
        {
            moveUp = false;
            animator.SetBool("WalkLeft", false);
        }
    }
}
