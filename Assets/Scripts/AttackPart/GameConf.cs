using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameConf",menuName ="GameConf")]
public class GameConf : ScriptableObject
{
    [Tooltip("猫猫")]
    public GameObject ballCat;
    public GameObject longCat;
    public GameObject turkeyCat;
    public GameObject samuraiCat;

    [Tooltip("机器人")]
    public GameObject robot1;
}
