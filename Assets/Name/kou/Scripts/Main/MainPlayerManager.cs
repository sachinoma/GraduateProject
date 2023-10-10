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

    //��ʕ����̐ݒ�
    [SerializeField]
    private Rect[][] cameraRect = new Rect[4][]
    {
        new Rect[]{ new Rect(0.0f, 0.0f, 1.0f, 1.0f) },
        new Rect[]{ new Rect(0.0f, 0.0f, 0.5f, 1.0f) , new Rect(0.5f, 0.0f, 0.5f, 1.0f) },
        new Rect[]{ new Rect(0.0f, 0.5f, 0.5f, 0.5f) , new Rect(0.5f, 0.5f, 0.5f, 0.5f) , new Rect(0.0f, 0.0f, 0.5f, 0.5f) },
        new Rect[]{ new Rect(0.0f, 0.5f, 0.5f, 0.5f) , new Rect(0.5f, 0.5f, 0.5f, 0.5f) , new Rect(0.0f, 0.0f, 0.5f, 0.5f) , new Rect(0.5f, 0.0f, 0.5f, 0.5f) }
    };
        
        
        //{ new Rect(0.0f, 0.0f, 1.0f, 1.0f), new Rect(0.0f, 0.0f, 1.0f, 1.0f) }; 

    void Start()
    {
        //playerConfigs����Ƀv���C���[��z�u
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            Debug.Log(i);
            int prefabNum = playerConfigs[i].PlayerPrefabNum;
            var player = Instantiate(playerPrefab[prefabNum], playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            //��ʕ���
            player.GetComponent<PlayerCameraLayerUpdater>().GetCamera().rect = cameraRect[playerConfigs.Length - 1][i];
            //player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);    
        }
    }

    //public void SpawnPlayer(int playerNum)
    //{
    //    playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
    //    GameObject player = Instantiate(playerPrefab[0], playerSpawns[playerNum].position, playerSpawns[playerNum].rotation, gameObject.transform);
    //    player.GetComponent<InputReceiver>().SetTargetNum(playerNum);
    //    //player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[playerNum]);
    //}

}
