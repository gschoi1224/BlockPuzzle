using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoPositions
{
    public int width;
    public int height;
    public Vector2 pivot;
    public float rotation;
    public Vector3[] childLocal;

    public static TetrominoPositions tp;

    public TetrominoPositions(int w, int h, Vector2 p, float r)
    {
        width = w;
        height = h;
        pivot = p;
        rotation = r;
    }
    public TetrominoPositions() {
        if (tp == null)
        {
            tp = this;
        }
    }

    public TetrominoPositions[] GetTetrominoPosition(string name, TetrominoPositions[] tps)
    {
        name = name.Replace("(Clone)", "");

        //Debug.Log("name : " + name);
        if (name == "Tetromino_I")
        {
            tps = new TetrominoPositions[4];
            tps[0] = new TetrominoPositions(280, 70, new Vector2(70, 0), 0);
            tps[1] = new TetrominoPositions(70, 280, new Vector2(0, 140), 270);
            tps[2] = new TetrominoPositions(280, 70, new Vector2(140, 0), 180);
            tps[3] = new TetrominoPositions(70, 280, new Vector2(0, 70), 90);
        }

        if (name == "Tetromino_J")
        {
            tps = new TetrominoPositions[4];
            tps[0] = new TetrominoPositions(210, 140, new Vector2(70, 70), 0);
            tps[1] = new TetrominoPositions(140, 210, new Vector2(70, 70), 270);
            tps[2] = new TetrominoPositions(210, 140, new Vector2(70, 0), 180);
            tps[3] = new TetrominoPositions(140, 210, new Vector2(0, 70), 90);
        }

        if (name == "Tetromino_L")
        {
            tps = new TetrominoPositions[4];
            tps[0] = new TetrominoPositions(210, 140, new Vector2(70, 70), 0);
            tps[1] = new TetrominoPositions(140, 210, new Vector2(70, 70), 270);
            tps[2] = new TetrominoPositions(210, 140, new Vector2(70, 0), 180);
            tps[3] = new TetrominoPositions(140, 210, new Vector2(0, 70), 90);
        }

        if (name == "Tetromino_O")
        {
            tps = new TetrominoPositions[1];
            tps[0] = new TetrominoPositions(140, 140, new Vector2(0, 0), 0);
        }

        if (name == "Tetromino_S")
        {
            tps = new TetrominoPositions[4];
            tps[0] = new TetrominoPositions(210, 140, new Vector2(70, 70), 0);
            tps[1] = new TetrominoPositions(140, 210, new Vector2(70, 70), 270);
            tps[2] = new TetrominoPositions(210, 140, new Vector2(70, 0), 180);
            tps[3] = new TetrominoPositions(140, 210, new Vector2(0, 70), 90);
        }

        if (name == "Tetromino_Z")
        {
            tps = new TetrominoPositions[4];
            tps[0] = new TetrominoPositions(210, 140, new Vector2(70, 70), 0);
            tps[1] = new TetrominoPositions(140, 210, new Vector2(70, 70), 270);
            tps[2] = new TetrominoPositions(210, 140, new Vector2(70, 0), 180);
            tps[3] = new TetrominoPositions(140, 210, new Vector2(0, 70), 90);
        }

        if (name == "Tetromino_T")
        {
            tps = new TetrominoPositions[4];
            tps[0] = new TetrominoPositions(210, 140, new Vector2(70, 0), 0);
            tps[1] = new TetrominoPositions(140, 210, new Vector2(0, 70), 270);
            tps[2] = new TetrominoPositions(210, 140, new Vector2(70, 70), 180);
            tps[3] = new TetrominoPositions(140, 210, new Vector2(70, 70), 90);
        }
        return tps;
    }

    public void SetLocalChildren(Transform[] children, int i, TetrominoPositions[] tps)
    {
        Vector3[] list = new Vector3[4];
        for(int k = 0; k < children.Length; k++)
        {
            list[k] = GetLocalPosition(i, k, children, tps);
        }
        childLocal = list;
    }


    private Vector3 GetLocalPosition(int tpsIndex, int childIndex, Transform[] myChildren, TetrominoPositions[] tps)
    {
        float x = myChildren[childIndex].localPosition.x;
        float y = myChildren[childIndex].localPosition.y;

        //Debug.Log("original X : " + x);
        //Debug.Log("original Y : " + y);

        int i = 0;

        if (Mathf.Abs(x) == Mathf.Abs(y))
        {
            if (x < 0 && y < 0)
            {
                i = 0;
            } else if (x > 0 && y < 0)
            {
                i = 1;
            } else if (x > 0 && y > 0)
            {
                i = 2;
            } else if (x < 0 && y > 0)
            {
                i = 3;
            }
        } else
        {
            if (x > 0 || y > 0)
            {
                i = 2;
            }
        }


        if (tps[tpsIndex].rotation == 90)           // x,y 좌표 반전
        {
                i += 1;
        }
        else if (tps[tpsIndex].rotation == 180)   // 부호 반전
        {
                i += 2;
        }
        else if (tps[tpsIndex].rotation == 270)   // 부호 반전, 좌표 반전
        {
                i += 3;
        }

        if (i >= 4)
        {
            i -= 4;
        }

        //Debug.Log("i : " + i);
        if (i == 0)      // ( -, - )
        {
            x = Mathf.Abs(myChildren[childIndex].localPosition.x) * -1;
            y = Mathf.Abs(myChildren[childIndex].localPosition.y) * -1;
        } else if (i == 1)   // ( +, - )
        {
            x = Mathf.Abs(myChildren[childIndex].localPosition.y);
            y = Mathf.Abs(myChildren[childIndex].localPosition.x) * -1;
        } else if (i == 2)   // ( +, + )
        {
            x = Mathf.Abs(myChildren[childIndex].localPosition.x);
            y = Mathf.Abs(myChildren[childIndex].localPosition.y);
        } else if (i == 3)   // ( - , + )
        {
            x = Mathf.Abs(myChildren[childIndex].localPosition.y) * -1;
            y = Mathf.Abs(myChildren[childIndex].localPosition.x);
        }


        //Debug.Log("tps[" + tpsIndex + "].myChildren[" + childIndex + "].localPosition = (" + x + ", " + y + ")");
        return new Vector3(x, y, 0);
    }

}

