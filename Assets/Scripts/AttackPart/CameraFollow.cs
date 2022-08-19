using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera myCamera;
    private float camSize;
    private Vector3 camPos;

    private Vector3 offset;

    public float changSpeed = 0.1f;
    public float moveSpeed = 0f;

    private bool changeDone = false;

    public GameObject target;
    // Start is called before the first frame update
       void Start()
    {
        offset = transform.position - target.transform.position;
        myCamera = gameObject.GetComponent<Camera>();
        camSize = myCamera.orthographicSize;
        camPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {

        if (ShowCard.disapear)
        {
            if (myCamera.orthographicSize > 3.5f)
            {
                myCamera.orthographicSize -= Time.deltaTime/changSpeed;
                moveSpeed += Time.deltaTime/0.5f;
                transform.position = new Vector3((target.transform.position + offset).x - moveSpeed, (target.transform.position + offset).y, transform.position.z);
            }
            
        }
        else
        {
            if (myCamera.orthographicSize <7)
            {
                myCamera.orthographicSize += Time.deltaTime / changSpeed;
                moveSpeed -= Time.deltaTime / 0.5f;
                transform.position = new Vector3((target.transform.position + offset).x - moveSpeed, (target.transform.position + offset).y, camPos.z);
            }
            
        }


    }
}
