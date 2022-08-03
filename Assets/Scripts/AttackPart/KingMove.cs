using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KingMove : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float hp = 200f;
    public float maxHp;
    private float attackValue;

    private float borderUp = 2.49f;
    private float borderDown = -1.09f;

    public static KingMove instance;

    private bool isOnSpite = false;
    private bool isAttackState = false;

    protected SpriteRenderer spriteRenderer;
    void Start()
    {
        maxHp = hp;

        attackValue = hp;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if(!instance)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetectSpite();
        DetectEnemy();
        CheckCanMove();

        
    }

    public Vector2 GetKingPos()
    {
        return transform.position;
    }

    //检测王前面有无敌人 是否可以继续前进
    private void CheckCanMove()
    {
        Vector2 start = (Vector2)transform.position + new Vector2(-0.5f, 0f);

        RaycastHit2D checkUp = Physics2D.Linecast(start, start + new Vector2(0f, 2f), LayerMask.GetMask("Wall"));
        RaycastHit2D checkDown = Physics2D.Linecast(start, start + new Vector2(0f, -2f), LayerMask.GetMask("Wall"));

        Debug.DrawLine(start, start + new Vector2(4f, 0f),Color.red);
        Debug.DrawLine(start, start + new Vector2(0f, 2f), Color.red);
        Debug.DrawLine(start, start + new Vector2(0f, -2f), Color.red);

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (transform.position.y >= borderUp || checkUp.collider != null)
            {
                return;
            }
            transform.Translate(Vector2.up * 1.8f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (transform.position.y <= borderDown || checkDown.collider != null)
            {
                return;
            }
            transform.Translate(Vector2.down * 1.8f);
        }


    }

    private void DetectEnemy()
    {
        Vector2 start = (Vector2)transform.position + new Vector2(0f,-0.5f);
        RaycastHit2D info = Physics2D.Linecast(start, start + new Vector2(1f, 0f));
        Debug.DrawLine(start, start + new Vector2(1f, 0f), Color.red);
        if (isAttackState) return;
        if (info.collider != null && info.collider.gameObject.tag == "Enemy")
        {
            Attack(info.collider.gameObject);
        }
        else if (info.collider.gameObject.tag == "Wall")
        {
            return;
        }
        else if (info.collider.gameObject.tag == "Switch")
        {
            Debug.Log("open");
            WallSwitch _switch = info.collider.gameObject.GetComponent<WallSwitch>();
            _switch.SwitchOn();
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
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
            Hurt(thisEnemy.hp);
            thisEnemy.Hurt(attackValue);
            yield return new WaitForSeconds(0.2f);
        }
        isAttackState = false;
    }
    private void DetectSpite()
    {
        Vector2 start = transform.position;
        RaycastHit2D info = Physics2D.Linecast(start, start + new Vector2(0f, -0.5f));
        Debug.DrawLine(start, start + new Vector2(0f, -0.5f), Color.red);
        if (info.collider != null && info.collider.gameObject.tag == "Spite")
        {
            if (isOnSpite) return;
            StartCoroutine("StepOnSpite");
        }
        else if(info.collider.gameObject.tag == "Boom")
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

    public void Hurt(float hurtValue)
    {
        hp -= hurtValue;
        UIManager.instance.HpChange(hp / maxHp);
        StartCoroutine(ColorEF(0.2f, new Color(0.5f, 0.5f, 0.5f), 0.05f, null));
        Debug.Log("i am hurt miao" + hp);
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
