using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private PlayerConfigurationManager manager;

    private void Start()
    {
        manager = GameObject.Find("PlayerInputManager").GetComponent<PlayerConfigurationManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
            manager.SetPlayerInputManager(true);
            manager.SetPlayerInputManagerJoinSetting(true);
            SceneManager.LoadScene("Test_Lobby");
        }
    }

    void OnTriggerExit(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
            
        }
    }
}
