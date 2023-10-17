using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class LobbyPlayerManager : MonoBehaviour
{
    //プレイヤーのスポーン地点
    [SerializeField]
    private Transform[] playerSpawns;
    //プレイヤーPrefab
    [SerializeField]
    private GameObject[] playerPrefab;

    [SerializeField]
    private PlayerConfiguration[] playerConfigs;



    void Start()
    {
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        //playerConfigsを基にプレイヤーを配置
        if (playerConfigs.Length != 0)
        {
            for (int i = 0; i < playerConfigs.Length; i++)
            {
                Debug.Log(i);
                GameObject player = Instantiate(playerPrefab[0], playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
                player.GetComponent<InputReceiver>().SetTargetNum(i);
            }
        }
    }

    public void SpawnPlayer(int playerNum)
    {
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        GameObject player = Instantiate(playerPrefab[0], playerSpawns[playerNum].position, playerSpawns[playerNum].rotation, gameObject.transform);
        player.GetComponent<InputReceiver>().SetTargetNum(playerNum);
    }

}
