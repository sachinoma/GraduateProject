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

    //�v���C���[�̃X�|�[���n�_
    [SerializeField]
    private Transform[] playerSpawns;
    //�v���C���[Prefab
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

        //playerConfigs����Ƀv���C���[��z�u
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        
        var player = Instantiate(playerPrefab[0], playerSpawns[0].position, playerSpawns[0].rotation);
        player.transform.Find("Avatar").gameObject.GetComponent<InputReceiver>().SetTargetNum(0);
        GameStart();
        //�J�nUI�̕\��
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

        //UI�\��

        //SE�炷

        //�G�t�F�N�g�o��

        //���сi���U���g�p�j�o�^
        resultData[0].UpdateScore(gameTimer);

        // �C�x���g�ɓo�^
        //SceneManager.sceneLoaded += ResultSceneLoaded;

        //�V�[���J��
        gameManager.LoadToSoloResult();
    }

    //private void ResultSceneLoaded(Scene next, LoadSceneMode mode)
    //{
    //    //�}�l�[�W���[���擾
    //    var gameManager = GameObject.FindWithTag("GameManager").GetComponent<ResultManager>();

    //    //���Ԃ�m��Ȃ��Ă����肪�������Ă���邩��o�O������
    //    gameManager.SetData(gameTimer);

    //    //�C�x���g���폜���Ȃ��ƁA�V�[���J�ڂł����ƌĂ΂�Ă��܂�
    //    SceneManager.sceneLoaded -= ResultSceneLoaded;
    //}

}
