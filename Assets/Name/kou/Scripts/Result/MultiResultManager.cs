using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class MultiResultManager : MonoBehaviour
{
    private GameManager gameManager;
    
    [SerializeField]
    private ResultData[] resultData;

    [SerializeField]
    private int[] rank;
    [SerializeField]
    private float[] time;

    private int allMenber;
    private int maxMenber = 4;

    [SerializeField]
    Text[] rankPlayerText;
    [SerializeField]
    Text[] rankTimeText;
    [SerializeField]
    Text[] rankFallText;

    [SerializeField]
    Animator winAnimator;

    [SerializeField]
    RotationRod_UP pillar;

    void Start()
    {
        GameObject playerInputManager = GameObject.Find("PlayerInputManager");
        gameManager = playerInputManager.GetComponent<GameManager>();
        resultData = GameManager.Instance.GetResultData().ToArray();
        pillar.enabled = false;
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

        for(int i = allMenber; i < maxMenber; ++i)
        {
            rankPlayerText[i].text = "";
            rankTimeText[i].text = "";
            rankFallText[i].text = "";
        }
    }

    public void IntroFinish()
    {
        winAnimator.SetBool("Spinning", true);
        pillar.enabled = true;
    }

    public void ShowText()
    {
        
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
