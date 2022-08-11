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
        State = CatState.Move;
        PlayerManager.instance.CatNeedCost(catCost);
    }
}