public class GridOption
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public Transform topBlock;
    public Transform bottomBlock;
    public Transform leftBlock;
    public Transform rightBlock;

    public GridOption(float minX, float maxX, float minY, float maxY, Transform t, Transform b, Transform l, Transform r)
    {
        this.minX = minX;
        this.maxX = maxX;
        this.minY = minY;
        this.maxY = maxY;
        topBlock = t;
        bottomBlock = b;
        leftBlock = l;
        rightBlock = r;
    }

    public GridOption()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tpsIndex"></param>
    /// <param name="tps"></param>
    /// <param name="child"></param>
    /// <returns></returns>
    public GridOption SetGridOption(int tpsIndex, TetrominoPositions[] tps, Transform[] child)
    {
        Transform[,] blocks = GridManager.instance.blocks;
        int gridCol = GridManager.instance.block_col;
        int gridRow = GridManager.instance.block_row;

        float x = child[0].parent.position.x - child[0].position.x;
        float y = child[0].parent.position.y - child[0].position.y;

        int top = Mathf.RoundToInt(y);
        int bottom = Mathf.RoundToInt(y);
        int left = Mathf.RoundToInt(x);
        int right = Mathf.RoundToInt(x);

        topBlock = child[0];
        bottomBlock = child[0];
        leftBlock = child[0];
        rightBlock = child[0];

        for (int i = 0; i < child.Length; i++)
        {
            x = child[i].parent.position.x - child[i].position.x;
            y = child[i].parent.position.y - child[i].position.y;

            if (top >= y)
            {
                top = Mathf.RoundToInt(y);
                topBlock = child[i];
            }
            if (bottom <= y)
            {
                bottom = Mathf.RoundToInt(y);
                bottomBlock = child[i];
            }
            if (left < x)
            {
                left = Mathf.RoundToInt(x);
                leftBlock = child[i];
            }
            if (right > x)
            {
                right = Mathf.RoundToInt(x);
                rightBlock = child[i];
            }
        }

        float minX = blocks[0, 0].position.x + left;
        float maxX = blocks[gridRow - 1, gridCol - 1].position.x + right;

        float minY = blocks[0, 0].position.y + bottom;
        float maxY = blocks[gridRow - 1, gridCol - 1].position.y + top;

        GridOption go = new GridOption(minX, maxX, minY, maxY, topBlock, bottomBlock, leftBlock, rightBlock);

        //Debug.Log("go.minX : " + go.minX);
        //Debug.Log("go.minY : " + go.minY);
        //Debug.Log("go.maxX : " + go.maxX);
        //Debug.Log("go.maxY : " + go.maxY);

        //Debug.Log("topBlock : " + topBlock.position);
        //Debug.Log("bottomBlock : " + bottomBlock.position);
        //Debug.Log("leftBlock : " + leftBlock.position);
        //Debug.Log("rightBlock : " + rightBlock.position);

        return go;
    }
}

