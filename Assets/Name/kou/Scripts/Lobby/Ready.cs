using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Ready : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerConfigurationManager playerConfigurationManager;

    [SerializeField]
    Animator readyAnimator;

    private bool readyFinished = false;

    [SerializeField]
    private int readyNum = 0;

    [SerializeField]
    private int readyTimeMax = 3;
    [SerializeField]
    private float readyTime = 0;


    private void Start()
    {
        playerConfigurationManager = GameObject.Find("PlayerInputManager").GetComponent<PlayerConfigurationManager>();
        GameObject playerInputManager = GameObject.Find("PlayerInputManager");
        gameManager = playerInputManager.GetComponent<GameManager>();
    }

    private void Update()
    {
        if(!readyFinished)
        {
            if (IsAllReady())
            {
                readyTime = readyTime + Time.deltaTime;
                readyAnimator.SetBool("CountFlag", true);
            }
            else
            {
                readyTime = 0.0f;
                readyAnimator.SetBool("CountFlag", false);
            }

            if (readyTime >= readyTimeMax)
            {
                readyFinished = true;
                Invoke(nameof(StartGame), 1.0f);
            }
        }      
    }

    private void StartGame()
    {
        gameManager.LoadToMain();
    }

    private bool IsAllReady()
    {
        return playerConfigurationManager.GetNowPlayers() == readyNum && playerConfigurationManager.GetNowPlayers() != 0;
    }


    void OnTriggerEnter(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
            readyNum++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
            readyNum--;
        }
    }
}
