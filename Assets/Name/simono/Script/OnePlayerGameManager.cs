using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class OnePlayerGameManager : MonoBehaviour
{
    [SerializeField] private float gameTimer;
    private bool isDeath;

    // Start is called before the first frame update
    private void Awake()
    {
        isDeath = false;
        gameTimer = 0.0f;
    }

    private void FixedUpdate()
    {
        if (isDeath) { return; }

       gameTimer += Time.fixedDeltaTime;
    }

    public void GameOver()
    {
        isDeath = true;

        //UI表示

        //SE鳴らす

        //エフェクト出す

        // イベントに登録
        SceneManager.sceneLoaded += ResultSceneLoaded;

        //シーン遷移
        SceneManager.LoadScene("Result");
    }

    private void ResultSceneLoaded(Scene next, LoadSceneMode mode)
    {
        //マネージャーを取得
        var gameManager = GameObject.FindWithTag("GameManager").GetComponent<OnePlayerGameManager>();

        //実態を知らなくても相手が実装してくれるからバグが減る
        gameManager.SetData(gameTimer, isDeath);

        //イベントを削除しないと、シーン遷移でずっと呼ばれてしまう
        SceneManager.sceneLoaded -= ResultSceneLoaded;
    }

    public void SetData(float _gameTimer, bool _isDeath)
    {
        this.gameTimer = _gameTimer;
        this.isDeath = _isDeath;
    }

}
