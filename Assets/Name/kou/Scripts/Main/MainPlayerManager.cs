using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MainPlayerManager : MonoBehaviour
{
    private GameManager gameManager;

    //プレイヤーのスポーン地点
    [SerializeField]
    private Transform[] playerSpawns;
    //プレイヤーPrefab
    [SerializeField]
    private GameObject[] playerPrefab;

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
    private int[] rank;

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

        //playerConfigsを基にプレイヤーを配置
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        playerAvatar = new Transform[playerConfigs.Length];
        distance = new float[playerConfigs.Length];
        rank = new int[playerConfigs.Length];
        playerReceiver = new GameMessageReceiver[playerConfigs.Length];

        int rankNum = 0;
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            Debug.Log(i);
            var player = Instantiate(playerPrefab[0], playerSpawns[i].position, playerSpawns[i].rotation);
                
            //画面分割
            player.GetComponent<PlayerCameraLayerUpdater>().SetPlayerNum(i);
            player.GetComponentInChildren<Camera>().rect = cameraRect[playerConfigs.Length - 1][i];

            player.transform.Find("Avatar").gameObject.GetComponent<InputReceiver>().SetTargetNum(i);
            playerReceiver[i] = player.GetComponentInChildren<GameMessageReceiver>();
            //プレイヤーのavatar（現在位置取得用））
            playerAvatar[i] = player.transform.Find("Avatar");
            //rankの初期化
            rank[i] = rankNum;
            rankNum++;
        }
        //プレイヤー人数に対応するuiを表示
        uiCanvasByPlayerNum[playerConfigs.Length - 1].SetActive(true);
    }

    private void Update()
    {
        CheckNowRank();
        gameManager.SetRank(rank);
    }

    private void CheckNowRank()
    {
        for(int i = 0;i < playerConfigs.Length;i++) 
        {
            distance[i] = Vector3.Distance(goal.position, playerAvatar[i].position);
        }

        Array.Sort(distance);

        for (int i = 0; i < playerConfigs.Length; ++i)
        {
            for (int j = 0; j < playerConfigs.Length; ++j)
            {
                if (Vector3.Distance(goal.position, playerAvatar[j].position) == distance[i])
                {
                    rank[i] = j;
                }
            }
        }
        
    }

    public GameMessageReceiver[] GetOtherReceiver(int num)
    {
        GameMessageReceiver[] receiver = new GameMessageReceiver[playerConfigs.Length - 1];

        int receiverNum = 0;
        for(int i = 0; i < playerConfigs.Length; i++)
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


    //public void SpawnPlayer(int playerNum)
    //{
    //    playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
    //    GameObject player = Instantiate(playerPrefab[0], playerSpawns[playerNum].position, playerSpawns[playerNum].rotation, gameObject.transform);
    //    player.GetComponent<InputReceiver>().SetTargetNum(playerNum);
    //    //player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[playerNum]);
    //}

}
