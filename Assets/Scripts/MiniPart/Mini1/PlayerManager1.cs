using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager1 : MonoBehaviour
{
    public static PlayerManager1 instance;
    public int unitNum;
    private bool isStart = false;
    private int catNum;

    public int CatNum
    {
        get => catNum;
        set
        {
            catNum = value;
            UIManager1.instance.UpdateCatNum(catNum);
        }
    }

    private void Start()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isStart)
        {
            CatNum = unitNum;
            isStart = true;
        }
    }

    public void CatNeedCost(int cost)
    {
        CatNum -= cost;
    }
}
