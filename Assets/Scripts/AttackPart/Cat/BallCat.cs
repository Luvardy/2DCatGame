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
        attackValue = 100f;
        catCost = 1;
        State = CatState.Move;
        PlayerManager.instance.CatNeedCost(catCost);
    }


}
