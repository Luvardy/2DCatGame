using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    private List<Vector2> pointList = new List<Vector2>();
    private List<Grid> GridList = new List<Grid>();
    private List<GameObject> RangeList = new List<GameObject>();

    public static GridManager instance;

    public GameObject initPos1;
    public GameObject initPos2;
    public GameObject initPos3;
    public GameObject initPos4;
    public GameObject initPos5;
    public GameObject initPos6;


    public GameObject PlaceRange;

    // Start is called before the first frame update
    void Start()
    {
        if(!instance)
        {
            instance = this;
        }
        CreateGridsBaseGrid(initPos2, 40, 6);
        CreateGridBaseColl(initPos2, 40, 6);

        CreateGridsBaseGrid(initPos4, 18, 13);
        CreateGridBaseColl(initPos4, 18, 13);
        CreateGridsBaseGrid(initPos6, 9, 10);
        CreateGridBaseColl(initPos6, 9, 10);

    }

    private void Update()
    {
        CheckCanPlace();
        Debug.DrawLine(GetGridPointByMouse(), Vector2.right);
    }


    private void CreateGridBaseColl(GameObject initPos, int lineHor, int lineVer)
    { 
            // 由于这个预制体比较简单，所以直接用代码来定义，创建一个预制体网格
            GameObject prefabGrid = new GameObject();
            // 设置碰撞器大小
            prefabGrid.AddComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
            prefabGrid.GetComponent<BoxCollider2D>().isTrigger = true;
            // 父物体是网格管理器
            prefabGrid.transform.SetParent(transform);
            // 位置就是网格管理器的位置
            prefabGrid.transform.position = transform.position;
            prefabGrid.tag = "Grid";
            prefabGrid.name = 0 + "0" + 0 ;
            for (int i = 0; i < lineHor; ++i)
            {
                for (int j = 0; j < lineVer; ++j)
                {
                        GameObject grid = GameObject.Instantiate(prefabGrid, initPos.gameObject.transform.position + new Vector3(1f * i, 1f * j, 0), Quaternion.identity);
                        grid.name = i + "-" + j + initPos.gameObject.name;         

                }
            }
        }


    private void CreateGridsBaseGrid(GameObject initPos, int lineHor, int lineVer)
    {
        for (int i = 0; i < lineHor; ++i)
        {
            for (int j = 0; j < lineVer; ++j)
            {
                // 由于该脚本依附的游戏对象是在根目录，所以transform.position是世界坐标
                GridList.Add(new Grid(new Vector2(i, j),
                    initPos.gameObject.transform.position + new Vector3(1f * i, 1f * j, 0), null));
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
        Vector2 point = GridList[0].Position;
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
        float kingPosX = GetGridPointByKing().x;
        float kingPosY = GetGridPointByKing().y;
        if (GetGridPointByMouse().x - kingPosX <= 1.01f && GetGridPointByMouse().x - kingPosX >= -1.01f
            &&GetGridPointByMouse().y -kingPosY <= 1.01f && GetGridPointByMouse().y - kingPosY >= -1.01f)
        {

            return true;
        }
            
        else return false;
    }

    public void ShowNearKing(GameObject canEdit)
    {
        float kingPosX = GetGridPointByKing().x;
        float kingPosY = GetGridPointByKing().y;

        for (int i = 0; i <GridList.Count; i++)
        {
            if (GridList[i].Position.x - kingPosX <= 1.01f && GridList[i].Position.x - kingPosX >= -1.01f
                && GridList[i].Position.y - kingPosY <= 1.01f && GridList[i].Position.y - kingPosY >= -1.01f)
            {
                GridList[i].CanPlace = Instantiate(canEdit, GridList[i].Position, Quaternion.identity);
            }
        }
    }

    public void DestoryNearKing()
    {
        for(int i = 0; i< GridList.Count;i++)
        {
            if(GridList[i].CanPlace != null)
            {
                Destroy(GridList[i].CanPlace);
            }
        }
    }

    public bool CheckCanPlace()
    {
        RaycastHit2D info = Physics2D.Raycast(GetGridPointByMouse(), Vector2.right, 0.1f);
        if (info.collider!=null)
        {
            if (info.collider.gameObject.tag == "Enemy" || info.collider.gameObject.tag == "Wall")
            {
                return false;
            }
            else
                return true;
        }
        return true;

    }



}
