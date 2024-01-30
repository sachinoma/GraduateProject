using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MainPlayerManager : MonoBehaviour
{
    //シングルトン取得
    private GameManager gameManager;
    [SerializeField]
    private ResultData[] resultData;

    //プレイヤーのスポーン地点
    [SerializeField]
    private Transform[] playerSpawns;
    //プレイヤーPrefab
    [SerializeField]
    private GameObject[] playerPrefab;

    private int playerNum;

    [SerializeField]
    private PlayerConfiguration[] playerConfigs;

    [SerializeField]
    private GameMessageReceiver[] playerReceiver;

    [SerializeField]
    private Transform goal;

    [SerializeField]
    private Transform[] playerAvatar;
    //現在の距離（ソートかける）
    private float[] distance;
    //現在の順位
    [SerializeField]
    private int[] rankTmp;
    private int[] rankBefore;
    private int[] rankOriData;
    private bool[] rankLock;

    [SerializeField]
    private GameObject[] uiCanvasByPlayerNum;

    //画面分割の設定
    [SerializeField]
    private Rect[][] cameraRect = new Rect[4][]
    {
        new Rect[]{ new Rect(0.0f, 0.0f, 1.0f, 1.0f) },
        new Rect[]{ new Rect(0.0f, 0.0f, 0.5f, 1.0f) , new Rect(0.5f, 0.0f, 0.5f, 1.0f) },
        new Rect[]{ new Rect(0.0f, 0.5f, 0.5f, 0.5f) , new Rect(0.5f, 0.5f, 0.5f, 0.5f) , new Rect(0.25f, 0.0f, 0.5f, 0.5f) },
        new Rect[]{ new Rect(0.0f, 0.5f, 0.5f, 0.5f) , new Rect(0.5f, 0.5f, 0.5f, 0.5f) , new Rect(0.0f, 0.0f, 0.5f, 0.5f) , new Rect(0.5f, 0.0f, 0.5f, 0.5f) }
    };
        
        
        //{ new Rect(0.0f, 0.0f, 1.0f, 1.0f), new Rect(0.0f, 0.0f, 1.0f, 1.0f) }; 

    void Start()
    {
        GameObject playerInputManager = GameObject.Find("PlayerInputManager");
        gameManager = playerInputManager.GetComponent<GameManager>();
        resultData = GameManager.Instance.GetResultData().ToArray();

        //playerConfigsを基にプレイヤーを配置
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        playerNum = playerConfigs.Length;

        playerAvatar = new Transform[playerNum];
        distance = new float[playerNum];
        rankTmp = new int[playerNum];
        rankBefore = new int[playerNum];
        rankOriData = new int[playerNum];
        rankLock = new bool[playerNum];
        playerReceiver = new GameMessageReceiver[playerNum];

        int rankNum = 0;
        for (int i = 0; i < playerNum; i++)
        {
            Debug.Log(i);
            var player = Instantiate(playerPrefab[0], playerSpawns[i].position, playerSpawns[i].rotation);
                
            //画面分割
            player.GetComponent<PlayerCameraLayerUpdater>().SetPlayerNum(i);
            player.GetComponentInChildren<Camera>().rect = cameraRect[playerNum - 1][i];

            player.transform.Find("Avatar").gameObject.GetComponent<InputReceiver>().SetTargetNum(i);
            playerReceiver[i] = player.GetComponentInChildren<GameMessageReceiver>();
            //プレイヤーのavatar（現在位置取得用））
            playerAvatar[i] = player.transform.Find("Avatar");
            //rankTmpの初期化
            rankTmp[i] = rankNum;
            rankBefore[i] = 0;
            rankNum++;
        }
        //プレイヤー人数に対応するuiを表示
        uiCanvasByPlayerNum[playerNum - 1].SetActive(true);
    }

    private void Update()
    {
        CheckNowRank();
        if(IsRankDifferent())
        {
            gameManager.SetRankAll(rankTmp);
        }
    }

    private bool IsRankDifferent()
    {
        for(int i = 0; i < rankTmp.Length; i++)
        {
            if (rankTmp[i] != rankBefore[i])  {return true;}
        }
        return false;
    }

    private void CheckNowRank()
    {
        GetOriginalRank();
        CheckByDistance();
    }

    public void GetOriginalRank()
    {
        rankOriData = gameManager.GetRankAll();
        rankLock = gameManager.GetRankLockAll();
    }


    public void CheckByDistance()
    {
        int lockNum = 0;
        rankTmp = rankOriData;

        if(goal != null)
        {
            for (int i = 0; i < playerNum; ++i)
            {
                if (rankLock[i] == true)
                {
                    //ゴールした人を排除するために大きい値にします。
                    distance[i] = 10000000;
                    lockNum++;
                }
                else
                {
                    distance[i] = Vector3.Distance(goal.position, playerAvatar[i].position);
                }
            }
            //距離をソートかけます
            Array.Sort(distance);

            //ソートした距離によりランク決めます
            for (int i = 0; i < playerNum; ++i)
            {
                for (int j = 0; j < playerNum; ++j)
                {
                    if (Vector3.Distance(goal.position, playerAvatar[j].position) == distance[i])
                    {
                        rankTmp[i + lockNum] = j;
                        //gameManager.SetRankOne(i + lockNum,j);
                    }
                }
            }
        }
    }

    public GameMessageReceiver[] GetOtherReceiver(int num)
    {
        GameMessageReceiver[] receiver = new GameMessageReceiver[playerNum - 1];

        int receiverNum = 0;
        for(int i = 0; i < playerNum; i++)
        {
            if (i == num)
            {
                continue;
            }
            receiver[receiverNum] = playerReceiver[i];
            receiverNum++;
        }
        return receiver;
    }

    public GameMessageReceiver[] GetAllReceiver()
    {
        return playerReceiver;
    }


    //public void SpawnPlayer(int playerNum)
    //{
    //    playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
    //    GameObject player = Instantiate(playerPrefab[0], playerSpawns[playerNum].position, playerSpawns[playerNum].rotation, gameObject.transform);
    //    player.GetComponent<InputReceiver>().SetTargetNum(playerNum);
    //    //player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[playerNum]);
    //}

}
