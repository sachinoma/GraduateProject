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
    [SerializeField]
    private float[] time;

    private int allMenber;
    private int maxMenber = 4;

    [SerializeField] Text[] rankPlayerText;
    [SerializeField] Text[] rankTimeText;
    [SerializeField] Text[] rankFallText;
    [SerializeField] GameObject UiMain;

    [SerializeField] Animator winAnimator;
    [SerializeField] RotationRod_UP pillar;
    [SerializeField] GameObject winnerPrefab;
    [SerializeField] GameObject loserPrefab;
    [SerializeField] Transform winnerSpawnPos;
    [SerializeField] Transform[] loserSpawnPos;

    void Start()
    {
        GameObject playerInputManager = GameObject.Find("PlayerInputManager");
        gameManager = playerInputManager.GetComponent<GameManager>();
        resultData = GameManager.Instance.GetResultData().ToArray();
        pillar.enabled = false;
        UiMain.SetActive(false);
        RankProcess();
        SpawnWinner();
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
        SpawnLoser();

        winAnimator.SetBool("Spinning", true);
        pillar.enabled = true;
    }

    public void ShowText()
    {
        UiMain.SetActive(true);
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

    private void SpawnWinner()
    {
        GameObject winner = Instantiate(winnerPrefab, winnerSpawnPos.position, winnerSpawnPos.rotation);
        ResultCharacter resultCharacter = winner.GetComponent<ResultCharacter>();
        resultCharacter.SetPlayerNum(rank[0]);
        resultCharacter.ChangeOutfit();
        winAnimator = winner.GetComponent<Animator>();
    }

    private void SpawnLoser()
    {
        for(int i = 0; i < allMenber - 1; ++i) //winnerを引いてマイナス1
        {
            GameObject loser = Instantiate(loserPrefab, loserSpawnPos[i].position, loserSpawnPos[i].rotation);
            ResultCharacter resultCharacter = loser.GetComponent<ResultCharacter>();
            resultCharacter.SetPlayerNum(rank[i + 1]); //二位からなので+1
            resultCharacter.ChangeOutfit();
        }        
    }

}
