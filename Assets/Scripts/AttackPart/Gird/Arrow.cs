using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public bool isPicked = false;

    Vector2 mousePos;

    public bool isInit = false;

    public GameObject waySign;

    private bool getPathDone = false;
    RaycastHit2D info;
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        waySign = Resources.Load<GameObject>("Way");
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        InArrow();

        if(isInit && !getPathDone)
        {
            getPathDone = true;
            info = Physics2D.Linecast(transform.position,(Vector2)transform.position+new Vector2(.1f,0f),1<<16);
            if(info.collider!= null)
            {
                Debug.Log("fuck");
                DestroyWaySign(info.collider.gameObject);
                Destroy(info.collider.gameObject);
            }
            GetWaySign();
        }
    }
    // Start is called before the first frame update
    private void InArrow()
    {
        if(Vector2.Distance(mousePos,(Vector2)transform.position)<=0.3f)
        {
            isPicked = true;
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            isPicked = false;
            spriteRenderer.color = new Color(0f,0f,0f,0.5f);
        }

    }


    public void AfterClick(Vector2 arrowPos)
    {
        if(isPicked)
        {
            gameObject.transform.position = arrowPos;
            isInit = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DestroyWaySign(GameObject obj)
    {
        int i = 0;
        int j = 0;
        if (obj.tag == "Left")
        {
            i = -1;
        }
        else if (obj.tag == "Right")
        {
            i = 1;
        }
        else if (obj.tag == "Up")
        {
            j = 1;
        }
        else
        {
            j = -1;
        }

        while (true)
        {
            RaycastHit2D checkWall = Physics2D.Linecast((Vector2)transform.position + new Vector2(i, j), (Vector2)transform.position + new Vector2(i + .1f, j + .1f), 1 << 16);
            if (checkWall.collider != null)
            {
                Debug.Log("Destroy!!! i=" + i + " j=" + j);
                Destroy(checkWall.collider.gameObject);
            }
            else
            {
                break;
            }

            if (obj.tag == "Left")
            {
                i--;
            }
            else if (obj.tag == "Right")
            {
                i++;
            }
            else if (obj.tag == "Up")
            {
                j++;
            }
            else
            {
                j--;
            }
        }
    }
    public void GetWaySign( )
    {
        int i = 0;
        int j = 0;
        Quaternion rotate;
        if ( gameObject.tag == "Left")
        {
           rotate = Quaternion.Euler(0f, 0f, 180f);
            i = -1;
        }
        else if(gameObject.tag == "Right")
        {
            rotate = Quaternion.Euler(0f, 0f, 0f);
            i = 1;
        }
        else if(gameObject.tag == "Up")
        {
            rotate = Quaternion.Euler(0f, 0f, 90f);
            j = 1;
        }
        else
        {
            rotate = Quaternion.Euler(0f, 0f, -90f);
            j = -1;
        }

        while(true)
        {
            RaycastHit2D checkWall = Physics2D.Linecast((Vector2)transform.position + new Vector2(i,j), (Vector2)transform.position + new Vector2(i+.1f,j+.1f), 1<<9|1<<12);      
            if(checkWall.collider == null)
            {
                GameObject obj = Instantiate(waySign, (Vector2)transform.position + new Vector2(i, j), rotate);
                obj.tag = gameObject.tag;
            }
            else
            {
                break;
            }

            if (gameObject.tag == "Left")
            {
                i--;
            }
            else if (gameObject.tag == "Right")
            {
                i++;
            }
            else if (gameObject.tag == "Up")
            {
                j++;
            }
            else
            {
                j--;
            }
        }
    }

}
