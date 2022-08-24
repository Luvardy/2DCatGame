using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUp : MonoBehaviour
{
    RaycastHit2D info;
    public GameObject Light;

    public float width;
    public float height;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        info = Physics2D.Linecast((Vector2)transform.position + new Vector2(-width, -height), (Vector2)transform.position + new Vector2(width, height), LayerMask.GetMask("King"));

        if (info.collider != null)
        {
            if(Light != null)
            {
                Debug.Log("LightUP");
                Light.gameObject.SetActive(true);
            }

        }
        Debug.DrawLine((Vector2)transform.position + new Vector2(-width, -height), (Vector2)transform.position + new Vector2(width, height), Color.red);
    }
}