public class Tetromino : MonoBehaviour {

    public static Tetromino instance;
    public float fallSpeed = 500f;
    public bool canMove = true;
    public LayerMask tetrominoLayer;
    public LayerMask wall;
    public GameObject ghost;
    public static Tetromino t;

    private int leftCheck;
    private int rightCheck;
    private int bottomCheck;
    private int bottomBlock;
    private float speed;
    private float blockSize;
    private float timeOut;

    private Rigidbody2D rig;
    private Vector3 pos;
    private Vector3 velVec;
    private TetrominoPositions[] tps;
    private int tpsIndex = 0;
    private Collider2D myCol;
    private GridOption go;
    private Coroutine stayBottom;

    private Transform[] myChildren = new Transform[4];

    // 객체 생성되면서 실행 - 1
    private void Awake()
    {
        t = this;
        go = new GridOption();
        rig = GetComponent<Rigidbody2D>();
        leftCheck = 1;
        rightCheck = 1;
        bottomCheck = 1;
        if (GetComponent<BoxCollider2D>() != null)
        {
            myCol = GetComponent<BoxCollider2D>();
        } else if (GetComponent<PolygonCollider2D>() != null)
        {
            myCol = GetComponent<PolygonCollider2D>();
        }
        for (int i = 0; i < 4; i++)
        {
            myChildren[i] = transform.GetChild(i);
            myChildren[i].GetComponent<BoxCollider2D>().enabled = false;
            myChildren[i].GetComponent<Rigidbody2D>().Sleep();
        }
    }

    // 객체 생성되면서 실행 - 2 (Awake다음)
    private void Start()
    {
        //Debug.Log(GridManager.instance);
        blockSize = GridManager.instance.blockSize;
        GetPositions(transform.name);
        timeOut = 7f / GameManager.instance.level * 0.5f;
        SetGhostPosition();
    }
    // tps(pivot size 등 담은 파일), 
    public void GetPositions(String name)
    {
        new TetrominoPositions();
        tps = TetrominoPositions.tp.GetTetrominoPosition(name, tps);
        for (int i = 0; i < tps.Length; i++)
        {
            tps[i].SetLocalChildren(myChildren, i, tps);
        }
        go = go.SetGridOption(tpsIndex, tps, myChildren);
    }

