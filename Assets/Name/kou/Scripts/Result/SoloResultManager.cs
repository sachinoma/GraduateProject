using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoloResultManager : MonoBehaviour
{
    private GameManager gameManager;
    
    [SerializeField]
    private ResultData[] resultData;


    [SerializeField] TextMeshProUGUI minText;
    [SerializeField] TextMeshProUGUI secText;
    [SerializeField] TextMeshProUGUI digText;

    void Start()
    {
        GameObject playerInputManager = GameObject.Find("PlayerInputManager");
        gameManager = playerInputManager.GetComponent<GameManager>();
        resultData = GameManager.Instance.GetResultData().ToArray();

        timeProcess();
    }


    private void timeProcess()
    {
        int timeInt = (int)resultData[0].GetScoreTime();
        int minute = timeInt / 60;
        int second = timeInt - minute;
        float decimalPoint = resultData[0].GetScoreTime() - timeInt;

        minText.SetText(ConvFoolCoolFont(minute.ToString().PadLeft(2, '0')));
        secText.SetText(ConvFoolCoolFont(second.ToString().PadLeft(2, '0')));
        digText.SetText(ConvFoolCoolFont(decimalPoint.ToString().Substring(2, 2)));
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

    public void LoadToLobby()
    {
        gameManager.LoadToLobby();
    }

    public void LoadToMain()
    {
        gameManager.LoadToSoloMain();
    }

    public void LoadToTitle()
    {
        gameManager.LoadToTitle();
    }
}
