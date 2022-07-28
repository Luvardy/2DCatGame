using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    private List<Vector2> pointList = new List<Vector2>();
    private List<Grid> GridList = new List<Grid>();

    public static GridManager instance;

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
        if (Input.GetMouseButtonDown(0))
        {
            print(GetGridPointByMouse());
        }
    }


    private void CreateGridBaseColl()
    { 
            // 由于这个预制体比较简单，所以直接用代码来定义，创建一个预制体网格
            GameObject prefabGrid = new GameObject();
            // 设置碰撞器大小
            prefabGrid.AddComponent<BoxCollider2D>().size = new Vector2(1.83f, 1.80f);
            // 父物体是网格管理器
            prefabGrid.transform.SetParent(transform);
            // 位置就是网格管理器的位置
            prefabGrid.transform.position = this.transform.position;
            prefabGrid.name = 0 + "0" + 0;
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    // 每次实例时需要增加偏移量，让每个网格的上下和左右保持一定的距离
                    GameObject grid = GameObject.Instantiate(prefabGrid, transform.position + new Vector3(1.97f * i, 1.96f * j, 0), Quaternion.identity);
                    grid.name = i + "-" + j;
                }
            }
        }


    private void CreateGridsBaseGrid()
    {
        for (int i = 0; i < 9; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                // 由于该脚本依附的游戏对象是在根目录，所以transform.position是世界坐标
                GridList.Add(new Grid(new Vector2(i, j),
                    transform.position + new Vector3(1.97f * i, 1.96f * j, 0), false));
            }
        }
    }
    private void CreateGridsBasePointList()
    {
        for (int i = 0; i < 9; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                // 将每个点预先保存在二维list中
                pointList.Add(transform.position + new Vector3(1.97f * i, 1.96f * j, 0));
            }
        }
    }

    public Vector2 GetGridPointByMouse()
    {
        float dis = 999999;
        Vector2 point = pointList[0];
        for (int i = 0; i < GridList.Count; ++i)
        {
            float mouseToGrid = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), GridList[i].Position);
            if (mouseToGrid < dis)
            {
                dis = mouseToGrid;
                point = GridList[i].Position;
            }
        }
        return point;
    }

}
