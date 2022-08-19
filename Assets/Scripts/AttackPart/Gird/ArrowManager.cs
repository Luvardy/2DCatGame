using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public static bool isEditing = false;
    private bool isSelectMode = false;
    private bool clickToShowArrow = true;
    private bool firstTime = true;
    //gameconf need
    public GameObject ArrowLeft;
    public GameObject ArrowRight;
    public GameObject ArrowUp;
    public GameObject ArrowDown;
    public GameObject canEdit;
    public GameObject cantEdit;
    public GameObject canPlace;

    private GameObject left;
    private GameObject right;
    private GameObject up;
    private GameObject down;

    private float time = 0;
    private float delayTime = 0.1f;

    private Vector2 pickPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isEditing);
        Debug.Log("isSelect:" + isSelectMode);
        if(Input.GetKeyDown(KeyCode.K))
        {
            changeEditMode();
        }

        if(isEditing && !ShowCard.disapear)
        {
            Vector2 mousePos = GridManager.instance.GetGridPointByMouse();

            RaycastHit2D checkArrow = Physics2D.Linecast(mousePos, mousePos + new Vector2(0.1f, 0f), 1 << 12);



            if (clickToShowArrow)
            {
                if (GridManager.instance.CheckCanPlace())
                {
                    if (cantEdit != null)
                    {
                        cantEdit.gameObject.SetActive(false);
                    }
                    canEdit.gameObject.SetActive(true);
                    canEdit.gameObject.transform.position = mousePos;
                }
                else
                {
                    if (canEdit != null)
                    {
                        canEdit.gameObject.SetActive(false);
                    }
                    cantEdit.gameObject.SetActive(true);
                    cantEdit.gameObject.transform.position = mousePos;
                }
            }




            if (Input.GetMouseButtonDown(0) && !isSelectMode && GridManager.instance.CheckCanPlace() && clickToShowArrow)
            {

                if (checkArrow.collider == null)
                {
                    isSelectMode = true;

                    pickPos = mousePos;

                    clickToShowArrow = false;

                    left = Instantiate(ArrowLeft, mousePos + new Vector2(-.5f, 0f), Quaternion.Euler(0f, 0f, 180f));
                    right = Instantiate(ArrowRight, mousePos + new Vector2(.5f, 0f), Quaternion.identity);
                    up = Instantiate(ArrowUp, mousePos + new Vector2(0f, .5f), Quaternion.Euler(0f, 0f, 90f));
                    down = Instantiate(ArrowDown, mousePos + new Vector2(0f, -.5f), Quaternion.Euler(0f, 0f, 270f));
                }


            }

            if(Input.GetMouseButtonDown(1) && !isSelectMode)
            {
                
                if(checkArrow.collider != null)
                {
                    Destroy(checkArrow.collider.gameObject);
                }
            }
        }
        else
        {

            canEdit.gameObject.SetActive(false);
            cantEdit.gameObject.SetActive(false);
            if(!clickToShowArrow)
            {
                Destroy(left.gameObject);
                Destroy(right.gameObject);
                Destroy(up.gameObject);
                Destroy(down.gameObject);
            }
        }

        if(!ShowCard.disapear)
        {
            Debug.Log("FirstStep");
            if(isEditing)
            {
                canPlace.gameObject.SetActive(false);
                Debug.Log("SecondStep");
                GridManager.instance.DestoryNearKing();
                firstTime = true;
            }
            else
            {
                if(firstTime)
                {
                    canPlace.gameObject.SetActive(true);
                    Debug.Log("ShowCanPlace");
                    GridManager.instance.ShowNearKing(canPlace);
                    firstTime = false;
                }
            }
        }
        else
        { 
            canPlace.gameObject.SetActive(false);
            Debug.Log("SecondStep");
            GridManager.instance.DestoryNearKing();
            firstTime = true;
        }

        if (isSelectMode)
        {
            Debug.Log("I am running" + clickToShowArrow);

            if(time <= delayTime)
            {
                time += Time.deltaTime;
            }
            else
            {
                if(Input.GetMouseButtonDown(0)|| Input.GetMouseButtonDown(1))
                {
                    Debug.Log("chao");
                    left.gameObject.GetComponent<Arrow>().AfterClick(pickPos);
                    right.gameObject.GetComponent<Arrow>().AfterClick(pickPos);
                    up.gameObject.GetComponent<Arrow>().AfterClick(pickPos); 
                    down.gameObject.GetComponent<Arrow>().AfterClick(pickPos);

                    isSelectMode = false;
                    clickToShowArrow = true;
                    time = 0f;
                }
            }


        }
    }

    public void changeEditMode()
    {
        UIManager.instance.ChangeButtonColor();
        if (isEditing)
        {
            isEditing = false;
        }
        else
        {
            isEditing = true;
        }
        isSelectMode = false;
        if(!clickToShowArrow)
        {
            Destroy(left.gameObject);
            Destroy(right.gameObject);
            Destroy(up.gameObject);
            Destroy(down.gameObject);
            clickToShowArrow = true;
        }
    }
}
