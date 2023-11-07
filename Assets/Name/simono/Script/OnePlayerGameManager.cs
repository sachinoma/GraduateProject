using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class OnePlayerGameManager : MonoBehaviour
{
    private GameManager gameManager;
    private ResultData[] resultData;

    [SerializeField] private float gameTimer;
    private bool isStart;
    private bool isDeath;

    //プレイヤーのスポーン地点
    [SerializeField]
    private Transform[] playerSpawns;
    //プレイヤーPrefab
    [SerializeField]
    private GameObject[] playerPrefab;
    [SerializeField]
    private PlayerConfiguration[] playerConfigs;

    private void Awake()
    {
        isStart = false;
        isDeath = false;
        gameTimer = 0.0f;
    }

    private void Start()
    {
        GameObject playerInputManager = GameObject.Find("PlayerInputManager");
        gameManager = playerInputManager.GetComponent<GameManager>();
        resultData = GameManager.Instance.GetResultData().ToArray();

        //playerConfigsを基にプレイヤーを配置
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        
        var player = Instantiate(playerPrefab[0], playerSpawns[0].position, playerSpawns[0].rotation);
        player.transform.Find("Avatar").gameObject.GetComponent<InputReceiver>().SetTargetNum(0);
        GameStart();
        //開始UIの表示
    }

    private void FixedUpdate()
    {
        if (!isStart) { return; }
        if (isDeath) { return; }

       gameTimer += Time.fixedDeltaTime;
    }

    public void GameStart()
    {
        isStart = true;
    }

    public void GameOver()
    {
        isDeath = true;

        //UI表示

        //SE鳴らす

        //エフェクト出す

        //成績（リザルト用）登録
        resultData[0].UpdateScore(gameTimer);

        // イベントに登録
        //SceneManager.sceneLoaded += ResultSceneLoaded;

        //シーン遷移
        gameManager.LoadToSoloResult();
    }

    //private void ResultSceneLoaded(Scene next, LoadSceneMode mode)
    //{
    //    //マネージャーを取得
    //    var gameManager = GameObject.FindWithTag("GameManager").GetComponent<ResultManager>();

    //    //実態を知らなくても相手が実装してくれるからバグが減る
    //    gameManager.SetData(gameTimer);

    //    //イベントを削除しないと、シーン遷移でずっと呼ばれてしまう
    //    SceneManager.sceneLoaded -= ResultSceneLoaded;
    //}

}
