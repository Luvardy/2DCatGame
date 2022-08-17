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
        HP = 100f;
        AttackPrice = hp;
        attackValue = 0f;
        catCost = 1;
        catSpeed = 3f;
        State = CatState.Move;
        PlayerManager.instance.CatNeedCost(catCost);

    }


}
