using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMove : MonoBehaviour
{
    public float moveSpeed = 0.5f;

    private float borderUp = 2.49f;
    private float borderDown = -1.09f;

    public static KingMove instance;
    // Start is called before the first frame update
    void Start()
    {
        if(!instance)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (transform.position.y >= borderUp) return;
            transform.Translate(Vector2.up * 1.8f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (transform.position.y <= borderDown) return;
            transform.Translate(Vector2.down * 1.8f);
        }
        transform.Translate(Vector2.right * moveSpeed *Time.deltaTime);
    }

    public Vector2 GetKingPos()
    {
        return transform.position;
    }
}
