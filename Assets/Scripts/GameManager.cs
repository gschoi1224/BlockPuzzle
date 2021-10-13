using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using DataInfo;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameObject[] tetrominos = new GameObject[7]; //테트로미노 오브젝트 배열
    public GameObject[] nexts = new GameObject[7];
    public GameObject[] mini_nexts = new GameObject[7];
    public GameObject[] holds = new GameObject[8];
    public float gameSpeed = 5f;
    public int level = 1;
    private int score = 1990;
    public GameObject txt_score;
    public GameObject txt_level;
    public GameObject img_level;
    private GameObject holdBlock;
    public GameData gameData;
    public int[,] saveMap = new int[20, 10];
    public GameObject btn_start;

    public GameObject btn_play;
    public GameObject btn_pause;
    public GameObject pn_option;

    public GameObject pn_gameOver;
    public GameObject txt_final_level;
    public GameObject txt_final_score;
    public Animator final_animator_level;
    public Animator final_animator_score;

    private GameObject tetromino;
    private GameObject nextBlock;
    private GameObject afterNextBlock;
    private Vector3 topVec;
    private int next = -1;
    private int nextnext = -1;
    private int hold = -1;
    private int now = -1;
    private bool isHold;
    private bool isFirst = true;
    private bool gameOver = false;

    private DataManager dataManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != null)
        {
            Destroy(this);
        }

        //Advertisement.Initialize("2809796", false);
        //DataManager를 추출해 저장
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        //DataManager 초기화
        dataManager.Initialize();
    }
    // Use this for initialization
    void Start() {
        //게임의 초기 데이터 로드
        LoadGameData();
    }

    public void GetStartBtn()
    {
        if (now == -1 || next == -1 || nextnext == -1)
            GetNext();
        MakeTetromino();

        SetTexts(0);
        if (hold != -1)
        {
            Destroy(holdBlock);
            holdBlock = Instantiate(holds[hold]) as GameObject;
        }
        isFirst = false;
        if (hold == -1)
            holdBlock = Instantiate(holds[7]) as GameObject;
        txt_level.SetActive(true);
        txt_score.SetActive(true);
        img_level.SetActive(true);
        btn_start.SetActive(false);
        btn_pause.SetActive(true);
        BgmManager.instance.GetStart();
    }

    void LoadGameData()
    {
        //dataManager를 통해 파일에 저장된 데이터 불러오기
        GameData data = dataManager.Load();

        gameData.level = data.level;
        gameData.next = data.next;
        gameData.nextnext = data.nextnext;
        gameData.now = data.now;
        gameData.score = data.score;
        gameData.hold = data.hold;
        gameData.saveMap = data.saveMap;
        gameData.optionData = data.optionData;

        level = data.level;
        score = data.score;
        now = data.now;
        next = data.next;
        nextnext = data.nextnext;
        hold = data.hold;
        saveMap = data.saveMap;

        OptionManager.instance.LoadOptionData(data.optionData);

        txt_score.GetComponent<TextMeshPro>().text = score.ToString();
        txt_level.GetComponent<TextMeshProUGUI>().text = level.ToString();
    }

    void SaveGameData()
    {
        gameData.level = level;
        gameData.score = score;
        gameData.now = now;
        gameData.next = next;
        gameData.nextnext = nextnext;
        gameData.hold = hold;
        gameData.saveMap = saveMap;
        gameData.optionData = OptionManager.instance.SaveOptionData();
        dataManager.Save(gameData);
    }

    public void SaveOption(OptionData data)
    {
        gameData.optionData = data;
        dataManager.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        if (score != 0)
        {
            GridManager.instance.CheckChild();
            SaveGameData();
        }
    }
    // Update is called once per frame
    void Update() {
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Destroy(tetromino);
            now = 0;
            tetromino = Instantiate(tetrominos[now], topVec, Quaternion.identity) as GameObject;
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Destroy(tetromino);
            now = 1;
            tetromino = Instantiate(tetrominos[now], topVec, Quaternion.identity) as GameObject;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Destroy(tetromino);
            now = 2;
            tetromino = Instantiate(tetrominos[now], topVec, Quaternion.identity) as GameObject;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Destroy(tetromino);
            now = 3;
            tetromino = Instantiate(tetrominos[now], topVec, Quaternion.identity) as GameObject;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Destroy(tetromino);
            now = 4;
            tetromino = Instantiate(tetrominos[now], topVec, Quaternion.identity) as GameObject;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Destroy(tetromino);
            now = 5;
            tetromino = Instantiate(tetrominos[now], topVec, Quaternion.identity) as GameObject;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Destroy(tetromino);
            now = 6;
            tetromino = Instantiate(tetrominos[now], topVec, Quaternion.identity) as GameObject;
        }
    }

    private void FixedUpdate()
    {
        if (tetromino == null && !isFirst && !gameOver)
        {
            GetNext();
            MakeTetromino();
        }
    }

    private void GetNext()
    {
        if (next == -1)
            now = Random.Range(0, tetrominos.Length);

        else
        {
            now = next;
            Destroy(nextBlock);
        }

        if (nextnext == -1)
            next = Random.Range(0, tetrominos.Length);
        else
        {
            next = nextnext;
            Destroy(afterNextBlock);
        }

        nextnext = Random.Range(0, tetrominos.Length);

        //Debug.Log(next == nextnext);
        //Debug.Log(now == nextnext);
        //Debug.Log(!(now == nextnext && next == nextnext));

        while (next == nextnext)
        {
            nextnext = Random.Range(0, tetrominos.Length);
        }
    }

    private void MakeTetromino()
    {
        topVec = new Vector3(0, 0, 0);
        tetromino = Instantiate(tetrominos[now], topVec, Quaternion.identity) as GameObject;
        nextBlock = Instantiate(nexts[next]) as GameObject;
        afterNextBlock = Instantiate(mini_nexts[nextnext]) as GameObject;

        isHold = false;
    }

    public void GetHold()
    {
        if (!isHold)
        {
            Destroy(holdBlock);
            holdBlock = Instantiate(holds[now]) as GameObject;
            Destroy(tetromino);
            if (hold == -1)
            {
                hold = now;
                GetNext();
                MakeTetromino();
            }
            else
            { 
                tetromino = Instantiate(tetrominos[hold], topVec, Quaternion.identity) as GameObject;
                int imsi = now;
                now = hold;
                hold = imsi;
            }
            isHold = true;
            SoundManager.instance.PlayHold();
        }
    }

    private bool isStarted = false;
    private Coroutine rowPlus;
    public void SetTexts(int plusScore)
    {
        score += plusScore;
        txt_score.GetComponent<TextMeshPro>().text = score.ToString();
        if (Mathf.RoundToInt(score / 200) + 1 != level)
        {
            level = Mathf.RoundToInt(score / 200) + 1;
            BgmManager.instance.UpdateLevel(level);
            if (level >= 10)
            {
                for (int i = 0; i < level/10; i++)
                {
                    GridManager.instance.OneLowPlus();
                }
            }
        }
        txt_level.GetComponent<TextMeshProUGUI>().text = level.ToString();
        if (!isStarted && level >= 10)
        {
            isStarted = true;
            rowPlus = StartCoroutine("GetOneRow");
        }
    }

    public void OnPauseClick()
    {
        tetromino.transform.GetChild(0).GetComponent<MonoBehaviour>().enabled = false;
        BgmManager.instance.audioSource.Pause();
        btn_play.SetActive(true);
        btn_pause.SetActive(false);
        //Advertisement.Show();
        pn_option.SetActive(true);
        if (rowPlus != null)
            StopCoroutine(rowPlus);
    }

    public void OnPlayClick()
    {
        tetromino.transform.GetChild(0).GetComponent<MonoBehaviour>().enabled = true;
        BgmManager.instance.audioSource.Play();
        btn_play.SetActive(false);
        btn_pause.SetActive(true);
        pn_option.SetActive(false);
        if (rowPlus != null)
            rowPlus = StartCoroutine("GetOneRow");
    }


    public void GoBottom()
    {
        Tetromino.t.GoBottom();
    }

    public void GoRight()
    {
        Tetromino.t.GoRight();
    }
    
    public void GoLeft()
    {
        Tetromino.t.GoLeft();
    }

    public void GoRotate()
    {
        Tetromino.t.RotateTetromino();
    }

    public void GoDrop()
    {
        Tetromino.t.GoDrop();
    }


    public IEnumerator GetOneRow()
    {
        while(true)
        {
            float time = 150f / level;
            if (time <= 2f)
            {
                time = 2f;
            }
            yield return new WaitForSeconds(time);
            GridManager.instance.OneLowPlus();
        }
    }

    public void GameOver() {
        Debug.Log("GameOver");
        gameOver = true;
        pn_gameOver.SetActive(true);
        OptionManager.instance.OnPauseClick();
        StopCoroutine(rowPlus);
        StartCoroutine("StartScore");
        StartCoroutine("StartLevel");
    }

    private IEnumerator StartScore()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i <= score; i+=10)
        {
            yield return new WaitForSeconds(0.01f);
            txt_final_score.GetComponent<TextMeshProUGUI>().text = i.ToString();
            if (score == i)
            {
                yield return new WaitForSeconds(0.01f);
                final_animator_score.Play("finalTxtHighlight");
            }
        }
    }

    private IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 1; i <= level; i++)
        {
            yield return new WaitForSeconds(0.25f);
            txt_final_level.GetComponent<TextMeshProUGUI>().text = i.ToString();
            if (level == i)
            {
                yield return new WaitForSeconds(0.01f);
                final_animator_level.Play("finalTxtHighlight");
            }
        }
    }
}
