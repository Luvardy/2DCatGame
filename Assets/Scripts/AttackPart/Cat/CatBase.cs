using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CatState
{
    Idel,
    Move,
    Attack,
    Dead
}
public class CatBase : MonoBehaviour
{

    public CatState state;//实例化猫的状态
    // 只能让子类调用
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    //检测猫状态
    public bool isAttackState;
    private bool isOnSpite;

    //猫的基本属性
    public int catCost;
    public float hp;
    public float attackPrice;
    public float attackValue;


    public GameObject currCat;

    public CatState State
    {
        get => state;
        set
        {
            state = value;
            switch(state)
            {
                case CatState.Idel:
                    Debug.Log("??");
                    //animator.Play();
                    //animator.speed = 0;
                    break;
                case CatState.Move:
                    //animator.Play();
                    break;
                case CatState.Attack:
                    //animator.Play();
                    break;
                case CatState.Dead:
                    //animator.Play();
                    break;
            }
        }
    }


    public float HP
    {
        get => hp;
        set
        {
            hp = value;
            if(hp < 0.01f)
            {
                State = CatState.Dead;
            }
        }
    }

    public float AttackPrice
    {
        get => attackPrice;
        set
        {
            attackPrice = value;
        }
    }

    public void FSM()
    {
        switch (state)
        {
            case CatState.Idel:
                Debug.Log("???");
                break;
            case CatState.Move:
                MoveCat();
                DetectEnemy();
                DetectSpite();
                break;
            case CatState.Attack:
                if (isAttackState) break;
                Attack(currCat);
                break;
            case CatState.Dead:
                Dead();
                break;
        }
    }

    private void MoveCat()
    {
        Vector2 start = transform.position;
        RaycastHit2D infoFront = Physics2D.Linecast(start, start + new Vector2(0.5f, 0f), ~(1 << 0));
        RaycastHit2D infoUp = Physics2D.Linecast(start, start + new Vector2(0f, 0.5f), ~(1 << 0));
        RaycastHit2D infoDown = Physics2D.Linecast(start, start + new Vector2(0f, -0.5f), ~(1 << 0));

        if(infoFront.collider != null)
        {
            if(infoDown.collider == null)
            {
                transform.Translate(Vector2.down * 1f * Time.deltaTime);
            }
            else if(infoUp.collider == null)
            {
                transform.Translate(Vector2.up * 1f * Time.deltaTime);
            }
            return;
        }
        else
        {
            transform.Translate(Vector2.right * 1f * Time.deltaTime);
        }
    }

    private void DetectEnemy()
    {
        Vector2 start = transform.position;
        RaycastHit2D info = Physics2D.Linecast(start, start + new Vector2(0.5f, 0f), ~(1 << 0));
        Debug.DrawLine(start, start + new Vector2(1f, 0f), Color.red);
        if (isAttackState) return;

        if (info.collider != null && info.collider.gameObject.tag == "Enemy")
        {
            currCat = info.collider.gameObject;
            State = CatState.Attack;
        }
        else if (info.collider != null && info.collider.gameObject.tag == "Wall")
        {
            return;
        }
        else if (info.collider != null && info.collider.gameObject.tag == "Switch")
        {
            WallSwitch _switch = info.collider.gameObject.GetComponent<WallSwitch>();
            _switch.SwitchOn();
            //transform.Translate(Vector2.right * 1f * Time.deltaTime);

        }
        //else
        //{
        //    transform.Translate(Vector2.right * 1f * Time.deltaTime);
        //}
    }

    private void DetectSpite()
    {
        Vector2 start = transform.position;
        RaycastHit2D info = Physics2D.Linecast(start, start + new Vector2(0f, -0.5f));
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

    private void Attack(GameObject enemy)
    {
        isAttackState = true;
        Enemy thisEnemy = enemy.GetComponent<Enemy>();
        StartCoroutine(DoHurt(thisEnemy));
    }

    IEnumerator DoHurt(Enemy thisEnemy)
    {

        while (thisEnemy.hp > 0)
        {
            thisEnemy.Hurt(attackValue);
            Hurt(attackPrice);
            yield return new WaitForSeconds(0.2f);
        }
        isAttackState = false;
        State = CatState.Move;
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

    public void Hurt(float hurtValue)
    {
        HP -= hurtValue;
        StartCoroutine(ColorEF(0.2f, new Color(0.5f, 0.5f, 0.5f), 0.05f, null));
        Debug.Log("i am hurt miao" + hp);
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
    // 查找自身相关组件
    protected void Find()
    {
        //animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // 创建时的初始化
    public void InitForCreate(bool inGrid)
    {
        // 获取组件
        Find();
        // 拖拽时不播放动画
        //animator.speed = 0;
        if (inGrid)
        {
            spriteRenderer.sortingOrder = 0;
            spriteRenderer.color = new Color(1, 1, 1, 0.6f);
        }
    }

    public void InitForPlace()
    {
        // 恢复动画
        //animator.speed = 1;
        spriteRenderer.sortingOrder = 3;
        OnInitForPlace();
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

    // 创建一个虚基类
    protected virtual void OnInitForPlace() { }
}