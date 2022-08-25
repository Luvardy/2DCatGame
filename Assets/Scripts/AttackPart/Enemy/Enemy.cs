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
    private bool isThisCat = false;
    private bool isOnSpite;

    public AudioClip attack;
    public AudioClip dead;

    GameObject curCat;

    private bool isStarted = false;
    SpriteRenderer spriteRenderer;
    public Animator animator;

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
            DetectCat();
            MoveEnemy();
            DetectSpite();
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
        SoundManager.instance.PlaySingelEnemy(dead);
        stopDetect = true;
        animator.SetBool("Die",true);
        animator.SetBool("Attack", false);
        Destroy(gameObject.GetComponent<BoxCollider2D>());

    }

    protected virtual void MoveEnemy()
    {
    }
    public virtual void DetectCat()
    {
        Vector2 start = gameObject.transform.position;
        RaycastHit2D info = Physics2D.Linecast(start, start + new Vector2(-.3f, 0f), LayerMask.GetMask("Cat"));

        Debug.DrawLine(start, start + new Vector2(-.3f, 0f), Color.red); 
        if(isAttackState)
        {
            if(info.collider == null || info.collider.gameObject != curCat)
            {
                isThisCat = false;
            }
            return;
        }
        if (info.collider != null)
        {
            isThisCat = true;
            curCat = info.collider.gameObject;
            Debug.Log("RobotAttacking" + info.collider.gameObject.name);
            Attack(info.collider.gameObject);
        }
    }

    private void DetectSpite()
    {
        Vector2 start = transform.position;
        RaycastHit2D info = Physics2D.Linecast(start, start + new Vector2(0f, -0.5f), LayerMask.GetMask("Trap"));
        Debug.DrawLine(start, start + new Vector2(0f, -0.5f), Color.red);
        if (info.collider != null)
        {
            Debug.Log(info.collider.gameObject.name);
        }
        if (info.collider != null && info.collider.gameObject.tag == "Spite")
        {
            if (isOnSpite) return;
            StartCoroutine("StepOnSpite");
        }
        else if (info.collider != null && info.collider.gameObject.tag == "Boom")
        {
            Hurt(150);
            Destroy(info.collider.gameObject);
        }
        else
        {
            StopCoroutine("StepOnSpite");
            isOnSpite = false;
        }
    }

    IEnumerator StepOnSpite()
    {
        isOnSpite = true;
        while (hp > 0)
        {
            Hurt(10);
            yield return new WaitForSeconds(0.5f);
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
        while(thisCat.hp > 0 && !stopDetect && isThisCat)
        {
            SoundManager.instance.PlaySingelEnemy(attack);
            animator.SetBool("Attack", true);
            thisCat.Hurt(attackValue);
            yield return new WaitForSeconds(1.5f);
        }
        animator.SetBool("Attack", false);
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
