using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiResultManager : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        GameObject playerInputManager = GameObject.Find("PlayerInputManager");
        gameManager = playerInputManager.GetComponent<GameManager>();
    }

    void Update()
    {
        
    }

    public void LoadToLobby()
    {
        gameManager.LoadToLobby();
    }

    public void LoadToMain()
    {
        gameManager.LoadToMain();
    }

    public void LoadToTitle()
    {
        gameManager.LoadToTitle();
    }
}
