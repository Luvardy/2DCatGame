using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatType
{
    ballCat,
    longCat,
    turkeyCat,
    samuraiCat
}

public class CatManager : MonoBehaviour
{
    public static CatManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    public GameObject GetCatType(CatType cat)
    {
        switch (cat)
        {
            case CatType.ballCat:
                return GameManager.instance.GameConf.ballCat;
            case CatType.longCat:
                return GameManager.instance.GameConf.longCat;
            case CatType.turkeyCat:
                return GameManager.instance.GameConf.turkeyCat;
            case CatType.samuraiCat:
                return GameManager.instance.GameConf.samuraiCat;
        }
        return null;
    }
}