using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class OnePlayerGameManager : MonoBehaviour
{
    [SerializeField] private float gameTimer;
    private bool isStart;
    private bool isDeath;

    // Start is called before the first frame update
    private void Awake()
    {
        isStart = false;
        isDeath = false;
        gameTimer = 0.0f;
    }

    private void Start()
    {
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

        // �C�x���g�ɓo�^
        SceneManager.sceneLoaded += ResultSceneLoaded;

        //�V�[���J��
        SceneManager.LoadScene("Result");
    }

    private void ResultSceneLoaded(Scene next, LoadSceneMode mode)
    {
        //�}�l�[�W���[���擾
        var gameManager = GameObject.FindWithTag("GameManager").GetComponent<ResultManager>();

        //���Ԃ�m��Ȃ��Ă����肪�������Ă���邩��o�O������
        gameManager.SetData(gameTimer);

        //�C�x���g���폜���Ȃ��ƁA�V�[���J�ڂł����ƌĂ΂�Ă��܂�
        SceneManager.sceneLoaded -= ResultSceneLoaded;
    }

}
