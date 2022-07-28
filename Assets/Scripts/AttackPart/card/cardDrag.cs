using UnityEngine;
using UnityEngine.EventSystems;

public class cardDrag : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool dragging = false;
    private bool selectMode = true;
    private cardPreview preView;

    private bool wantPlace;

    private CatBase cat;
    private CatBase catInGrid;

    public CatType carCatType;
    public bool WantPlace
    {
        get => wantPlace;
        set
        {
            wantPlace = value;
            if(wantPlace)
            {
                GameObject tempCat = CatManager.instance.GetCatType(carCatType);
                cat = Instantiate(tempCat, Vector3.zero, Quaternion.identity).GetComponent<CatBase>();
                cat.InitForCreate(false);
            }
            else
            {
                if(cat != null)
                {
                    Destroy(cat.gameObject);
                    cat = null;
                }
            }

        }
    }

    private void Awake()
    {
        if (GetComponent<cardPreview>() != null)
        {
            preView = GetComponent<cardPreview>();
        }
        else
        {
            Debug.Log("没找到Preview组件");
        }
    }

    //拖拽模式
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(!WantPlace)
            {
                WantPlace = true;
            }
            if (!dragging)
            {
                Debug.Log("按住鼠标左键");
                dragging = true;
                selectMode = false;
                //开始拖拽状态的预览
                preView.DragPreview();

            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if(catInGrid != null)
        {
            cat.transform.position = GridManager.instance.GetGridPointByMouse();
            cat.InitForPlace();
            cat = null;
            Destroy(catInGrid.gameObject);
            catInGrid = null;
        }
        Debug.Log("松开鼠标左键");
        selectMode = true;
        EndThisDrag();
    }

    //选取模式
    public void OnPointerClick(PointerEventData eventData)
    {
        //点击鼠标左键
        if (selectMode && eventData.button == PointerEventData.InputButton.Left)
        {
            if (!WantPlace)
            {
                WantPlace = true;
            }
            if (!dragging)
            {
                Debug.Log("选取");
                dragging = true;
                //开始拖拽状态的预览
                preView.DragPreview();
            }
        }

    }

    //拖拽中每帧更新位置
    private void Update()
    {
        if(WantPlace && cat != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (dragging)
            {
                cat.transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
            }
            if (Vector2.Distance(mousePos, GridManager.instance.GetGridPointByMouse()) < 1.5f)
            {
                if(catInGrid == null)
                {
                    catInGrid = Instantiate(cat, GridManager.instance.GetGridPointByMouse(), Quaternion.identity).GetComponent<CatBase>();
                    catInGrid.InitForCreate(true);
                }
                else
                {
                    catInGrid.transform.position = GridManager.instance.GetGridPointByMouse() ;
                }
            }
            else
            {
                if(catInGrid != null)
                {
                    Destroy(catInGrid.gameObject);
                    catInGrid = null;
                }

            }

            if (Input.GetMouseButtonDown(0))
            {
                cat.transform.position = GridManager.instance.GetGridPointByMouse();
                cat.InitForPlace();
                cat = null;
                if(catInGrid != null)
                {
                    Destroy(catInGrid.gameObject);
                    catInGrid = null;
                }

                EndThisDrag();
            }

        }
        else
        {
            if (catInGrid != null)
            {
                Destroy(catInGrid.gameObject);
                catInGrid = null;
            }
        }

        //点击鼠标右键
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(2);
            if (dragging)
            {
                EndThisDrag();
            }
        }
    }

    //取消拖拽，返回原来状态
    private void EndThisDrag()
    {
        Debug.Log("取消拖拽");
        dragging = false;
        preView.EndDrag();
        gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        WantPlace = false;

    }
}