using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurkeyCat : CatBase
{
    private void Update()
    {
        FSM();
    }
    protected override void OnInitForPlace()
    {
        HP = 100f;
        AttackPrice = hp;//撞击敌人自损血量
        catCost = 4;
        catSpeed = 1f;
        State = CatState.Move;
        PlayerManager.instance.CatNeedCost(catCost);
    }
}