    public void SetGhostPosition()
    {
        Vector3 end = transform.position;
        end.y = go.minY;
        HitMinMax hit = IsHit(end);
        if (hit.hit)
        {
            //Debug.Log("max : " + hit.top);
            //Debug.Log("min : " + hit.bottom);
            //Debug.Log("hit.max.y : " + hit.max.y + ", blockSize ; " + blockSize);

            //Debug.Log("local max : " + hit.localTop);
            //Debug.Log("local min : " + hit.localBottom);

            //Debug.Log("pivot : " + tps[tpsIndex].pivot);


            float basic = hit.top.y - hit.bottom.y;
            float local = hit.localTop.y - hit.localBottom.y;

            //float top = hit.top.y - hit.localTop.y;
            //float bottom = hit.bottom.y - hit.localBottom.y;

            //Debug.Log("tag ; " + hit.tag);

            if (basic >= local)
            {
                end.y = hit.top.y + blockSize - hit.localTop.y;
            }
            else
            {
                end.y = hit.bottom.y + blockSize - hit.localBottom.y;
            }

            //Debug.Log("top ; " + basic);
            //Debug.Log("bottom ; " + local);

        }
        //Debug.Log("end : " + end);
        ghost.transform.position = end;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            CheckUserInput();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            speed = -1 * fallSpeed * Time.deltaTime * GameManager.instance.level * bottomCheck * bottomBlock;
            velVec = new Vector3(0, speed, 0);
            rig.velocity = velVec;
            SetGhostPosition();
            InGridCheck();
            IsBottomBlock();
        }
    }

    // 내려갈 곳에 블록 있냐
    private void IsBottomBlock()
    {

        for (int i = 0; i < myChildren.Length; i++)
        {
            int row = Mathf.FloorToInt(myChildren[i].position.y / blockSize);
            int col = Mathf.RoundToInt(myChildren[i].position.x / blockSize);

            if (row < 0)
            {
                row = 0;
            }
            if (row >= GridManager.instance.block_row - 1)
            {
                row = GridManager.instance.block_row - 1;
            }

            if (!GridManager.instance.FindMyParent(row, col))
            {
                bottomBlock = 0;
                pos = transform.position;
                float f = (transform.position.y / blockSize);
                int k = Mathf.RoundToInt(f);

                pos.y = k * blockSize;
                transform.position = pos;
                if (canMove)
                    stayBottom = StartCoroutine("StayBottom");
                break;
            }
            else
            {
                bottomBlock = 1;
            }
        }
    }

    Coroutine keepRight;
    Coroutine keepLeft;
    Coroutine keepBottom;

    void CheckUserInput()
    {
        Vector3 end = transform.position;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GoRight();
            keepRight = StartCoroutine("KeepRight");
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && keepRight != null) {
            StopCoroutine(keepRight);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GoLeft();
            keepLeft = StartCoroutine("KeepLeft");
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && keepLeft != null)
        {
            StopCoroutine(keepLeft);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GoBottom();
            keepBottom = StartCoroutine("KeepBottom");
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && keepBottom != null)
        {
            StopCoroutine(keepBottom);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && tps.Length != 1)
        {
            RotateTetromino();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GoDrop();
        }

        if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.RightShift))
        {
            GameManager.instance.GetHold();
        }
    }

    public void GoDrop()
    {
        transform.position = ghost.transform.position;
        StopTetromino();
    }

    IEnumerator KeepRight()
    {
        yield return new WaitForSeconds(.5f);
        GoRight();

        while (true)
        {
            yield return new WaitForSeconds(.2f);
            GoRight();
        }
    }

    IEnumerator KeepLeft()
    {
        yield return new WaitForSeconds(.5f);
        GoLeft();

        while (true)
        {
            yield return new WaitForSeconds(.2f);
            GoLeft();
        }
    }

    IEnumerator KeepBottom()
    {
        yield return new WaitForSeconds(.5f);
        GoBottom();

        while (true)
        {
            yield return new WaitForSeconds(.2f);
            GoBottom();
        }
    }
    public void GoRight()
    {
        Vector3 end = transform.position + new Vector3(blockSize * rightCheck, 0, 0);
        if (end != transform.position)
        {
            if (!IsHit(end).hit)
            {
                transform.position = end;
                SoundManager.instance.PlayMove();
                SetGhostPosition();
            }
            else
            {
                StopCoroutine(keepRight);
            }
        }
    }

    public void GoLeft()
    {
        Vector3 end = transform.position + new Vector3(blockSize * leftCheck * -1, 0, 0);
        if (end != transform.position)
        {
            if (!IsHit(end).hit)
            {
                transform.position = end;
                SoundManager.instance.PlayMove();
                SetGhostPosition();
            }
            else
            {
                StopCoroutine(keepLeft);
            }
        }
    }

    public void GoBottom()
    {
        Vector3 end = transform.position + new Vector3(0, -1 * blockSize * bottomCheck, 0);
        if (end != transform.position)
        {
            if (!IsHit(end).hit)
            {
                transform.position = end;
                SoundManager.instance.PlayMove();
                SetGhostPosition();
            }
            else
            {
                StopCoroutine(keepBottom);
            }
        }
    }

    public void RotateTetromino()
    {
        //벽에 붙어서 회전시 통과 방지
        HitMinMax hmm = RotateCheck();
        int nextIndex = tpsIndex + 1;
        if (nextIndex >= tps.Length)
        {
            nextIndex = 0;
        }

        //Debug.Log("max : " + hmm.max);
        //Debug.Log("min : " + hmm.min);
        //Debug.Log("localMax : " + hmm.localMax);
        //Debug.Log("localMin : " + hmm.localMin);
        //Debug.Log("tag : " + hmm.tag);
        //Debug.Log("hit : " + hmm.hit);
         

        if (hmm.hit)
        {
            if (DirectionCheck(hmm, nextIndex))
            {
                DoRotate();
            }
        } else
        {
            DoRotate();
        }
    }

    private bool DirectionCheck(HitMinMax hmm, int i)
    {
        Vector3 move = transform.position;

        if (hmm.left != hmm.right || hmm.right != hmm.left) //충돌 발생한 부분이 두 곳 이상일 경우 이동도 안하고 회전도 안함
        {
            return false;
        }

        //Debug.Log("move 1111 : " + move);

        //Debug.Log("top : " + hmm.top);
        //Debug.Log("local top : " + hmm.localTop);
        //Debug.Log("bottom : " + hmm.bottom);
        //Debug.Log("localBottom : " + hmm.localBottom);
        //Debug.Log("left : " + hmm.left);
        //Debug.Log("localLeft : " + hmm.localLeft);
        //Debug.Log("right : " + hmm.right);
        //Debug.Log("localRight : " + hmm.localRight);

        // 충돌 방향 판별
        if (move.x > hmm.left.x)  // 왼쪽 충돌
        {
            //Debug.Log("왼쪽충돌");
            move.x = hmm.left.x + blockSize - hmm.localLeft.x;
        }
        else if (move.x < hmm.right.x)      // 오른쪽 충돌
        {
            //Debug.Log("오른쪽충돌");
            move.x = hmm.right.x - blockSize - hmm.localRight.x;
        }
        if (move.y > hmm.bottom.y)  // 바닥 충돌 
        {
            //Debug.Log("바닥충돌");
            move.y = hmm.bottom.y + blockSize - hmm.localBottom.y;
        }
        else if (move.y < hmm.top.y)
        {
            //Debug.Log("위쪽 충돌");
            move.y = hmm.top.y - blockSize - hmm.localTop.y;
        }
        //Debug.Log("move 2222: " + move);
        if (!IsHit(move).hit)  
        {
            transform.position = move;
            return true;
        }


        return false;
    }

    private void DoRotate()
    {
        tpsIndex++;
        float spin = -90f;

        if (tpsIndex >= tps.Length)
        {
            tpsIndex = 0;
        }
        transform.Rotate(0, 0, spin);
        for (int i = 0; i < myChildren.Length; i++)
        {
            myChildren[i].Rotate(0, 0, spin * -1);
        }
        go = go.SetGridOption(tpsIndex, tps, myChildren);

        ghost.transform.Rotate(0, 0, spin);
        for (int i = 0; i < ghost.transform.childCount; i++)
        {
            ghost.transform.GetChild(i).Rotate(0, 0, spin * -1);
        }
        SoundManager.instance.PlayMove();
        SetGhostPosition();
    }


    IEnumerator StayBottom()
    {
        //7f / GameManager.instance.level * 0.5f
        Debug.Log("Start Coroutine bottomCheck : " + bottomCheck + ", bottomBlock : " + bottomBlock);
        yield return new WaitForSeconds(timeOut);
        Debug.Log("End Coroutine bottomCheck : " + bottomCheck + ", bottomBlock : " + bottomBlock);
        if (bottomCheck == 0 || bottomBlock == 0)
        {
            StopTetromino();
        } else
        {
            StopCoroutine(stayBottom);
        }
    }

   

    private void OnCollisionExit2D(Collision2D collision)
    {
        BoxCollider2D col;

        if (collision.collider.GetComponent<BoxCollider2D>() != null)
        {
            col = collision.collider.GetComponent<BoxCollider2D>();
            if (col.name == "Wall_L")
            {
                leftCheck = 1;
            }
            if (col.name == "Wall_R")
            {
                rightCheck = 1;
            }
            if (col.name == "Wall_B")
            {
                bottomCheck = 1;
                StopCoroutine(stayBottom);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BoxCollider2D col;
        PolygonCollider2D pol;
        
        if (collision.collider.GetComponent<BoxCollider2D>() != null)
        {
            col = collision.collider.GetComponent<BoxCollider2D>();
            //Debug.Log(col.tag);

            if (col.name == "Wall_L")
            {
                leftCheck = 0;
            }
            if (col.name == "Wall_R")
            {
                rightCheck = 0;
            }
            if (col.name == "Wall_B")
            {
                IsBottom();
            }
            if (col.tag == "Tetromino")
            {
                IsDrop(col);
            }
        } else if (collision.collider.GetComponent<PolygonCollider2D>() != null)
        {
            pol = collision.collider.GetComponent<PolygonCollider2D>();
            if (pol.tag == "Tetromino")
            {
                IsDrop(pol);
            }
        }
    }

    protected virtual bool IsDrop(Collider2D col)
    {
        //Debug.Log(col.name) ;
        return true;
    }




    private void InGridCheck()
    {
        pos = transform.position;

        if (pos.x < go.minX)
        {
            pos.x = go.minX;
        } else if (pos.x > go.maxX)
        {
            pos.x = go.maxX;
        }
        if (pos.y < go.minY)
        {
            pos.y = go.minY;
        }

        float f = (transform.position.x / blockSize);
        int i = Mathf.RoundToInt(f);
        pos.x = i * blockSize;
        transform.position = pos;
    }


    /**
     * true -> 충돌함, false -> 충돌안함
     */
    private HitMinMax IsHit(Vector3 end)
    {
        Vector3 start;

        RaycastHit2D hit = Physics2D.Linecast(transform.position, end, wall);

        myCol.enabled = false;

        HitMinMax hmm = new HitMinMax
        {
            top = Vector3.zero,
            bottom = Vector3.zero,
            left = Vector3.zero,
            right = Vector3.zero,
            localTop = Vector3.zero,
            localBottom = Vector3.zero,
            localLeft = Vector3.zero,
            localRight = Vector3.zero,
            tag = "",
            hit = false
        };

        //Debug.Log("들어온 end : " + end);

        for (int i = 0; i < myChildren.Length; i++)
        {
            start = myChildren[i].position;
            //Vector3 myend = end + tps[tpsIndex].childLocal[i];
            Vector3 myLocal = tps[tpsIndex].childLocal[i];
            Vector3 myend = end + myLocal;


            //Debug.Log("childcount : " + i);
            //Debug.Log("start" + start);
            //Debug.Log("end" + myend);
            //Debug.Log("myLocal" + myLocal);


            hit = Physics2D.Linecast(transform.position, transform.position, tetrominoLayer);
            

            hit = Physics2D.Linecast(start, myend, tetrominoLayer);

            if (hit)
            {
                //Debug.Log("부딪힌 포인트 블록 ; " + hit.transform.position);
                hmm = GetHitMinMax(hit, hmm, myLocal);
            }
            else
            {
                hit = Physics2D.Linecast(start, end, wall);
                if (hit)
                {
                    //Debug.Log("부딪힌 포인트 벽 ; " + hit.transform.position);
                    hmm = GetHitMinMax(hit, hmm, myLocal);
                }

            }
        }

        //Debug.Log("final max : " + hmm.max);
        //Debug.Log("final min : " + hmm.min);

        myCol.enabled = true;
        return hmm;
    }

    public struct HitMinMax
    {
        public Vector3 top;
        public Vector3 bottom;
        public Vector3 left;
        public Vector3 right;
        public bool hit;
        public Vector3 localTop;
        public Vector3 localBottom;
        public Vector3 localLeft;
        public Vector3 localRight;
        public string tag;
    }

    // 회전 체크
    public HitMinMax RotateCheck()
    {
        myCol.enabled = false;

        int f = tpsIndex + 1;   // 곧 회전하며 바뀔 좌표
        if (f >= tps.Length)
        {
            f = 0;
        }
        HitMinMax hmm = new HitMinMax
        {
            top = Vector3.zero,
            bottom = Vector3.zero,
            left = Vector3.zero,
            right = Vector3.zero,
            localTop = Vector3.zero,
            localBottom = Vector3.zero,
            localLeft = Vector3.zero,
            localRight = Vector3.zero,
            tag = "",
            hit = false
        };

        Vector3 move = transform.position;

        RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position, tetrominoLayer);

        Vector3 plusVec = Vector3.zero;

        for (int i = 0; i < myChildren.Length; i++)
         {
            Vector3 myLocal = tps[f].childLocal[i];
            Vector3 end = transform.position + myLocal;
            Vector3 start = transform.position;
            
            hit = Physics2D.Linecast(transform.position, transform.position, tetrominoLayer);

            

            //Debug.Log("childcount : " + i);
            //Debug.Log("start" + start);
            //Debug.Log("end" + end);




            hit = Physics2D.Linecast(start, end, tetrominoLayer);

            if (hit)
            {
                //Debug.Log("부딪힌 포인트 ; " + hit.transform.position);
                hmm = GetHitMinMax(hit, hmm, myLocal);
            }
            else
            {
                hit = Physics2D.Linecast(start, end, wall);
                if (hit)
                {
                    hmm = GetHitMinMax(hit, hmm, myLocal);
                }
                
            }
           
        }

        //Debug.Log("final min : " + hmm.min);
        //Debug.Log("final max : " + hmm.max);
        //Debug.Log("충돌 체크 : " + hmm.hit);

        if (move != transform.position)
        {
            //Debug.Log("move : " + move);
            if (move!=transform.position && !IsHit(move).hit)
            {
                transform.position = move;
            }
        }

        myCol.enabled = true;

        return hmm;
    }

    private HitMinMax GetHitMinMax(RaycastHit2D hit, HitMinMax hmm, Vector3 local)
    {
        //Debug.Log("local : " + local);
        Collider2D col = hit.collider.GetComponent<BoxCollider2D>();

        Vector3 v = hit.transform.position;

        hmm.tag = hit.transform.tag;

        

        if (col.name == "Wall_L")
        {
            v = new Vector3(go.minX - blockSize, transform.position.y, 0);
        }
        if (col.name == "Wall_R")
        {
            v = new Vector3(blockSize + go.maxX, transform.position.y, 0);
        }
        if (col.name == "Wall_B")
        {
            v = new Vector3(transform.position.x, blockSize * -1, 0);
        }

        hmm.hit = true;

        if (hmm.top == Vector3.zero)
        {
            hmm.top = v;
            hmm.bottom = v;
            hmm.left = v;
            hmm.right = v;
            hmm.localTop = local;
            hmm.localBottom = local;
            hmm.localLeft = local;
            hmm.localRight = local;
        }

        if (hmm.top.y <= v.y && local != hmm.localTop)
        {
            if (hmm.top.y != v.y)
            {
                hmm.top = v;
                hmm.localTop = local;
            } else if (hmm.localTop.y > local.y)
            {
                hmm.localTop = local;
            }
        }
        if (hmm.bottom.y >= v.y)
        {
            if (hmm.bottom.y != v.y)
            {
                hmm.bottom = v;
                hmm.localBottom = local;
            } else if(hmm.localBottom.y > local.y)
            {
                hmm.localBottom = local;
            }
        }
        if (hmm.left.x >= v.x)
        {
            if (hmm.left.x != v.x)
            {
                hmm.left = v;
                hmm.localLeft = local;
            } else if (hmm.localLeft.x > local.x)
            {
                hmm.localLeft = local;
            }
        }

        //Debug.Log("localRight : " + hmm.localRight); 
        if (hmm.right.x <= v.x)
        {
            if (hmm.right.x != v.x)
            {
                hmm.right = v;
                hmm.localRight = local;
            } else if (hmm.localRight.x < local.x)
            {
                hmm.localRight = local;
            }
        }

        //Debug.Log("!!!!!!!!!!!!!!!!!!!!!!v : " + v);
        //Debug.Log("!!!!!!!!!!!!!!!!!!!!!!local : " + local);

        return hmm;
    }

    private void IsBottom()
    {
        bottomCheck = 0;
        pos = transform.position;
        float f = (transform.position.y / blockSize);
        int i = Mathf.RoundToInt(f);

        pos.y = i * blockSize;
        transform.position = pos;
        if (canMove)
            stayBottom = StartCoroutine("StayBottom");
    }

    private void StopTetromino()
    {
        bottomCheck = 0;
        rightCheck = 0;
        leftCheck = 0;
        canMove = false;
        DestroyImmediate(ghost, true);
        Destroy(transform.parent.gameObject);

        for (int i = 0; i < myChildren.Length; i++)
        {
            myChildren[i].SetParent(null);
            GridManager.instance.FindMyParent(myChildren[i], i);
            myChildren[i].gameObject.GetComponent<BoxCollider2D>().enabled = true;
            myChildren[i].gameObject.GetComponent<Rigidbody2D>().WakeUp();
            myChildren[i].gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        rig.velocity = new Vector3(0,0,0); 
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}






