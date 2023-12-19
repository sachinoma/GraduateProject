using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] Text text;

    private GameManager gameManager;

    [SerializeField]
    private ResultData[] resultData;
    private void Start()
    { 
        
    }
    void Update()
    {
        time -= Time.deltaTime;
        text.text = time.ToString("f1") + "s";
        timeText.SetText(ConvFoolCoolFont(time.ToString("f1"))+ "s");
        
        if (time < 0)
        {
            gameManager.SetMode(GameManager.Mode.Ring);
            LoadToResult();
        }
    }
    private void LoadToResult()
    {
        gameManager.LoadToResult();
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
}
