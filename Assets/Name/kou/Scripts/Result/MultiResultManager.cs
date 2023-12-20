using System;
using System.Drawing;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class MultiResultManager : MonoBehaviour
{
    private GameManager gameManager;
    
    [SerializeField]
    private ResultData[] resultData;
    [SerializeField]
    private GameObject nameObj;
    [SerializeField]
    private GameObject timeObj;
    [SerializeField]
    private GameObject numObj;

    [SerializeField]
    private int[] rank;
    [SerializeField]
    private float[] time;

    [SerializeField]
    private int[] ring;

    private int allMenber;
    private int maxMenber = 4;


    [SerializeField] Image numTitle;
    [SerializeField] Sprite ringSprite;
    [SerializeField] Image[] rankPlayerIcon;
    [SerializeField] TextMeshProUGUI[] minText;
    [SerializeField] TextMeshProUGUI[] secText;
    [SerializeField] TextMeshProUGUI[] digText;
    [SerializeField] GameObject[] minsecUI;
    [SerializeField] TextMeshProUGUI[] numText;

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
        switch (gameManager.GetMode())
        {
            case GameManager.Mode.Main:
                RankProcessMain();
                break;
            case GameManager.Mode.Ring:
                RankProcessRing();
                break;
            case GameManager.Mode.Survive:
                RankProcessFallGame();
                break;
            default:
                Assert.IsTrue(false, "modeはNoneになってます！");
                break;
        }
        
        SpawnWinner();
    }

    private void timeProcess(int num)
    {
        int timeInt = (int)resultData[rank[num]].GetScoreTime();
        int minute = timeInt/ 60;
        int second = timeInt% 60;
        float decimalPoint = resultData[rank[num]].GetScoreTime() - timeInt;

        minText[num].SetText(ConvFoolCoolFont(minute.ToString().PadLeft(2, '0')));
        secText[num].SetText(ConvFoolCoolFont(second.ToString().PadLeft(2, '0')));
        digText[num].SetText(ConvFoolCoolFont(decimalPoint.ToString().Substring(2, 2)));
    }

    private void RankProcessMain()
    {
        allMenber = gameManager.GetAllMenber();
        rank = Enumerable.Repeat<int>(-1, allMenber).ToArray();

        time = new float[allMenber];


        for (int i = 0; i < allMenber; ++i)
        {
            time[i] = resultData[i].scoreTime;
        }

        Array.Sort(time);

        for (int i = 0; i < allMenber; ++i)
        {
            for (int j = 0; j < allMenber; ++j)
            {
                if (resultData[j].scoreTime == time[i])
                {
                    if(rank[i] == -1)
                    {
                        rank[i] = j;
                    }                  
                }
            }
        }

        for (int i = 0; i < allMenber; ++i)
        {
            rankPlayerIcon[i].sprite = playerSprite[resultData[rank[i]].GetPlayerNum() - 1];
            timeProcess(i);
            fallNumProcess(i);
        }

        for (int i = allMenber; i < maxMenber; ++i)
        {
            rankPlayerIcon[i].gameObject.SetActive(false);
            minText[i].SetText("");
            secText[i].SetText("");
            digText[i].SetText("");
            numText[i].SetText("");
            minsecUI[i].SetActive(false);
        }
    }

    private void fallNumProcess(int num)
    {
        Debug.Log("落ちた回数：" + resultData[rank[num]].GetFallNum());
        numText[num].SetText(ConvFoolCoolFont(resultData[rank[num]].GetFallNum().ToString()));
    }

    private void RankProcessFallGame()
    {
        allMenber = gameManager.GetAllMenber();
        rank = new int[allMenber];
        time = new float[allMenber];

        for (int i = 0; i < allMenber; ++i)
        {
            time[i] = resultData[i].GetSurvivorTime();
        }

        Array.Sort(time);

        for (int i = 0; i < allMenber; ++i)
        {
            for (int j = 0; j < allMenber; ++j)
            {
                if (resultData[j].GetSurvivorTime() == time[i])
                {
                    rank[allMenber - 1 - i] = j;
                }
            }
        }

        for (int i = 0; i < allMenber; ++i)
        {
            rankPlayerIcon[i].sprite = playerSprite[resultData[rank[i]].GetPlayerNum() - 1];
            surviveTimeProcess(i);
        }

        for (int i = allMenber; i < maxMenber; ++i)
        {
            rankPlayerIcon[i].gameObject.SetActive(false);
            minText[i].SetText("");
            secText[i].SetText("");
            digText[i].SetText("");
            minsecUI[i].SetActive(false);
        }
        //定数を表示するUIは必要ない
        numObj.SetActive(false);
        //UIの位置調整
        nameObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-190,380, 0);
        timeObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(140, 380, 0);
    }

    private void surviveTimeProcess(int num)
    {
        int timeInt = (int)resultData[rank[num]].GetSurvivorTime();
        int minute = timeInt / 60;
        int second = timeInt % 60;
        float decimalPoint = resultData[rank[num]].GetSurvivorTime() - timeInt;

        minText[num].SetText(ConvFoolCoolFont(minute.ToString().PadLeft(2, '0')));
        secText[num].SetText(ConvFoolCoolFont(second.ToString().PadLeft(2, '0')));
        digText[num].SetText(ConvFoolCoolFont(decimalPoint.ToString().Substring(2, 2)));
    }

    private void RankProcessRing()
    {
        allMenber = gameManager.GetAllMenber();
        rank = new int[allMenber];
        ring = new int[allMenber];

        for (int i = 0; i < allMenber; ++i)
        {
            ring[i] = resultData[i].GetRingNum();
        }

        Array.Sort(ring);

        for (int i = 0; i < allMenber; ++i)
        {
            for (int j = 0; j < allMenber; ++j)
            {
                if (resultData[j].GetRingNum() == ring[i])
                {
                    rank[allMenber - 1 - i] = j;
                }
            }
        }

        for (int i = 0; i < allMenber; ++i)
        {
            rankPlayerIcon[i].sprite = playerSprite[resultData[rank[i]].GetPlayerNum() - 1];
            ringProcess(i);
            minText[i].SetText("");
            secText[i].SetText("");
            digText[i].SetText("");
            minsecUI[i].SetActive(false);
        }

        for (int i = allMenber; i < maxMenber; ++i)
        {
            rankPlayerIcon[i].gameObject.SetActive(false);
            minText[i].SetText("");
            secText[i].SetText("");
            digText[i].SetText("");
            numText[i].SetText("");
            minsecUI[i].SetActive(false);
        }
        //時間を表示するUIは必要ない
        timeObj.SetActive(false);
        //項目名をRingに
        numTitle.sprite = ringSprite;
        //UIの位置調整
        nameObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-190, 380, 0);
        numObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(140, 380, 0);
    }

    private void ringProcess(int num)
    {
        numText[num].SetText(ConvFoolCoolFont(resultData[rank[num]].GetRingNum().ToString()));
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
                case '.':
                    convStr = "16";
                    break;
                case ' ':
                    rtnStr += "<space=0.4em>";
                    continue;
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
