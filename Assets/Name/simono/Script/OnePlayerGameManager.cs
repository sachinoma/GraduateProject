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
        var gameManager = GameObject.FindWithTag("GameManager").GetComponent<OnePlayerGameManager>();

        //���Ԃ�m��Ȃ��Ă����肪�������Ă���邩��o�O������
        gameManager.SetData(gameTimer, isDeath);

        //�C�x���g���폜���Ȃ��ƁA�V�[���J�ڂł����ƌĂ΂�Ă��܂�
        SceneManager.sceneLoaded -= ResultSceneLoaded;
    }

    public void SetData(float _gameTimer, bool _isDeath)
    {
        this.gameTimer = _gameTimer;
        this.isDeath = _isDeath;
    }

}
