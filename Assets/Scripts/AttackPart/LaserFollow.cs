using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFollow : MonoBehaviour
{
    Vector2 targetPos;
    GameObject aimTarget;
    private bool moveDown = true;
    private bool canMove;
    private float moveWhere2;
    //追踪玩家的镭射激光
    void Start()
    {
        aimTarget = this.gameObject;
        StartCoroutine(SetNum2());
    }

    // Update is called once per frame
    void Update()
    {


        if(moveWhere2 > 3f)
        {
            moveDown = true;
        }
        else
        {
            moveDown = false;
        }
        if (!ShowCard.disapear)
        {
            gameObject.SetActive(true);
            targetPos = KingMove.instance.GetKingPos();
            canMove = true;
        }
        if(canMove)
        {
            if (aimTarget.transform.position.x >= targetPos.x)
            {
                aimTarget.transform.Translate(new Vector2(-4 * Time.deltaTime, 0f));
            }
            if (aimTarget.transform.position.y >= (targetPos.y + 0.3f) && moveDown)
            {
                aimTarget.transform.Translate(new Vector2(0f, -4 * Time.deltaTime));
            }
        }


    }

    IEnumerator SetNum2()
    {
        while(true)
        {
            Debug.Log("movewhere2=" + moveWhere2);
            moveWhere2 = Random.Range(0, 10);
            yield return new WaitForSeconds(0.3f);
        }

    }
}
