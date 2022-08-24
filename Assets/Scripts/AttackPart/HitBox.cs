using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class HitBox : MonoBehaviour
{
    private float hp;
    private float damage;

    public Sprite broken;
    public AudioClip digging;
    SpriteRenderer spriteRenderer;

    public GameObject lightUpPre;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hp = 100f;
        damage = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        Debug.Log(Vector2.Distance(KingMove.instance.GetKingPos(), transform.position));

        if(Input.GetMouseButtonDown(0) && Vector2.Distance(KingMove.instance.GetKingPos(), transform.position) <= 3f)
        {
            if(hp>0.01f)
            {
                if(hp < 50)
                {
                    if(broken != null)
                        spriteRenderer.sprite = broken;
                }
                SoundManager.instance.PlaySingle(digging);
                hp -= damage;
                StartCoroutine(ColorEF(0.2f, new Color(0.5f, 0.5f, 0.5f), 0.05f, null));
                Debug.Log("Digging:" + hp);
            }
            else
            {
                if(lightUpPre != null)
                    lightUpPre.gameObject.SetActive(true);
                Destroy(gameObject);
            }
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
