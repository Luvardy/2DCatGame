using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private Text unitCatNum;
    // Start is called before the first frame update
    void Start()
    {
        if(!instance)
        {
            instance = this;
        }


        unitCatNum = transform.Find("UnitCat/UnitCatNum").GetComponent<Text>();

    }

    public void UpdateCatNum(int num)
    {
        unitCatNum.text = num.ToString();
    }
}
