using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<ResultData> resultData;

    private PlayerConfigurationManager playerConfigurationManager;
    
    [SerializeField]
    private int allMenber = 0;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        resultData = new List<ResultData>();
        Instance = this;
    }
    public List<ResultData> GetResultData()
    {
        return resultData;
    }

    void Start()
    {
        playerConfigurationManager =this.GetComponent<PlayerConfigurationManager>();
    }

    void Update()
    {
        
    }

    public int GetAllMenber()
    {
        return allMenber;
    }

    public void UpdateAllMenber(int num) 
    {
        allMenber = num;
    }

    public void AddResultData(int num)
    {
        resultData.Add(new ResultData(num));
    }

  


    public void LoadToLobby()
    {
        playerConfigurationManager.SetPlayerInputManager(true);
        playerConfigurationManager.SetPlayerInputManagerJoinSetting(true);
        for(int i = 0; i < resultData.Count; i++)
        {
            Debug.Log(resultData[i].GetScoreTime().ToString());
            Debug.Log(resultData[i].GetFallNum().ToString());
        }
        SceneManager.LoadScene("Test_Lobby");
    }
}

public class ResultData
{
    public ResultData(int num)
    {
        playerNum = num;
        scoreTime = 0;
        fallNum = 0;
    }

    public int playerNum { get; set; }
    public float scoreTime { get; set; }
    public int fallNum { get; set; }

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
        scoreTime = time;
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
}