using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MainPlayerManager : MonoBehaviour
{
    //�v���C���[�̃X�|�[���n�_
    [SerializeField]
    private Transform[] playerSpawns;
    //�v���C���[Prefab
    [SerializeField]
    private GameObject[] playerPrefab;

    [SerializeField]
    private PlayerConfiguration[] playerConfigs;

    [SerializeField]
    private GameMessageReceiver[] playerReceiver;

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
        //playerConfigs����Ƀv���C���[��z�u
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        playerReceiver = new GameMessageReceiver[playerConfigs.Length];

        for (int i = 0; i < playerConfigs.Length; i++)
        {
            Debug.Log(i);
            //int prefabNum = playerConfigs[i].PlayerPrefabNum;
            var player = Instantiate(playerPrefab[0], playerSpawns[i].position, playerSpawns[i].rotation);
            //��ʕ���
            player.GetComponent<PlayerCameraLayerUpdater>().SetPlayerNum(i);
            player.GetComponentInChildren<Camera>().rect = cameraRect[playerConfigs.Length - 1][i];

            //CullingMask��S����
            //player.GetComponentInChildren<Camera>().cullingMask = -1;

            player.transform.Find("Avatar").gameObject.GetComponent<InputReceiver>().SetTargetNum(i);
            playerReceiver[i] = player.GetComponentInChildren<GameMessageReceiver>();
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
