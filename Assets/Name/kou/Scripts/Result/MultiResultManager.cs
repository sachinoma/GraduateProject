using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiResultManager : MonoBehaviour
{
    private GameManager gameManager;
    
    [SerializeField]
    private ResultData[] resultData;

    [SerializeField]
    private int[] rank;
    //[SerializeField]
    //private int[] player;
    [SerializeField]
    private float[] time;
    //[SerializeField]
    //private int[] fallNum;

    private int allMenber;

    [SerializeField]
    Text[] rankPlayerText;
    [SerializeField]
    Text[] rankTimeText;
    [SerializeField]
    Text[] rankFallText;

    void Start()
    {
        GameObject playerInputManager = GameObject.Find("PlayerInputManager");
        gameManager = playerInputManager.GetComponent<GameManager>();
        resultData = GameManager.Instance.GetResultData().ToArray();

        RankProcess();
    }

    void Update()
    {
        
    }

    private void RankProcess()
    {
        allMenber = gameManager.GetAllMenber();
        rank = new int[allMenber];
        //player = new int[allMenber];
        time = new float[allMenber];
        //fallNum = new int[allMenber];

        for (int i = 0; i < allMenber; ++i) 
        {
            time[i] = resultData[i].scoreTime;
        }

        Array.Sort(time);

        for (int i = 0; i < allMenber; ++i)
        {
            for(int j = 0; j < allMenber; ++j)
            {
                if (resultData[j].scoreTime == time[i])
                {
                    rank[i] = j;
                }
            }
        }

        for (int i = 0; i < allMenber; ++i)
        {
            rankPlayerText[i].text = "Player" + resultData[rank[i]].GetPlayerNum().ToString();
            rankTimeText[i].text = resultData[rank[i]].GetScoreTime().ToString();
            rankFallText[i].text = resultData[rank[i]].GetFallNum().ToString();
        }
    }

    public void LoadToLobby()
    {
        gameManager.LoadToLobby();
    }

    public void LoadToMain()
    {
        gameManager.LoadToMain();
    }

    public void LoadToTitle()
    {
        gameManager.LoadToTitle();
    }
}
