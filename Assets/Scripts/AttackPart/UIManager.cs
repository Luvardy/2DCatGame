using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private Text unitCatNum;
    private Image hpFill;
    private Image buttonImg;

    public Sprite inEdit;
    public Sprite OutEdit;

    // Start is called before the first frame update
    void Start()
    {
        if(!instance)
        {
            instance = this;
        }


        unitCatNum = transform.Find("UnitCat/UnitCatNum").GetComponent<Text>();
        hpFill = transform.Find("HP").GetComponent<Image>();
        buttonImg = transform.Find("Card/Button").GetComponent<Image>();

    }

    public void UpdateCatNum(int num)
    {
        unitCatNum.text = num.ToString();
    }

    public void HpChange(float valuePercent)
    {
        if(hpFill != null)
        {
            Debug.Log(hpFill.name);

            hpFill.fillAmount = valuePercent;

        }
    }

    public void ChangeButtonColor()
    {
        if(buttonImg.sprite == inEdit)
        {
            buttonImg.sprite = OutEdit;
        }
        else
        {
            buttonImg.sprite = inEdit;
        }
    }
}
