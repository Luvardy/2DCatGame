using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public bool isPicked = false;

    Vector2 mousePos;
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        InArrow();
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
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
