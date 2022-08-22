using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSwitch : MonoBehaviour
{
    public GameObject wall;
    public Sprite afterStepSprite;
    public Sprite beforeStepSprite;

    public AudioClip open;

    private bool init = false;

    public void SwitchOn()
    {
        if (!init)
        {
            init = true;
            SoundManager.instance.PlaySingelSwitch(open);
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = afterStepSprite;
        Destroy(wall);
    }

}
