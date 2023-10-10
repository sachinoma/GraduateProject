using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    private Mover mover;
    private PlayerController controls;

    [SerializeField]
    private int hitPower;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        controls = new PlayerController();
    }

    private void Start()
    {

    }


    ////Input�C�x���g
    //private void Input_onActionTriggered(CallbackContext context)
    //{
    //    if (context.action.name == controls.Player.Movement.name)
    //    {
    //        OnMove(context);
    //    }

    //    if (context.action.name == controls.Player.Jump.name)
    //    {
    //        OnJump(context);
    //    }

    //    if (context.action.name == controls.Player.CameraMovement.name)
    //    {
    //        OnCameraMove(context);
    //    }
    //}

    //�ړ���
    public void OnMove(CallbackContext context)
    {
        if(mover != null)
        {
            mover.OnMove(context);
        }
    }
    //Camera�ړ���
    public void OnCameraMove(CallbackContext context)
    {
        if (mover != null)
        {
            //mover.OnCameraMove(context);
        }
    }
    //Jump��
    public void OnJump(CallbackContext context)
    {
        if (mover != null)
        {
            mover.OnJump(context);
        }
    }

    //public void InitializePlayer(PlayerConfiguration pc)
    //{
    //    playerConfig = pc;
    //    playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    //}
}
