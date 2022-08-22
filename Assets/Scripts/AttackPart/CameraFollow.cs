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

    private float viewSpeed = 10f;


    private bool changeDone = true;

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
                changeDone = false;
                myCamera.orthographicSize -= Time.deltaTime/changSpeed;
                moveSpeed += Time.deltaTime/.1f;
                transform.position = new Vector3((target.transform.position + offset).x - moveSpeed, (target.transform.position + offset).y, transform.position.z);
            }
            else
            {
                changeDone = true;
            }
            if(changeDone)
            {
                transform.position = new Vector3((target.transform.position + offset).x - moveSpeed, (target.transform.position + offset).y, transform.position.z);
            }
        }
        else
        {
            if (myCamera.orthographicSize <5)
            {
                changeDone = false;
                myCamera.orthographicSize += Time.deltaTime / changSpeed;
                moveSpeed -= Time.deltaTime / .1f;
                transform.position = new Vector3((target.transform.position + offset).x - moveSpeed, (target.transform.position + offset).y, camPos.z);
            }
            else
            {
                changeDone = true;
            }
            if (changeDone)
            {
                CameraView();
            }
        }


    }

    private void CameraView()
    {
        if(Input.mousePosition.x >= Screen.width - 1f)
        {
            if(transform.position.x < target.transform.position.x + 10f)
                transform.Translate(viewSpeed * Time.deltaTime , 0f, 0f);
        }
        if(Input.mousePosition.x <= 0 + 1f)
        {
            if(transform.position.x > target.transform.position.x + 3f)
                transform.Translate(-viewSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.mousePosition.y >= Screen.height -1f)
        {
            if (transform.position.y < target.transform.position.y + 3f)
                transform.Translate(0f, viewSpeed * Time.deltaTime, 0f);
        }
        if (Input.mousePosition.y <= 0 +1f)
        {
            if (transform.position.y > target.transform.position.y - 1f)
                transform.Translate(0f, -viewSpeed * Time.deltaTime, 0f);
        }
    }
}
