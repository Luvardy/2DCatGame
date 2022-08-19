using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCard : MonoBehaviour
{
    public static bool disapear = true;

    void Update()
    {

        Debug.Log(KingMove.canCallCat);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(KingMove.canCallCat)
            {
                if(disapear == true)
                {
                    disapear = false;
                }
                else
                {
                    disapear = true;
                }
            }

        }

        if (!disapear)
        { 
            if(transform.position.y > 980f)
            {
                transform.Translate(Vector2.down * 400 * Time.deltaTime); 
            }
        }
        else
        {
            
            if (transform.position.y < 1230f)
            {
                transform.Translate(Vector2.up * 400 * Time.deltaTime);
            }
        }
    }



}
