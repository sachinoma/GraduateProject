using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoloResultManager : MonoBehaviour
{
    private GameManager gameManager;
    
    [SerializeField]
    private ResultData[] resultData;

    [SerializeField]
    Text rankPlayerText;
    [SerializeField]
    Text rankTimeText;

    void Start()
    {
        GameObject playerInputManager = GameObject.Find("PlayerInputManager");
        gameManager = playerInputManager.GetComponent<GameManager>();
        resultData = GameManager.Instance.GetResultData().ToArray();

        RankProcess();
    }

    private void RankProcess()
    {
        rankPlayerText.text = "Player" + resultData[0].GetPlayerNum().ToString();
        rankTimeText.text = resultData[0].GetScoreTime().ToString();
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
