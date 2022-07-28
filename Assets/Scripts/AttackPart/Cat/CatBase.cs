using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CatBase : MonoBehaviour
{
    // 只能让子类调用
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

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

    // 创建一个虚基类
    protected virtual void OnInitForPlace() { }
}