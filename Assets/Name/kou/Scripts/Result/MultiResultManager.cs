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

    private float[] time;

    private int allMenber;

    [SerializeField]
    Text[] rankText;

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
        time = new float[allMenber];

        for (int i = 0; i < allMenber; i++) 
        {
            time[i] = resultData[i].scoreTime;
        }

        Array.Sort(time);

        for (int i = 0; i < allMenber; i++)
        {
            if (time[i] == resultData[i].scoreTime)
            {
                rank[i] = resultData[i].GetPlayerNum();
            }
        }

        for (int i = 0; i < allMenber; i++)
        {
            rankText[i].text = "Player" + rank[i].ToString();
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
