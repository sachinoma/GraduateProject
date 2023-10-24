using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerConfigurationInput : MonoBehaviour
{
    [SerializeField]
    private int playerNo;

    [SerializeField]
    PlayerInput playerInput;

    private PlayerController controls;

    CallbackContext moveContent;
    CallbackContext jumpContent;

    private void Awake()
    {
        controls = new PlayerController();
        SetPlayerNo((int)playerInput.user.id - 1);
    }
    private void Start()
    {
        
    }

    //ˆÚ“®’†
    public void OnMove(CallbackContext context)
    {
        moveContent = context;
    }
    public CallbackContext GetMove()
    {
        return moveContent;
    }

    //Jump’†
    public void OnJump(CallbackContext context)
    {
        jumpContent = context;
    }

    public CallbackContext GetJump()
    {
        return jumpContent;
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        //playerConfig = pc;
        //playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    public int GetPlayerNo()
    {
        return playerNo;
    }

    public void SetPlayerNo(int num)
    {
        playerNo = num;
    }

}
