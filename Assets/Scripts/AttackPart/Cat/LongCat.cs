﻿using System.Collections;
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
        catCost = 2;
        State = CatState.Move;
        PlayerManager.instance.CatNeedCost(catCost);
    }

}
