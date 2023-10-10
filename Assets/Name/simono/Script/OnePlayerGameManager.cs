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
        var gameManager = GameObject.FindWithTag("GameManager").GetComponent<OnePlayerGameManager>();

        gameManager.gameTimer = gameTimer;
        gameManager.isDeath = isDeath;

        //�C�x���g���폜
        SceneManager.sceneLoaded -= ResultSceneLoaded;
    }

}
