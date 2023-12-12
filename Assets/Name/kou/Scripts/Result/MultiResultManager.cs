using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField] Image[] rankPlayerIcon;
    [SerializeField] TextMeshProUGUI[] minText;
    [SerializeField] TextMeshProUGUI[] secText;
    [SerializeField] TextMeshProUGUI[] digText;
    [SerializeField] GameObject[] minsecUI;
    [SerializeField] TextMeshProUGUI[] fallText;

    [SerializeField] Sprite[] playerSprite;

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
            rankPlayerIcon[i].sprite = playerSprite[resultData[rank[i]].GetPlayerNum() - 1];
            timeProcess(i);
            fallProcess(i);           
        }

        for(int i = allMenber; i < maxMenber; ++i)
        {
            rankPlayerIcon[i].gameObject.SetActive(false);
            minText[i].SetText("");
            secText[i].SetText("");
            digText[i].SetText("");
            fallText[i].SetText("");
            minsecUI[i].SetActive(false);          
        }
    }

    private void timeProcess(int num)
    {
        int timeInt = (int)resultData[rank[num]].GetScoreTime();
        int minute = timeInt/ 60;
        int second = timeInt - minute;
        float decimalPoint = resultData[rank[num]].GetScoreTime() - timeInt;

        minText[num].SetText(ConvFoolCoolFont(minute.ToString().PadLeft(2, '0')));
        secText[num].SetText(ConvFoolCoolFont(second.ToString().PadLeft(2, '0')));
        digText[num].SetText(ConvFoolCoolFont(decimalPoint.ToString().Substring(2, 2)));
    }

    private void fallProcess(int num)
    {
        fallText[num].SetText(ConvFoolCoolFont(resultData[rank[num]].GetFallNum().ToString()));
    }

    public static string ConvFoolCoolFont(string str)
    {
        string rtnStr = "";
        if (str == null)
            return rtnStr;
        for (int i = 0; i < str.Length; i++)
        {
            string convStr;
            switch (str[i])
            {
                case 's':
                    convStr = "10";
                    break;
                case 'e':
                    convStr = "11";
                    break;
                case 'c':
                    convStr = "12";
                    break;
                case 'm':
                    convStr = "13";
                    break;
                case 'i':
                    convStr = "14";
                    break;
                case 'n':
                    convStr = "15";
                    break;
                default:
                    convStr = str[i].ToString();
                    break;
            }
            rtnStr += "<sprite=" + convStr + ">";
        }
        return rtnStr;
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
