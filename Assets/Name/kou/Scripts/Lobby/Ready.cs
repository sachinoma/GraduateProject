using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ready : MonoBehaviour
{
    private PlayerConfigurationManager manager;

    [SerializeField]
    private int readyNum = 0;

    [SerializeField]
    private int readyTimeMax = 3;
    [SerializeField]
    private float readyTime = 0;

    private void Start()
    {
        manager = GameObject.Find("PlayerInputManager").GetComponent<PlayerConfigurationManager>();
    }

    private void Update()
    {
        if (IsAllReady())
        {
            readyTime = readyTime + Time.deltaTime;
        }
        else
        {
            readyTime = 0.0f;
        }

        if(readyTime >= readyTimeMax)
        {
            SceneManager.LoadScene("Test_Main");
        }
    }

    private bool IsAllReady()
    {
        return manager.GetNowPlayers() == readyNum && manager.GetNowPlayers() != 0;
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
