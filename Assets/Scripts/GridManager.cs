using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour {

    public Transform[,] blocks;
    public GameObject block_parent;
    public GameObject block_grid;
    public GameObject[] grids = new GameObject[5];
    public LayerMask ghost;
    public GameObject eft_boom;
    public GameObject[] maps = new GameObject[9];

    public static GridManager instance;
    
    public int block_row = 20; //블록 열
    public int block_col = 10; //블록 행
    public float blockSize = 70f;  //블록 사이즈
    private List<int> rowList;
    private List<int> removeRowList;
    private int combo = 0;
    

    private void Awake()
    {
        rowList = new List<int>();
        removeRowList = new List<int>();
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    

    private void CreateBlocks()
    {
        blocks = new Transform[block_row, block_col];
        GameObject temp_grid = Instantiate(block_grid, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        for (int row = 0; row < block_row; row++)
        {                                                           
            GameObject temp_parent = Instantiate(block_parent, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            temp_parent.transform.SetParent(temp_grid.transform);
            //blocks[row,] = temp_parent;
            int[,] saveMaps = GameManager.instance.saveMap;
            for (int col = 0; col < block_col; col++)
            {
                Vector3 tempVec = new Vector3(col * blockSize, row * blockSize, 0);
                GameObject tempBlock = Instantiate(grids[Random.Range(0,grids.Length)], tempVec, Quaternion.identity) as GameObject;
                tempBlock.transform.SetParent(temp_parent.transform);
                blocks[row, col] = tempBlock.transform;
                if (saveMaps[row, col] != 0)
                {
                    GameObject map = Instantiate(maps[saveMaps[row, col]], blocks[row, col].position, Quaternion.identity);
                    map.transform.SetParent(blocks[row, col]);
                }
                //Debug.Log(blocks[row, col].transform.position);
            }
        }
    }

    public void CheckChild()
    {
        for (int row = 0; row < block_row; row++)
        {
            for (int col = 0; col < block_col; col++)
            {
                if (blocks[row, col].childCount == 0)
                {
                    GameManager.instance.saveMap[row, col] = 0;
                } else
                {
                    GameManager.instance.saveMap[row, col] = int.Parse(blocks[row,col].GetChild(0).gameObject.tag);
                }
            }
        }
    }

    public void FindMyParent(Transform g, int i)
    {
        if (i == 0)
        {
            rowList = new List<int>();
        }
        int row = Mathf.RoundToInt(g.position.y / blockSize);
        int col = Mathf.RoundToInt(g.position.x / blockSize);

        if (row >= block_row)
        {
            Debug.Log(row);
            GameManager.instance.GameOver();
            Destroy(g.gameObject);
        } else
        {
            g.SetParent(blocks[row, col]);
            g.position = blocks[row, col].transform.position;
            //Debug.Log(g.gameObject.tag);
            GameManager.instance.saveMap[row, col] = int.Parse(g.gameObject.tag);
            bool overlap = false;
            foreach (var k in rowList)
            {
                //Debug.Log("k : " + k + ", row : " + row);
                if (k == row)
                {
                    overlap = true;
                    break;
                }
            }
            if (!overlap)
                rowList.Add(row);

            if (i == 3)
            {
                CheckFool(rowList);
                SoundManager.instance.PlayDrop();
            }
        }
    }

    public bool FindMyParent(int row, int col)
    {
        if (blocks[row,col].childCount != 0)
        {
            return false;
        }
        return true;
    }
    // Use this for initialization
    void Start () {
        CreateBlocks();
    }
	
	// Update is called once per frame
	void Update () {
        //CheckAllTime();
    }

    public void CheckFool(List<int> rows)
    {
        removeRowList = new List<int>();

        foreach (var i in rows)
        {
            //Debug.Log("rows : " + i);
            int row = i;
            bool isFool = true;
            for (int col = 0; col < block_col; col++)
            {
                //Debug.Log("count : " + blocks[row, col].childCount);
                if (blocks[row, col].childCount == 0)
                {
                    isFool = false;
                    break;
                }
            }
            if (isFool)
                removeRowList.Add(row);
        }

        if(removeRowList.Count > 0)
        {
            combo++;
            SoundManager.instance.PlayBoom(combo);
            DestroyTetrominos();
        } else
        {
            combo = 0;
        }
    }

    public void CheckAllTime()
    {
        for (int row = 0; row < block_row; row++)
        {
            for (int col = 0; col < block_col; col++)
            {
                if (blocks[row, col].childCount != 0)
                {
                    Transform block = blocks[row, col].GetChild(0);
                    block.localPosition = new Vector3(0, 0, 0);
                }
            }
        }
    }

    public void DropTetrominos()
    {
        int maxRow = -1;
        foreach(var i in removeRowList)
        {
            //Debug.Log("removeRowList[] : " + i);
            if (i > maxRow)
                maxRow = i;
        }

        //Debug.Log("maxRow : " + maxRow);
        for (int row = maxRow + 1; row < block_row; row++)
        {
            for (int col = 0; col < block_col; col++)
            {
                if (blocks[row, col].childCount != 0)
                {
                    Transform block = blocks[row, col].GetChild(0);
                    Vector3 start = blocks[row, col].position;
                    Vector3 end = blocks[row - removeRowList.Count, col].position;
                    RaycastHit2D hit;
                    hit = DropCheck(start, end);
                    if (!hit)
                    {
                        block.transform.SetParent(null);
                        //block.gameObject.GetComponent<Rigidbody2D>().MovePosition(new Vector3(0, 0, 0));
                        block.transform.SetParent(blocks[row - removeRowList.Count, col]);
                    }
                    block.localPosition = new Vector3(0, 0, 0);
                    block.position = block.parent.position;
                }
            }
        }

        SetScore();
    }

    public RaycastHit2D DropCheck(Vector3 start, Vector3 end)
    {
        //Debug.Log("Start : " + start);
        //Debug.Log("end : " + end);
        RaycastHit2D hit = Physics2D.Linecast(transform.position, end, ghost);
        return hit;
    }

    public void DestroyTetrominos()
    {
        foreach (var i in removeRowList)
        {
            //Debug.Log("removeRowList : " + i);
            for (int col = 0; col < block_col; col++)
            {
                Instantiate(eft_boom, blocks[i, col].position - new Vector3(0,blockSize/2,0), Quaternion.identity);
                Destroy(blocks[i, col].GetChild(0).gameObject);
            }
        }
        if (removeRowList.Count == 2 || removeRowList.Count == 3)
            CheckHole();
        DropTetrominos();
    }

    public void SetScore()
    {
        int temp;
        if (removeRowList.Count == 4)
        {
            temp = 80;
        } else
        {
            temp = removeRowList.Count * 10;
        }
        //temp = Mathf.RoundToInt(temp + Mathf.Pow(5, combo - 1));
        temp = Mathf.RoundToInt(temp + (10 * (combo - 1)));
        GameManager.instance.SetTexts(temp);
    }

    public void CheckHole()
    {
        List<int> tempList = new List<int>();
        int next = -1;
        int minHole = 0;
        int minRow = 0;
        foreach(var i in removeRowList)
        {
            //Debug.Log("next : " + next + "& i : " + i);
            if (next - i > 1 && next != -1)
            {
                minHole = i + 1;
                //Debug.Log("i + 1 : " + (i + 1));
                //Debug.Log("next - 1 : " + (next - 1));

                for (int k = i + 1; k <= (next - 1); k++)
                {
                    tempList.Add(k);
                }
            } 
            next = i;
            minRow = i;
        }

        int itv = minHole - minRow;

        //Debug.Log("itv : " + itv);
        //Debug.Log("minRow : " + minRow);
        //Debug.Log("minHole : " + minHole);
        //Debug.Log("tempList : " + tempList.Count);

        if (tempList.Count > 0)
        {
            foreach (var i in tempList)
            {
                for (int col = 0; col < block_col; col++)
                { 
                    //Debug.Log("childCount : " + blocks[i, col].childCount);
                    if (blocks[i, col].childCount != 0)
                    {
                        Transform block = blocks[i, col].GetChild(0);
                        Vector3 start = blocks[i, col].position;
                        Vector3 end = blocks[i - itv, col].position;
                        RaycastHit2D hit;
                        hit = DropCheck(start, end);
                        //Debug.Log("start : " + start);
                        //Debug.Log("end : " + end);
                        if (!hit)
                        {
                            block.transform.SetParent(null);
                            //block.gameObject.GetComponent<Rigidbody2D>().MovePosition(new Vector3(0, 0, 0));
                            block.transform.SetParent(blocks[i - itv, col]);
                        }
                        block.localPosition = new Vector3(0, 0, 0);
                    }
                }
            }
        }
    }

    public void DestroyChild(int row, int col)
    {
        Destroy(blocks[row, col].GetChild(0).gameObject);
    }

    public void OneLowPlus()
    {
        for (int row = block_row - 1; row >= 0 ; row--)
        {
            for (int col = block_col - 1; col >= 0 ; col--)
            {
                if (blocks[row, col].childCount != 0)
                {
                    Transform block = blocks[row, col].GetChild(0);
                    if (blocks[row, col].childCount > 1)
                        block = blocks[row, col].GetChild(blocks[row,col].childCount-1);
                    block.transform.SetParent(null);
                    block.transform.SetParent(blocks[row + 1, col]);
                    block.localPosition = new Vector3(0, 0, 0);
                    block.position = block.parent.position;
                }
            }
        }

        int hole = Random.Range(0, block_col);
        for (int i = 0; i < block_col; i++)
        {
            if (i != hole)
                Instantiate(maps[8], blocks[0, i]);
        }

        SoundManager.instance.PlayUp();
    }
}
