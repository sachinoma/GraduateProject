using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<ResultData> resultData;

    private PlayerConfigurationManager playerConfigurationManager;
    
    [SerializeField]
    private int allMenber = 0;

    [SerializeField]
    private int[] rank;
    private bool[] rankLock;

    private System.Action[] gameSceneCollect = new System.Action[3];

    int currentStage;

    private Mode mode { get; set; }
    public static GameManager Instance { get; private set; }

    public enum Mode
    {
        None,
        Main,
        Ring,
        Survive
    }
    private void Awake()
    {
        resultData = new List<ResultData>();
        if (Instance != null)
        {
            Debug.Log("trying to creat another singleton resultData!");
        }
        else
        {
            Instance = this;
        }

        //メインゲームの用意
        gameSceneCollect[0] = () => SceneManager.LoadScene("athletic");
        gameSceneCollect[1] = () => SceneManager.LoadScene("FallGame");
        gameSceneCollect[2] = () => SceneManager.LoadScene("stage2");
    }
    public List<ResultData> GetResultData()
    {
        Debug.Log("ResultData.Count:" + resultData.Count);
        return resultData;
    }

    void Start()
    {
        playerConfigurationManager = this.GetComponent<PlayerConfigurationManager>();
        mode = Mode.None;
    }

    void Update()
    {
        
    }

    public void SetRankAll(int[] data)
    {
        if(rank.Length != data.Length)
        {
            rank = data;
        }
        else
        {
            for(int i = 0; i < rank.Length; ++i)
            {
                rank[i] = data[i];
            }
        }
    }

    public void SetRankOne(int playerNum , int rankNum)
    {
        rank[rankNum] = playerNum;
        rankLock[playerNum] = true;
    }

    public int[] GetRankAll()
    {
        return rank;
    }

    public bool[] GetRankLockAll()
    {
        return rankLock;
    }
    public bool GetRankLock(int playerNum)
    {
        return rankLock[playerNum];
    }

    private void ResetRank()
    {
        for(int i = 0; i < rank.Length; ++i)
        {
            rank[i] = 0;
            rankLock[i] = false;
        }
    }

    public int GetRank(int playerNum) 
    {
        for(int i = 0; i < rank.Length; ++i)
        {
            if (rank[i] == playerNum)
            {
                return i;
            }
        }
        //例外
        return -1;
    }

    public void PrepareRank()
    {
        System.Array.Resize(ref rank, allMenber);
        System.Array.Resize(ref rankLock, allMenber);       
    }

    public int GetAllMenber()
    {
        return allMenber;
    }

    public void UpdateAllMenber(int num) 
    {
        allMenber = num;
        PrepareRank();
    }

    public void AddResultData(int num)
    {
        resultData.Add(new ResultData(num));
    }


    public void LoadToLobby()
    {
        playerConfigurationManager.SetPlayerInputManager(true);
        playerConfigurationManager.SetPlayerInputManagerJoinSetting(true);
        for (int i = 0; i < resultData.Count; i++)
        {
            Debug.Log(resultData[i].GetScoreTime().ToString());
            Debug.Log(resultData[i].GetFallNum().ToString());
        }
        SceneManager.LoadScene("Test_Lobby");
    }


    public void LoadToMain()
    {
        playerConfigurationManager.SetPlayerInputManager(false);
        ResetRank();
        for (int i = 0; i < resultData.Count; i++)
        {
            resultData[i].UpdateScore(0);
            resultData[i].ResetFallNum();
            resultData[i].ResetSurvivorTime();
            resultData[i].ResetRingNum();
        }

        GameObject obj = GameObject.Find("StageSelectButton");
        if (obj)
        {
            StageSelectButton button = obj.GetComponent<StageSelectButton>();
            currentStage = button.StageCount;
        }

        gameSceneCollect[currentStage]();
        //SceneManager.LoadScene("athletic");
        //SceneManager.LoadScene("stage2");
    }

    public void LoadToSoloMain()
    {
        playerConfigurationManager.SetPlayerInputManager(false);
        for (int i = 0; i < resultData.Count; i++)
        {
            resultData[i].UpdateScore(0);
            resultData[i].ResetFallNum();
        }
        SceneManager.LoadScene("OnePlayerGame");
    }

    public void LoadToResult()
    {
        SceneManager.LoadScene("Test_Result");
    }

    public void LoadToSoloResult()
    {
        SceneManager.LoadScene("Test_SoloResult");
    }

    public void LoadToTitle()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Title");
    }

    public Mode GetMode()
    {
        return mode;
    }

    public void SetMode(Mode modeTmp)
    {
        mode = modeTmp;
    }

}

public class ResultData
{
    public ResultData(int num)
    {
        playerNum = num;
        scoreTime = 0;
        fallNum = 0;

        ringNum = 0;
        survivorTime = 0;
    }



    public int playerNum { get; set; }
    public float scoreTime { get; set; }
    public int fallNum { get; set; }
    public int ringNum { get; set; }
    public float survivorTime { get; set; }


    public int GetPlayerNum()
    {
        return playerNum;
    }

    public float GetScoreTime()
    {
        return scoreTime;
    }

    public void UpdateScore(float time)
    {
        Debug.Log(time);
        scoreTime = time;
    }

    public bool CheckScoreIsZero()
    {
        return scoreTime == 0;
    }

    public int GetFallNum()
    {
        return fallNum;
    }

    public void PlusFallNum()
    {
        fallNum++;
    }

    public void ResetFallNum()
    {
        fallNum = 0;
    }

    public int GetRingNum()
    {
        return ringNum;
    }

    public void AddRingNum(int num)
    {
        ringNum += num;
    }

    public void ResetRingNum()
    {
        ringNum = 0;
    }

    public float GetSurvivorTime()
    {
        return survivorTime;
    }

    public void ResetSurvivorTime()
    {
        survivorTime = 0;
    }

    public void SetSurvivorTime(float num)
    {
        survivorTime = num;
    }
}