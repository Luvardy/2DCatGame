using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catFollow : MonoBehaviour
{
    Vector2 mousePos;
    public GameObject ballCat;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);

        if(Input.GetMouseButtonDown(0))
        {
            CatBase cat = Instantiate(ballCat, transform.position, Quaternion.identity).GetComponent<CatBase>();
            cat.InitForCreate(false);
            cat.InitForPlace();
        }
    }
}
