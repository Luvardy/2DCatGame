using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    private List<Vector2> pointList = new List<Vector2>();
    private List<Grid> GridList = new List<Grid>();
    private List<GameObject> RangeList = new List<GameObject>();

    public static GridManager instance;

    public GameObject PlaceRange;

    // Start is called before the first frame update
    void Start()
    {
        if(!instance)
        {
            instance = this;
        }
        CreateGridsBaseGrid();
        CreateGridsBasePointList();
        CreateGridBaseColl();
    }

    private void Update()
    {
        CheckCanPlace();
        Debug.DrawLine(GetGridPointByMouse(), Vector2.right);
        if (Input.GetMouseButton(0))
        {
            print(GetGridPointByMouse());
        }
    }


    private void CreateGridBaseColl()
    { 
            // 由于这个预制体比较简单，所以直接用代码来定义，创建一个预制体网格
            GameObject prefabGrid = new GameObject();
            // 设置碰撞器大小
            prefabGrid.AddComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
            prefabGrid.GetComponent<BoxCollider2D>().isTrigger = true;
            // 父物体是网格管理器
            prefabGrid.transform.SetParent(transform);
            // 位置就是网格管理器的位置
            prefabGrid.transform.position = this.transform.position;
            prefabGrid.tag = "Grid";
            prefabGrid.name = 0 + "0" + 0;
            for (int i = 0; i < 20; ++i)
            {
                for (int j = 0; j < 6; ++j)
                {
                        GameObject grid = GameObject.Instantiate(prefabGrid, transform.position + new Vector3(1f * i, 1f * j, 0), Quaternion.identity);
                        grid.name = i + "-" + j;         

                }
            }
        }


    private void CreateGridsBaseGrid()
    {
        for (int i = 0; i < 20; ++i)
        {
            for (int j = 0; j < 6; ++j)
            {
                // 由于该脚本依附的游戏对象是在根目录，所以transform.position是世界坐标
                GridList.Add(new Grid(new Vector2(i, j),
                    transform.position + new Vector3(1f * i, 1f * j, 0), false));
            }
        }
    }
    private void CreateGridsBasePointList()
    {
        for (int i = 0; i < 20; ++i)
        {
            for (int j = 0; j < 6; ++j)
            {
                // 将每个点预先保存在二维list中
                pointList.Add(transform.position + new Vector3(1f * i, 1f * j, 0));
            }
        }
    }

    public Vector2 GetGridPointByMouse()
    {
        return GetGridByMouse().Position;
    }

    public Grid GetGridByMouse()
    {
        float dis = 999999;
        Grid grid = null;
        for (int i = 0; i < GridList.Count; ++i)
        {
            float mouseToGrid = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), GridList[i].Position);
            if (mouseToGrid < dis)
            {
                dis = mouseToGrid;
                grid = GridList[i];
            }
        }
        return grid;
    }

    public Vector2 GetGridPointByKing()
    {
        float dis = 999999;
        Vector2 point = pointList[0];
        for (int i = 0; i < GridList.Count; ++i)
        {
            float kingToGrid = Vector2.Distance(KingMove.instance.GetKingPos(), GridList[i].Position);
            if (kingToGrid < dis)
            {
                dis = kingToGrid;
                point = GridList[i].Position;
            }
        }
        return point;
    }

    public bool CheckNearKing()
    {
        float kinglPosX = GetGridPointByKing().x;
        if (GetGridPointByMouse().x - kinglPosX <= 6.01f && GetGridPointByMouse().x - kinglPosX >= -0.01f)
            return true;
        else return false;
    }

    public bool CheckCanPlace()
    {
        RaycastHit2D info = Physics2D.Raycast(GetGridPointByMouse(), Vector2.right, 0.1f);
        if (info.collider!=null)
        {
            if (info.collider.gameObject.tag == "Enemy" || info.collider.gameObject.tag == "Wall")
            {
                Debug.Log("FindEnemy");
                return false;
            }
            else
                return true;
        }
        return true;

    }



}
