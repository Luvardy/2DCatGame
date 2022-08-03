using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSwitch : MonoBehaviour
{
    public GameObject wall;

    public void SwitchOn()
    {
        Destroy(wall);
    }
}
