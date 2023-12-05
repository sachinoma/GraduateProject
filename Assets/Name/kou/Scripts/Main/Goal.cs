using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private SoundEffect soundEffect;
    [SerializeField]
    private AudioClip clip;

    private GameManager gameManager;
    private float time;

    private int rankNow = 0;

    [SerializeField]
    private ResultData[] resultData;

    private bool[] clearFlag = {false, false, false, false};

    private void Start()
    {
        GameObject playerInputManager = GameObject.Find("PlayerInputManager");
        gameManager = playerInputManager.GetComponent<GameManager>();
        resultData = GameManager.Instance.GetResultData().ToArray();
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
            PlayerStatus playerStatus = other.gameObject.GetComponent<PlayerStatus>();
            int allNum = gameManager.GetAllMenber();
            int playerNum = playerStatus.GetPlayerNum();
                        
            if(resultData[playerNum].CheckScoreIsZero())
            {
                soundEffect.PlaySoundEffectClip(clip);
                resultData[playerNum].UpdateScore(time);
                gameManager.SetRankOne(playerNum, rankNow);
                rankNow++;

                if (CheckCanLoad(allNum))
                {
                    LoadToResult();
                }
            }
        }
    }

    private bool CheckCanLoad(int allNum)
    {
        for (int i = 0; i < allNum; ++i)
        {
            if (resultData[i].GetScoreTime() == 0) { return false; }
        }
        return true;
    }


    private void LoadToLobby()
    {
        gameManager.LoadToLobby();
    }

    private void LoadToResult()
    {
        gameManager.LoadToResult();
    }

}
