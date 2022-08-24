using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum LaserState
{
    StateIdle,
    StatePlaying
}

public class Laser : MonoBehaviour
{
    private bool isAttackState = false;
    private Animator animator;
    private LaserState state = LaserState.StateIdle;
    // ���䶯������ʱ��
    public float LaunchingTime = 1.0f;
    // �������
    public float IntervalTime = 2.0f;
    // һ���������ڵ�ʱ�� = IntervalTime + LaunchingTime
    private float cycleTime;
    // ��ʱ�ۼ�ʱ�䣬һ�����ڽ���������
    private float AccTime = -1.0f;
    // ���߷���
    private Vector2 rayDirection;
    // ������ײ��
    RaycastHit2D hitsStorage = new RaycastHit2D();
    Vector2 rayOriginPoint;
    public LayerMask PlatformMask = 0;
    // ����
    private float distance = 20;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        cycleTime = IntervalTime + LaunchingTime;
        state = LaserState.StateIdle;

        // ���߷�����Ϊ����λ��
        rayOriginPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == LaserState.StateIdle && AccTime <= 0 && AccTime <= LaunchingTime)
        {
            animator.SetInteger("state", 1);
            state = LaserState.StatePlaying;
        }

        //cycleTime = IntervalTime + LaunchingTime;
        if (state == LaserState.StatePlaying && AccTime > LaunchingTime && AccTime <= cycleTime)
        {
            animator.SetInteger("state", 0);
            state = LaserState.StateIdle;
        }

        AccTime += Time.deltaTime;
        if (AccTime > cycleTime)
        {
            AccTime = 0;
        }
        // ��ȡ���ߴ򵽵�����
        hitsStorage = Physics2D.Linecast(transform.position, (Vector2)transform.position + (Vector2)transform.right * -3f, 1 << 13 | 1 << 15);
        Debug.DrawLine(transform.position, (Vector2)transform.position + (Vector2)transform.right * -3f, Color.red);
        // ������߳��ȱ仯�Ÿ�ֵ
        if (hitsStorage && distance != hitsStorage.distance)
        {
            if (hitsStorage.distance >= 5f)
            {
                distance = 5f;
            }
            else
                distance = hitsStorage.distance;
            transform.localScale = new Vector3(distance / 2.5f, 1, 1);
        }
        else
        {

        }

        if (state == LaserState.StatePlaying)
        {
            if (isAttackState) return;
            if (hitsStorage && hitsStorage.collider.gameObject.layer == 15)
            {
                StartCoroutine(DoHurt(hitsStorage.collider.gameObject.GetComponent<KingMove>()));
            }
            else if (hitsStorage && hitsStorage.collider.gameObject.layer == 13)
            {
                CatBase cat = hitsStorage.collider.gameObject.GetComponent<CatBase>();
                StartCoroutine(DoHurt(cat));
            }
        }
    }
    IEnumerator DoHurt(KingMove obj)
    {
        isAttackState = true;
        if (obj.hp > 0)
        {
            obj.Hurt(10);
            yield return new WaitForSeconds(0.1f);
        }
        isAttackState = false;

    }

    IEnumerator DoHurt(CatBase obj)
    {
        isAttackState = true;
        if (obj.hp > 0)
        {
            obj.Hurt(10);
            yield return new WaitForSeconds(0.1f);
        }
        isAttackState = false;

    }
}
