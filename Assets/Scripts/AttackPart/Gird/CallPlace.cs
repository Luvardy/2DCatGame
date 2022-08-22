using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPlace : MonoBehaviour
{
    public Sprite afterStepSprite;
    public Sprite beforeStepSprite;

    RaycastHit2D info;

    private bool init = false;

    public AudioClip calling;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        info = Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(0f, 0.1f), LayerMask.GetMask("King"));

        if (info.collider != null)
        {
            Debug.Log("OnCallPlace");
            StepOnCallPlace(true);
        }
        else
        {
            StepOnCallPlace(false);
        }
        Debug.DrawLine(transform.position, (Vector2)transform.position + new Vector2(0f, 0.1f),Color.red);
    }
    // Update is called once per frame
    public void StepOnCallPlace(bool stepped)
    {
        if (stepped)
        {
            if(!init)
            {
                init = true;
                SoundManager.instance.PlaySingle(calling);
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = afterStepSprite;
        }
        else
        {
            init = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = beforeStepSprite;
        }
    }
}
