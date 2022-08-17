using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float hp  = 10f;
    public float attackValue = 20f;
    private bool isAttackState = false;
    private bool stopDetect = false;

    private bool isStarted = false;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Find()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
        if (!isStarted)
        {
            Find();
            isStarted = true;
        }
        if(!stopDetect)
        {
            detectCat();
        }

    }
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
        StartCoroutine(ColorEF(0.2f, new Color(0.5f, 0.5f, 0.5f), 0.05f, null));
    }

    private void Dead()
    {
   
        animator.Play("EnemyDie");
        stopDetect = true;
        gameObject.layer = 1 << 2;

    }

    public void detectCat()
    {
        Vector2 start = gameObject.transform.position;
        RaycastHit2D info = Physics2D.Linecast(start, start + new Vector2(-1f, 0f), LayerMask.GetMask("Cat"));
        Debug.DrawLine(start, start + new Vector2(-1f, 0f), Color.red);
        if(isAttackState)
        {
            return;
        }
        if (info.collider != null)
        {
            Debug.Log("RobotAttacking" + info.collider.gameObject.name);
            Attack(info.collider.gameObject);
        }
    }

    private void Attack(GameObject cat)
    {
        isAttackState = true;
        CatBase thisCat = cat.gameObject.GetComponent<CatBase>();
        StartCoroutine(DoHurt(thisCat));
    }
    IEnumerator DoHurt(CatBase thisCat)
    {
        while(thisCat.hp > 0)
        {
            animator.Play("EnemyAttack");
            thisCat.Hurt(attackValue);
            yield return new WaitForSeconds(0.7f);
        }
        animator.Play("EnemyIdel");
        isAttackState = false;
    }

    protected IEnumerator ColorEF(float wantTime, Color targetColor, float delayTime, UnityAction fun)
    {
        float currTime = 0;
        float lerp;
        while (currTime < wantTime)
        {
            yield return new WaitForSeconds(delayTime);
            lerp = currTime / wantTime;
            currTime += delayTime;
            // 实现一个从白到红的插值计算，lerp为0就是白色(原色)，如果为1就是Color(1,0.6f,0)
            spriteRenderer.color = Color.Lerp(Color.white, targetColor, lerp);
        }
        // 恢复原来的附加色(白色)
        spriteRenderer.color = Color.white;
        if (fun != null) fun();
    }

}
