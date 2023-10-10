using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputReceiver : MonoBehaviour
{
    [SerializeField]
    private Mover mover;

    [SerializeField]
    private PlayerConfigurationInput input;

    CallbackContext moveContent;
    CallbackContext jumpContent;

    int targetNum;

    private void Awake()
    {
        mover = GetComponent<Mover>();
    }

    private void Start()
    {
        SetInputConfiguration(targetNum);
    }

    private void Update()
    {
        OnMove();
        OnJump();
    }

    //ˆÚ“®’†
    public void OnMove()
    {
        if (input != null)
        {
            mover.OnMove(input.GetMove());
        }
    }

    //Jump’†
    public void OnJump()
    {
        if (input != null)
        {
            mover.OnJump(input.GetJump());
        }
    }

    private void SetInputConfiguration(int num)
    {
        var allPlayerConInput = FindObjectsOfType<PlayerConfigurationInput>();
        foreach (PlayerConfigurationInput nowPlayerConInput in allPlayerConInput)
        {
            Debug.Log("searchPlayerConInput = " + nowPlayerConInput.GetPlayerNo());
            if (nowPlayerConInput.GetPlayerNo() == num)
            {
                input = nowPlayerConInput;
            }
        }
    }

    public void SetTargetNum(int num)
    {
        targetNum = num;
    }
}
