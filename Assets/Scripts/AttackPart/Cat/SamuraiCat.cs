using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiCat : CatBase
{
    private void Update()
    {
        FSM();
    }
    protected override void OnInitForPlace()
    {
        HP = 60f;
        AttackPrice = hp;//撞击敌人自损血量
        catCost = 6;
        catSpeed = 2f;
        State = CatState.Move;
        PlayerManager.instance.CatNeedCost(catCost);
    }

    protected override void MoveCat()
    {
        base.MoveCat();

        if (myDir == Vector2.right)
        {
            animator.SetBool("WalkRight", true);
        }
        else
        {
            animator.SetBool("WalkRight", false);
        }
        if (myDir == Vector2.left)
        {
            animator.SetBool("WalkLeft", true);
        }
        else
        {
            animator.SetBool("WalkLeft", false);
        }
        if (myDir == Vector2.up)
        {
            animator.SetBool("WalkUp", true);
        }
        else
        {
            animator.SetBool("WalkUp", false);
        }
        if (myDir == Vector2.down)
        {
            animator.SetBool("WalkDown", true);
        }
        else
        {
            animator.SetBool("WalkDown", false);
        }

    }
}
