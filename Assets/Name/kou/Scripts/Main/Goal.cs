using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Goal : MonoBehaviour
{
    public enum GoalType
    {
        Athletic,
        FallGame,
    }
    [SerializeField]
    private GoalType type;

    [SerializeField]
    private SoundEffect soundEffect;
    [SerializeField]
    private AudioClip clip;

    private GameManager gameManager;
    private float time;

    private int rankNow = 0;

    [SerializeField]
    private ResultData[] resultData;

    private bool isGoalProcessCalled = false;

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
        
        if (other.CompareTag("Player"))
        {
            PlayerStatus playerStatus = other.gameObject.GetComponent<PlayerStatus>();
            int allNum = gameManager.GetAllMenber();
            int playerNum = playerStatus.GetPlayerNum();
                        
            if(resultData[playerNum].CheckScoreIsZero())
            {
                if(type == GoalType.Athletic)
                {
                    soundEffect.PlaySoundEffectClip(clip);
                    resultData[playerNum].UpdateScore(time);
                    gameManager.SetRankOne(playerNum, rankNow);  
                    rankNow++;

                    if (CheckCanLoad(allNum))
                    {
                        if(!isGoalProcessCalled)
                        {
                            isGoalProcessCalled = true;
                            // コルーチンの起動
                            StartCoroutine(DelayCoroutine(1, () =>
                            {
                                // 1秒後にここの処理が実行される
                                gameManager.SetMode(GameManager.Mode.Main);
                                LoadToResult();
                            }));
                        }
                    }
                }
                else if(type == GoalType.FallGame)
                {
                    soundEffect.PlaySoundEffectClip(clip);
                    resultData[playerNum].SetSurvivorTime(time);

                    other.gameObject.SetActive(false);

                    if (CheckCanLoad(allNum))
                    {
                        if (!isGoalProcessCalled)
                        {
                            isGoalProcessCalled = true;
                            // コルーチンの起動
                            StartCoroutine(DelayCoroutine(1, () =>
                            {
                                // 1秒後にここの処理が実行される
                                gameManager.SetMode(GameManager.Mode.Survive);
                                LoadToResult();
                            }));
                        }
                    }
                }
               
            }
        }
    }

    private bool CheckCanLoad(int allNum)
    {
        for (int i = 0; i < allNum; ++i)
        {
            if (type == GoalType.Athletic)
            {
                if (resultData[i].GetScoreTime() == 0) { return false; }
            }
            else if (type == GoalType.FallGame)
            {
                if (resultData[i].GetSurvivorTime() == 0) { return false; }
            }
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

    // 一定時間後に処理を呼び出すコルーチン
    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }

}
