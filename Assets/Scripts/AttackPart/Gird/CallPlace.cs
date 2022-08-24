using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPlace : MonoBehaviour
{
    public Sprite afterStepSprite;
    public Sprite beforeStepSprite;

    RaycastHit2D info;

    private bool init = false;

    private bool isThisPlace = false;
    public AudioClip calling;
    // Start is called before the first frame update

    void Update()
    {
        info = Physics2D.Linecast((Vector2)transform.position + new Vector2(0f, 0.2f), (Vector2)transform.position + new Vector2(0f, 0.3f), LayerMask.GetMask("King"));

        if (info.collider != null)
        {
            Debug.Log("OnCallPlace");
            isThisPlace = true;
            StepOnCallPlace(true);
        }
        else
        {
            StepOnCallPlace(false);
        }
        Debug.DrawLine((Vector2)transform.position + new Vector2(0f, 0.2f), (Vector2)transform.position + new Vector2(0f, 0.3f), Color.red);
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
            KingMove.canCallCat = true;
            
            gameObject.GetComponent<SpriteRenderer>().sprite = afterStepSprite;
        }
        else
        {
            init = false;
            if (isThisPlace)
            {
                KingMove.canCallCat = false;
                isThisPlace = false;
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = beforeStepSprite;
        }
    }
}
