using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameConf GameConf
    {
        get; private set;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameConf = Resources.Load<GameConf>("GameConf");

        if (!instance)
        {
            instance = this;
        }
    }

}
