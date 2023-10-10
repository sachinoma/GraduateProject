using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class OnePlayerGameManager : MonoBehaviour
{
    [SerializeField] private float gameTimer;
    private bool isDeath;
    // Start is called before the first frame update
    void Start()
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

        //シーン遷移
    }

    
}
