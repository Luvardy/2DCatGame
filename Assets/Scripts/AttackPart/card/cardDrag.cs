using UnityEngine;
using UnityEngine.EventSystems;

public class cardDrag : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool dragging = false;
    private static bool selectMode = true;
    private static bool clicked = false;
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
        if (eventData.button == PointerEventData.InputButton.Left && !clicked)
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
        if(!clicked)
        {
            Debug.Log("松开鼠标左键");
            EndThisDrag();
        }

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
                selectMode = false;
                clicked = true;
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
            Grid grid = GridManager.instance.GetGridByMouse();
            if (dragging)
            {
                cat.transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
            }
            if (GridManager.instance.CheckNearKing () && Vector2.Distance(mousePos, grid.Position) < 1.5f)
            {
                if(catInGrid == null)
                {
                    catInGrid = Instantiate(cat, grid.Position, Quaternion.identity).GetComponent<CatBase>();
                    catInGrid.InitForCreate(true);
                }
                else
                {
                    catInGrid.transform.position = grid.Position;
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

            if (catInGrid != null && Input.GetMouseButtonDown(0))
            {
                cat.transform.position = grid.Position;
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
                clicked = false;
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
        selectMode = true;
        gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        WantPlace = false;

    }
}