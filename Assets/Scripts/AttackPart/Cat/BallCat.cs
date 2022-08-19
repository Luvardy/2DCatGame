using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCat : CatBase
{





    private void Update()
    {
        FSM();
    }
    protected override void OnInitForPlace()
    {
        Debug.Log("?");
        HP = 40f;
        AttackPrice = hp;
        attackValue = 20f;
        catCost = 1;
        catSpeed = 3f;
        State = CatState.Move;
        PlayerManager.instance.CatNeedCost(catCost);

    }

    protected override IEnumerator DoHurt(Enemy thisEnemy)
    {

        while (thisEnemy.hp > 0)
        {
            thisEnemy.Hurt(attackValue);
            Hurt(attackPrice);
            yield return new WaitForSeconds(0.2f);
        }
        isAttackState = false;
        State = CatState.Move;
    }


}
