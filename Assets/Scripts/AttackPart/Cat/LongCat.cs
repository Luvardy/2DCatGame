using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongCat : CatBase
{
    private bool isJumping = false;

    private Vector2 myLastDir = Vector2.right;
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
        RaycastHit2D infoWay;
        RaycastHit2D infoWayLeft = Physics2D.Linecast(start + new Vector2(.3f,0f), start + new Vector2(.4f, 0f),1<<12);
        RaycastHit2D infoWayRight = Physics2D.Linecast(start + new Vector2(-.3f,0f), start + new Vector2(-.4f, 0f),1<<12);
        RaycastHit2D infoWayUp = Physics2D.Linecast(start + new Vector2(0f,-.3f), start + new Vector2(0f, -.4f),1<<12);
        RaycastHit2D infoWayDown = Physics2D.Linecast(start + new Vector2(0f,.3f), start + new Vector2(0f, .4f),1<<12);

        RaycastHit2D infoNextRight = Physics2D.Linecast(start + new Vector2(1.5f, 0f), start + new Vector2(1.6f, 0f), 1 << 9 | 1 << 8);
        RaycastHit2D infoNextLeft = Physics2D.Linecast(start + new Vector2(-1.5f, 0f), start + new Vector2(-1.6f, 0f), 1 << 9 | 1 << 8);
        RaycastHit2D infoNextUp = Physics2D.Linecast(start + new Vector2(0f, 1.5f), start + new Vector2(0f, 1.6f), 1 << 9 | 1 << 8);
        RaycastHit2D infoNextDown = Physics2D.Linecast(start + new Vector2(0f, -1.5f), start + new Vector2(0f, -1.6f), 1 << 9 | 1 << 8);

        RaycastHit2D infoWallRight = Physics2D.Linecast(start, start + new Vector2(0.3f, 0f),1<<9 |1 << 8);
        RaycastHit2D infoWallLeft = Physics2D.Linecast(start, start + new Vector2(-0.3f, 0f),1<<9 |1 << 8);
        RaycastHit2D infoWallUp = Physics2D.Linecast(start, start + new Vector2(0f, 0.3f),1<<9 | 1 << 8);
        RaycastHit2D infoWallDown = Physics2D.Linecast(start, start + new Vector2(0f, -0.3f),1<<9 | 1 << 8);

        //Debug.DrawLine(start + new Vector2(.45f, 0f), start + new Vector2(.5f, 0f), Color.red);
        //Debug.DrawLine(start + new Vector2(-.45f, 0f), start + new Vector2(-.5f, 0f), Color.red);
        //Debug.DrawLine(start + new Vector2(0f, .45f), start + new Vector2(0f, .5f), Color.red);
        //Debug.DrawLine(start + new Vector2(0f, -.45f), start + new Vector2(0f, -.5f), Color.red);

        Debug.DrawLine(start, start + new Vector2(.3f, 0f), Color.blue);
        Debug.DrawLine(start, start + new Vector2(-0.4f, 0f), Color.blue);
        Debug.DrawLine(start, start + new Vector2(0f, 0.4f), Color.blue);
        Debug.DrawLine(start, start + new Vector2(0f, -0.4f), Color.blue);

        Debug.DrawLine(start + new Vector2(1.5f, 0f), start + new Vector2(1.6f, 0f),Color.red);
        Debug.DrawLine(start + new Vector2(-1.5f, 0f), start + new Vector2(-1.6f, 0f), Color.red);
        Debug.DrawLine(start + new Vector2(0f, 1.5f), start + new Vector2(0f, 1.6f), Color.red);
        Debug.DrawLine(start + new Vector2(0f, -1.5f), start + new Vector2(0f, -1.6f), Color.red);
        if (myDir == Vector2.left)
        {
            infoWay = infoWayLeft;
        }
        else if(myDir == Vector2.right)
        {
            infoWay = infoWayRight;
        }
        else if(myDir == Vector2.up)
        {
            infoWay = infoWayUp;
        }
        else
        {
            infoWay = infoWayDown;
        }

        if (infoWay.collider != null)
        {
            switch (infoWay.collider.gameObject.tag)
            {
                case "Left":
                    myDir = Vector2.left;
                    myLastDir = myDir;
                    break;
                case "Right":
                    myDir = Vector2.right;
                    myLastDir = myDir;
                    break;
                case "Up":
                    myDir = Vector2.up;
                    myLastDir = myDir;
                    break;
                case "Down":
                    myDir = Vector2.down;
                    myLastDir = myDir;
                    break;
            }
            if (infoWallRight.collider != null && infoNextRight.collider != null || infoWallLeft.collider != null && infoNextLeft.collider != null
                || infoWallUp.collider != null && infoNextUp.collider != null || infoWallDown.collider != null && infoNextDown.collider != null)
            {
                Vector2 pos = KingMove.instance.GetKingPos();
                myDir = Vector2.zero;
                returnCat(pos);
            }
            else if (infoWallRight.collider != null && infoNextRight.collider == null || infoWallLeft.collider != null && infoNextLeft.collider == null
                    || infoWallUp.collider != null && infoNextUp.collider == null || infoWallDown.collider != null && infoNextDown.collider == null)
            {
                Jump();
            }
            transform.Translate(myDir * catSpeed * Time.deltaTime);
        }
        else if(infoWallRight.collider != null && infoNextRight.collider != null || infoWallLeft.collider != null && infoNextLeft.collider != null
                ||infoWallUp.collider != null && infoNextUp.collider != null || infoWallDown.collider != null && infoNextDown.collider != null)
        {
            Vector2 pos = KingMove.instance.GetKingPos();
            myDir = Vector2.zero;
            returnCat(pos);
        }
        else if(infoWallRight.collider != null && infoNextRight.collider == null || infoWallLeft.collider != null && infoNextLeft.collider == null
                || infoWallUp.collider != null && infoNextUp.collider == null || infoWallDown.collider != null && infoNextDown.collider == null)
        {
            Jump();
        }
        else
        {
            transform.Translate(myLastDir * catSpeed * Time.deltaTime);
        }

        if(myDir == Vector2.right)
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

    void Jump()
    {
        Debug.Log("我在跳！");
    }


}
