using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;

    [SerializeField]
    private PlayerInputManager playerInputManager;
    [SerializeField]
    private LobbyPlayerManager lobbyPlayerManager;

    //最大プレイヤー数
    [SerializeField]
    private int MaxPlayers = 4;
    //現在プレイヤー数
    [SerializeField]
    private int nowPlayers = 0;

    [SerializeField]
    private int WinnerPlayers;
    private int WinnerPrefabNum;

    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("trying to creat another singleton!");
            Destroy(this.gameObject);
        }
        else
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;

            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }

        SetLobbyPlayerManager(true);
    }


    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void SetPlayerColor(int index,Material color)
    {
        playerConfigs[index].PlayerMaterial = color;
    }

    public void SetPlayerPrefab(int index, int prefabNum)
    {
        playerConfigs[index].PlayerPrefabNum = prefabNum;
    }

    public void SetLobbyPlayerManager(bool flag)
    {
        if(flag)
        {
            GameObject lobbyManager = GameObject.Find("LobbyPlayerManager");
            if (lobbyManager != null)
            {
                lobbyPlayerManager = lobbyManager.GetComponent<LobbyPlayerManager>();
            }
        }
        else
        {
            lobbyPlayerManager = null;
        }
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        if(playerConfigs.Count == MaxPlayers && playerConfigs.All(p => p.IsReady == true))
        {
            GameStart();
        }
    }

    public void DisReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = false;
    }

    private void GameStart()
    {
        SetLobbyPlayerManager(false);
        LoadScene();
    }
    private void LoadScene()
    {
        SceneManager.LoadScene("Test_Main");
    }

    //playerConfigsを格納しているリストを空にする
    public void ListEmpty() 
    {
        playerConfigs.RemoveAll(p => p != null);
    }
    //プレイヤーがJoinした時呼び出せる、もしプレイヤーがの数がMaxPlayer以下ならplayerConfigsを作る。
    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Joined " + pi.playerIndex);
        if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex) && playerConfigs.Count < MaxPlayers)
        {
            Debug.Log("Player Joined " + (pi.user.id - 1));
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(pi));
            lobbyPlayerManager.SpawnPlayer((int)pi.user.id - 1);
            nowPlayers++;
        }
    }

    //MaxPlayerを返す
    public int GetMaxPlayer()
    {
        return MaxPlayers;
    }

    public void SetWinner(int num)
    {
        WinnerPlayers = num;
        WinnerPrefabNum = playerConfigs[num].PlayerPrefabNum;
    }

    public int GetWinner()
    {
        return WinnerPlayers;
    }

    public int GetWinnerPrefabNum()
    {
        return WinnerPrefabNum;
    }

    public int GetNowPlayers()
    {
        return nowPlayers;
    }

    public void SetPlayerInputManager(bool flag)
    {
        playerInputManager.enabled = flag;
    }

    public void SetPlayerInputManagerJoinSetting(bool flag)
    {
        playerInputManager.EnableJoining();
    }


    private void OnDestroy()
    {
       SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Listener for sceneLoaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //if (!string.Equals(scene.path, this.scene.path)) return;

        Debug.Log(scene.name + " scene loaded");
        //ロビー再ロード
        SetLobbyPlayerManager(true);
    }
}

//playerConfigsクラス
public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public Material PlayerMaterial { get; set; }

    public int PlayerPrefabNum { get; set; }
}
