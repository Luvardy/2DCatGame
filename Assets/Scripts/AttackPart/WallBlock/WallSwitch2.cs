using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSwitch2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wall;
    public Sprite afterStepSprite;
    public Sprite beforeStepSprite;
    RaycastHit2D info;
    public AudioClip open;

    private bool init = false;
    private void Update()
    {
        Debug.Log(init);
        info = Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(0f, 0.1f), LayerMask.GetMask("PushableBox"));

        if (info.collider != null)
        {
            Debug.Log("OnCallPlace");
            StepOnSwitch2(true);
        }
        else
        {
            StepOnSwitch2(false);
        }
        Debug.DrawLine(transform.position, (Vector2)transform.position + new Vector2(0f, 0.1f), Color.red);
    }
    // Update is called once per frame
    public void StepOnSwitch2(bool stepped)
    {
        if (stepped)
        {
            if(!init)
            {
                init = true;
                SoundManager.instance.PlaySingelSwitch(open);
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = afterStepSprite;
            wall.gameObject.SetActive(false);
        }
        else
        {
            init = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = beforeStepSprite;
            wall.gameObject.SetActive(true);
        }
    }
}
