using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public int unitNum;
    private bool isStart = false;
    private int catNum;

    public int CatNum
    {
        get => catNum;
        set
        {
            catNum = value;
            UIManager.instance.UpdateCatNum(catNum);
        }
    }

    private void Start()
    {
        if(!instance)
        {
            instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!isStart)
        {
            CatNum = unitNum;
            isStart = true;
        }
    }

    public void CatNeedCost(int cost)
    {
        CatNum -= cost;
    }

    public void CatPayBack(int back)
    {
        CatNum += back;
        Debug.Log("返还" + back);
    }
}
