using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cardPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool EnablePreview = true;

    [SerializeField]
    private Vector3 savePos;
    [SerializeField]
    private float upmove;
    [SerializeField]
    private int saveOrder;

    private cardDrag dragNoTarget;
    private Canvas cv;

    private void Awake()
    {
        if (GetComponent<cardDrag>() != null)
        {
            dragNoTarget = GetComponent<cardDrag>();
        }
        else
        {
            Debug.Log("没有添加拖拽脚本");
        }

        if (GetComponent<Canvas>() != null)
        {
            cv = GetComponent<Canvas>();
        }
        else
        {
            Debug.Log("没有添加Canvas");
        }
    }

    #region public
    public void OnPointerEnter(PointerEventData eventData)
    {
        SaveCardSate();
        Debug.Log("开始预览");
        if (EnablePreview && !ArrowManager.isEditing)
        {
            StartPreView();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("退出预览");
        if (EnablePreview)
        {
            EndPreView();
        }
    }

    //拖拽时的预览
    public void DragPreview()
    {
        //1.关闭普通预览功能
        EnablePreview = false;
        //2.进入拖拽预览状态
        StartDragPreView();
    }

    //结束拖拽
    public void EndDrag()
    {
        transform.localScale = Vector3.one;
        cv.sortingOrder = saveOrder;
        CheckHoverInThisCrd();
        //开启预览功能
        EnablePreview = true;
    }
    #endregion

    private void StartPreView()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        cv.sortingOrder += 100;
    }

    private void EndPreView()
    {
        transform.localScale = Vector3.one;
        cv.sortingOrder = saveOrder;
    }

    private void StartDragPreView()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        gameObject.GetComponent<CanvasGroup>().alpha = 0.5f;
    }

    //储存卡牌的初始状态
    private void SaveCardSate()
    {
        if (!dragNoTarget.dragging)
        {
            //savePos = transform.position;
            saveOrder = cv.sortingOrder;
        }
    }

    private void CheckHoverInThisCrd()
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        //创建一个鼠标事件
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        //向鼠标位置发射一条射线，射线检测到的是否点击UI
        EventSystem.current.RaycastAll(eventData, raycastResults);
        foreach (RaycastResult g in raycastResults)
        {
            if (g.gameObject == gameObject)
            {
                Debug.Log("保持预览");
                StartPreView();
            }
        }
    }
}