using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongCat : CatBase
{
    private bool canJump = false;

    private void Update()
    {
        if (canJump)
            DetectEnemy();
        transform.Translate(Vector2.right * 1f * Time.deltaTime);
    }
    protected override void OnInitForPlace()
    {
        canJump = true;
        HP = 30f;
        AttackPrice = hp;//撞击敌人自损血量
        catCost = 2;
        PlayerManager.instance.CatNeedCost(catCost);
    }

    private void DetectEnemy()
    {
        Vector2 start = transform.position;
        RaycastHit2D info = Physics2D.Linecast(start, start + new Vector2(1f, 0f));
        Debug.DrawLine(start, start + new Vector2(1f, 0f), Color.red);
        if (info.collider != null && info.collider.gameObject.tag == "Enemy")
        {
            Debug.Log("this:" + info.collider.gameObject.name);
            Destroy(gameObject);
            Destroy(info.collider.gameObject);
        }
    }
}
