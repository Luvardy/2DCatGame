using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp  = 10f;

    public float HP
    {
        get => hp;
        set
        {
            hp = value;
        }
    }
    public void Hurt(float hurtValue)
    {
        hp -= hurtValue;
        if(hp < 0.01f)
        {
            Debug.Log("fuck cat!!");
            Dead();
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
    }


}
