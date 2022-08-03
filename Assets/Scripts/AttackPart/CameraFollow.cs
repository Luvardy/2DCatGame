using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
     private float offset;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position.x - target.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x + offset,transform.position.y,transform.position.z) ;
    }
}
