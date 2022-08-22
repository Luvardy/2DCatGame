using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    private bool onEdgeUp = false;
    private bool onEdgeDown = false;
    private bool onEdgeLeft = false;
    private bool onEdgeRight = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkWall();
    }

    private void checkWall()
    {
        Vector2 start = (Vector2)transform.position + new Vector2(0f,-0.12f); 

        RaycastHit2D checkUpLeft = Physics2D.Linecast(start + new Vector2(-.42f, 0f), start + new Vector2(-.42f, 0.5f), LayerMask.GetMask("Wall"));
        RaycastHit2D checkUpRight = Physics2D.Linecast(start + new Vector2(.42f, 0f), start + new Vector2(.42f, 0.5f), LayerMask.GetMask("Wall"));

        RaycastHit2D checkDownLeft = Physics2D.Linecast(start + new Vector2(-0.42f, 0f), start + new Vector2(-0.42f, -0.5f), LayerMask.GetMask("Wall"));
        RaycastHit2D checkDownRight = Physics2D.Linecast(start + new Vector2(0.42f, 0f), start + new Vector2(0.42f, -0.5f), LayerMask.GetMask("Wall"));

        RaycastHit2D checkRightDown = Physics2D.Linecast(start + new Vector2(0f, -0.42f), start + new Vector2(0.5f, -0.42f), LayerMask.GetMask("Wall"));
        RaycastHit2D checkRightUp = Physics2D.Linecast(start + new Vector2(0f, 0.42f), start + new Vector2(0.5f, 0.42f), LayerMask.GetMask("Wall"));

        RaycastHit2D checkLeftDown = Physics2D.Linecast(start + new Vector2(0, -0.42f), start + new Vector2(-0.5f, -0.42f), LayerMask.GetMask("Wall"));
        RaycastHit2D checkLeftUp = Physics2D.Linecast(start + new Vector2(0, 0.42f), start + new Vector2(-0.5f, 0.42f), LayerMask.GetMask("Wall"));

        Debug.DrawLine(start + new Vector2(-0.42f, 0f), start + new Vector2(-0.42f, 0.5f), Color.red);
        Debug.DrawLine(start + new Vector2(0.42f, 0f), start + new Vector2(0.42f, 0.5f), Color.red);

        Debug.DrawLine(start + new Vector2(-0.42f, 0f), start + new Vector2(-0.42f, -0.5f), Color.red);
        Debug.DrawLine(start + new Vector2(0.42f, 0f), start + new Vector2(0.42f, -0.5f), Color.red);

        Debug.DrawLine(start + new Vector2(0f, -0.45f), start + new Vector2(0.5f, -0.45f), Color.red);
        Debug.DrawLine(start + new Vector2(0f, 0.45f), start + new Vector2(0.5f, 0.45f), Color.red);

        Debug.DrawLine(start + new Vector2(0, -0.45f), start + new Vector2(-0.5f, -0.45f), Color.red);
        Debug.DrawLine(start + new Vector2(0, 0.45f), start + new Vector2(-0.5f, 0.45f), Color.red);
        Debug.Log("Left:" + onEdgeLeft + " Right:" + onEdgeRight + " Up:" + onEdgeUp + " Down" + onEdgeDown);
        if (checkUpLeft.collider != null || checkUpRight.collider != null)
        {
            onEdgeUp = true;
            Debug.Log(checkUpRight.collider.gameObject);

        }
        else
        {
            onEdgeUp = false;
        }

        if (checkDownLeft.collider != null || checkDownRight.collider != null)
        {
            onEdgeDown = true;
            Debug.Log(checkDownRight.collider.gameObject);
        }
        else
        {
            onEdgeDown = false;
        }

        if(checkLeftDown.collider != null || checkLeftUp.collider != null)
        {
            onEdgeLeft = true;
        }
        else
        {
            onEdgeLeft = false;
        }

        if(checkRightDown.collider != null || checkRightUp.collider != null)
        {
            onEdgeRight = true;
        }
        else
        {
            onEdgeRight = false;
        }


    }
    public void isPushed(Vector2 pushDir, float moveSpeed)
    {
        if(pushDir == Vector2.left && !onEdgeLeft)
            transform.Translate(pushDir * moveSpeed * Time.fixedDeltaTime);

        if(pushDir == Vector2.right && !onEdgeRight)
            transform.Translate(pushDir * moveSpeed * Time.fixedDeltaTime);

        if(pushDir ==Vector2.down && !onEdgeDown)
            transform.Translate(pushDir * moveSpeed * Time.fixedDeltaTime);
        
        if(pushDir == Vector2.up && !onEdgeUp)
            transform.Translate(pushDir * moveSpeed * Time.fixedDeltaTime);
    }
}
