using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager1 : MonoBehaviour
{
    public static UIManager1 instance;
    private Text unitCatNum;
    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }


        unitCatNum = transform.Find("UnitCat").GetComponent<Text>();

    }

    public void UpdateCatNum(int num)
    {
        unitCatNum.text = num.ToString();
    }
}
