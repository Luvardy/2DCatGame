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

    private Vector2 myDirHor;
    private Vector2 myDirVer;

    private float leftTime = .1f;

    public static KingMove instance;

    private bool isOnSpite = false;
    private bool isAttackState = false;
    private bool isHurtState = false;
    public static bool canCallCat = false;
    private bool isPushing = false;

    public AudioClip move;
    public AudioClip attacked;
    public AudioClip calling;
    public AudioClip pushing;

    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    void Start()
    {
        maxHp = hp;

        attackValue = hp;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();

        if(!instance)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(myDirHor + ":Hor Ver: " + myDirVer);

        DetectSpite();
        DetectEnemy();

        if(ShowCard.disapear && !isHurtState)
        {
            animator.SetBool("Attacking", false);
            CheckCanMove();
        }
        else if(!ShowCard.disapear)
        {
            animator.SetBool("walkLeft", false);
            animator.SetBool("walkRight", false);
            animator.SetBool("walkUp", false);
            animator.SetBool("walkDown", false);
            animator.SetBool("Attacking", true);
            transform.position = GridManager.instance.GetGridPointByKing() + new Vector2(0f, 0.5f);
        }


    }

    public Vector2 GetKingPos()
    {
        return (Vector2)transform.position + new Vector2(0f, -0.58f);
    }

    //检测王前面有无敌人 是否可以继续前进
    private void CheckCanMove()
    {
        Vector2 start = (Vector2)transform.position + new Vector2(0f, -0.3f);

        RaycastHit2D checkUpLeft = Physics2D.Linecast(start + new Vector2(-0.15f, 0f), start + new Vector2(-0.15f, 0.3f), LayerMask.GetMask("Wall"));
        RaycastHit2D checkUpRight = Physics2D.Linecast(start + new Vector2(0.15f, 0f), start + new Vector2(0.15f, 0.3f), LayerMask.GetMask("Wall"));
        RaycastHit2D checkUp = Physics2D.Linecast(start, start + new Vector2(0f, 0.3f), LayerMask.GetMask("PushableBox"));

        RaycastHit2D checkDownLeft = Physics2D.Linecast(start + new Vector2(-0.15f,0f), start + new Vector2(-0.15f, -0.5f), LayerMask.GetMask("Wall"));
        RaycastHit2D checkDownRight = Physics2D.Linecast(start + new Vector2(0.15f, 0f), start + new Vector2(0.15f, -0.5f), LayerMask.GetMask("Wall"));
        RaycastHit2D checkDown = Physics2D.Linecast(start, start + new Vector2(0f, -0.55f), LayerMask.GetMask("PushableBox"));

        RaycastHit2D checkRightDown = Physics2D.Linecast(start + new Vector2(0f,-0.22f), start + new Vector2(0.5f,-0.22f), LayerMask.GetMask("Wall"));
        RaycastHit2D checkRightUp = Physics2D.Linecast(start + new Vector2(0f,0.22f), start + new Vector2(0.5f,0.22f), LayerMask.GetMask("Wall"));
        RaycastHit2D checkRight = Physics2D.Linecast(start + new Vector2(0f, 0f), start + new Vector2(0.5f, 0f), LayerMask.GetMask("PushableBox"));

        RaycastHit2D checkLeftDown = Physics2D.Linecast(start + new Vector2(0,-0.22f), start + new Vector2(-0.3f, -0.22f), LayerMask.GetMask("Wall"));
        RaycastHit2D checkLeftUp = Physics2D.Linecast(start + new Vector2(0, 0.22f), start + new Vector2(-0.3f, 0.22f), LayerMask.GetMask("Wall"));
        RaycastHit2D checkLeft = Physics2D.Linecast(start + new Vector2(0, 0.11f), start + new Vector2(-0.3f, 0.11f), LayerMask.GetMask("PushableBox"));

        Debug.DrawLine(start + new Vector2(-0.15f, 0f), start + new Vector2(-0.15f, 0.3f), Color.red);
        Debug.DrawLine(start + new Vector2(0.15f, 0f), start + new Vector2(0.15f, 0.3f), Color.red);
        Debug.DrawLine(start, start + new Vector2(0f, 0.3f), Color.red);

        Debug.DrawLine(start + new Vector2(-0.15f, 0f), start + new Vector2(-0.15f, -0.5f), Color.red);
        Debug.DrawLine(start + new Vector2(0.15f, 0f), start + new Vector2(0.15f, -0.5f), Color.red);
        Debug.DrawLine(start, start + new Vector2(0f, -0.5f), Color.red);

        Debug.DrawLine(start + new Vector2(0f, -0.22f), start + new Vector2(0.5f, -0.22f), Color.red);
        Debug.DrawLine(start + new Vector2(0f, 0.22f), start + new Vector2(0.5f, 0.22f), Color.red);

        Debug.DrawLine(start + new Vector2(0, -0.22f), start + new Vector2(-0.3f, -0.22f), Color.red);
        Debug.DrawLine(start + new Vector2(0, 0.22f), start + new Vector2(-0.3f, 0.22f), Color.red);
        Debug.DrawLine(start + new Vector2(0, 0.11f), start + new Vector2(-0.3f, 0.11f), Color.red);



        if (myDirHor == Vector2.zero && myDirVer == new Vector2(0f, -1f))
        {
            if (checkDown.collider != null)
            {
                isPushing = true;
                checkDown.collider.gameObject.GetComponent<PushBox>().isPushed(myDirHor + myDirVer, moveSpeed);
            }
        }
        else if (myDirHor == Vector2.zero && myDirVer == new Vector2(0f, 1f))
        {
            if (checkUp.collider != null)
            {
                isPushing = true;
                checkUp.collider.gameObject.GetComponent<PushBox>().isPushed(myDirHor + myDirVer, moveSpeed);
            }
        }
        else if (myDirVer == Vector2.zero && myDirHor == new Vector2(1f, 0f))
        {
            if (checkRight.collider != null)
            {
                isPushing = true;
                checkRight.collider.gameObject.GetComponent<PushBox>().isPushed(myDirHor + myDirVer, moveSpeed);
            }
        }
        else if (myDirVer == Vector2.zero && myDirHor == new Vector2(-1f, 0f))
        {
            if (checkLeft.collider != null)
            {
                isPushing = true;
                checkLeft.collider.gameObject.GetComponent<PushBox>().isPushed(myDirHor + myDirVer, moveSpeed);
            }
        }
        else
        {
            isPushing = false;
        }

        if (isPushing)
        {
            moveSpeed = 2f;
            if (!GetComponent<AudioSource>().isPlaying)
            {
                SoundManager.instance.PlaySingle(pushing);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 6f;
            }
            else
                moveSpeed = 4f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (checkLeftUp.collider == null && checkLeftDown.collider == null)
            {
                if (!GetComponent<AudioSource>().isPlaying)
                {
                    SoundManager.instance.PlaySingle(move);
                }
                myDirHor = Vector2.left;
                animator.SetBool("walkLeft", true);
                transform.Translate(myDirHor * moveSpeed * Time.deltaTime);
            }
        }
        if(Input.GetKeyUp(KeyCode.A))
        {
            myDirHor = Vector2.zero;
            animator.SetBool("walkLeft", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (checkRightUp.collider == null && checkRightDown.collider == null)
            {
                if(!GetComponent<AudioSource>().isPlaying)
                {
                    SoundManager.instance.PlaySingle(move);
                }
                myDirHor = Vector2.right;
                animator.SetBool("walkRight", true);
                transform.Translate(myDirHor * moveSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                SoundManager.instance.PlaySingle(move);
            }
            myDirHor = Vector2.zero;
            animator.SetBool("walkRight", false);
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                SoundManager.instance.PlaySingle(move);
            }
            animator.SetBool("walkUp", true);
            myDirVer = Vector2.up;
            if ( checkUpLeft.collider != null || checkUpRight.collider != null)
            {
                return;
            }
            transform.Translate(myDirVer * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            myDirVer = Vector2.zero;
            animator.SetBool("walkUp", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                SoundManager.instance.PlaySingle(move);
            }
            myDirVer = Vector2.down;
            animator.SetBool("walkDown", true);
            if (checkDownLeft.collider != null || checkDownRight.collider != null)
            {
                return;
            }
            transform.Translate(myDirVer * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            myDirVer = Vector2.zero;
            animator.SetBool("walkDown", false);
        }


    }


    IEnumerator GetHitPos(Vector2 dir)
    {
        while(leftTime > 0)
        {
            leftTime = leftTime - Time.deltaTime;
            transform.Translate(-dir * 3f * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
        leftTime = .1f;
        isHurtState = false;
    }

    private void DetectEnemy()
    {
        Vector2 start = (Vector2)transform.position + new Vector2(0f,-0.5f);
        RaycastHit2D info = Physics2D.Linecast(start, start + new Vector2(0.5f, 0f),~(1<<0));
        Debug.DrawLine(start, start + new Vector2(0.5f, 0f), Color.blue);
        if (isAttackState) return;
        //if (info.collider != null)
        //{
        //    Debug.Log(info.collider.gameObject.name);
        //}
        if (info.collider != null && info.collider.gameObject.tag == "Enemy")
        {
            Attack(info.collider.gameObject);
        }
        else if (info.collider != null && info.collider.gameObject.tag == "Switch")
        {
            Debug.Log("open");
            WallSwitch _switch = info.collider.gameObject.GetComponent<WallSwitch>();
            _switch.SwitchOn();
        }
        else if (info.collider!= null && info.collider.gameObject.tag == "Switch2")
        {
            Debug.Log("Switch2 Open");
            WallSwitch2 _switch = info.collider.gameObject.GetComponent<WallSwitch2>();
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
            isHurtState = true;
            StartCoroutine(GetHitPos(myDirVer + myDirHor));
            thisEnemy.Hurt(attackValue);
            yield return new WaitForSeconds(0.2f);
        }
        isAttackState = false;
    }
    private void DetectSpite()
    {
        Vector2 start = transform.position;
        RaycastHit2D info = Physics2D.Linecast(start + new Vector2(0f,-0.3f), start + new Vector2(0f, -0.5f),LayerMask.GetMask("Trap"));
        Debug.DrawLine(start, start + new Vector2(0f, -1f), Color.blue);
        if (info.collider != null )
        {
            Debug.Log("I am step on" + info.collider.gameObject.name);
        }
        if (info.collider != null && info.collider.gameObject.tag == "Spite")
        {
            if (isOnSpite) return;
            StartCoroutine("StepOnSpite");
        }
        else if(info.collider != null && info.collider.gameObject.tag == "Boom")
        {
            Hurt(1);
        }
        else if (info.collider != null && info.collider.gameObject.tag == "Heal")
        {
            hp += 50f;
            UIManager.instance.HpChange(hp / maxHp);
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
        SoundManager.instance.PlaySingle(attacked);
        hp -= hurtValue;
        UIManager.instance.HpChange(hp / maxHp);
        StartCoroutine(ColorEF(0.2f, new Color(0.5f, 0.5f, 0.5f), 0.05f, null));
        Debug.Log("i am hurt miao" + hp);
        if(hp < 0.01f)
        {
            hp = 0f;
            GameManager.instance.GameOver();
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
