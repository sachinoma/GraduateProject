using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MainPlayerManager : MonoBehaviour
{
    //�V���O���g���擾
    private GameManager gameManager;
    [SerializeField]
    private ResultData[] resultData;

    //�v���C���[�̃X�|�[���n�_
    [SerializeField]
    private Transform[] playerSpawns;
    //�v���C���[Prefab
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
    //���݂̋����i�\�[�g������j
    private float[] distance;
    //���݂̏���
    [SerializeField]
    private int[] rankTmp;
    private int[] rankBefore;
    private int[] rankOriData;
    private bool[] rankLock;

    [SerializeField]
    private GameObject[] uiCanvasByPlayerNum;

    //��ʕ����̐ݒ�
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

        //playerConfigs����Ƀv���C���[��z�u
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
                
            //��ʕ���
            player.GetComponent<PlayerCameraLayerUpdater>().SetPlayerNum(i);
            player.GetComponentInChildren<Camera>().rect = cameraRect[playerNum - 1][i];

            player.transform.Find("Avatar").gameObject.GetComponent<InputReceiver>().SetTargetNum(i);
            playerReceiver[i] = player.GetComponentInChildren<GameMessageReceiver>();
            //�v���C���[��avatar�i���݈ʒu�擾�p�j�j
            playerAvatar[i] = player.transform.Find("Avatar");
            //rankTmp�̏�����
            rankTmp[i] = rankNum;
            rankBefore[i] = 0;
            rankNum++;
        }
        //�v���C���[�l���ɑΉ�����ui��\��
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
                    //�S�[�������l��r�����邽�߂ɑ傫���l�ɂ��܂��B
                    distance[i] = 10000000;
                    lockNum++;
                }
                else
                {
                    distance[i] = Vector3.Distance(goal.position, playerAvatar[i].position);
                }
            }
            //�������\�[�g�����܂�
            Array.Sort(distance);

            //�\�[�g���������ɂ�胉���N���߂܂�
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
