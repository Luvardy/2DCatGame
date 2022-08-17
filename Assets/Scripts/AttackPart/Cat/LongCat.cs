using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongCat : CatBase
{
    private void Update()
    {
        FSM();
    }
    protected override void OnInitForPlace()
    {
        HP = 30f;
        AttackPrice = hp;//撞击敌人自损血量
        attackValue = 100f;
        catCost = 2;
        catSpeed = 2f;
        State = CatState.Move;
        PlayerManager.instance.CatNeedCost(catCost);
    }

    protected override void MoveCat()
    {
        Vector2 start = (Vector2)transform.position;
        RaycastHit2D infoFront = Physics2D.Linecast(start, start + new Vector2(0.2f, 0f), ~(1 << 0));
        Debug.DrawLine(start, start + new Vector2(0.1f, 0f), Color.red);

        if (infoFront.collider != null)
        {
            switch (infoFront.collider.gameObject.tag)
            {
                case "Left":
                    myDir = Vector2.left;
                    break;
                case "Right":
                    myDir = Vector2.right;
                    break;
                case "Up":
                    myDir = Vector2.up;
                    break;
                case "Down":
                    myDir = Vector2.down;
                    break;
                default:
                    checkCanJump();
                    break;
            }
            transform.Translate(myDir * catSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(myDir * catSpeed * Time.deltaTime);
        }
    }

    private void checkCanJump()
    {
        Vector2 start = transform.position;
        RaycastHit2D infoNext = Physics2D.Linecast(start + new Vector2(1.5f,0f), start + new Vector2(1.6f, 0f), LayerMask.GetMask("Wall"));
        Debug.DrawLine(start + new Vector2(1.5f, 0f), start + new Vector2(1.6f, 0f), Color.red);
        if (infoNext.collider != null)
        {
            Debug.Log(infoNext.collider.gameObject.name);
            myDir = Vector2.zero;
        }
        else
        {
            myDir = Vector2.right;
        }
    }

}
