using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private Mover mover;

    [SerializeField]
    private SkinnedMeshRenderer playerMesh;
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


    //InputƒCƒxƒ“ƒg
    private void Input_onActionTriggered(CallbackContext context)
    {
        if(context.action.name == controls.Player.Movement.name)
        {
            OnMove(context);
        }
        if (context.action.name == controls.Player.Attack.name)
        {
            OnAttack(context, hitPower);
        }
        if(context.action.name == controls.Player.Skill1.name)
        {
            OnSkill(context, 0);
        }
        if(context.action.name == controls.Player.Skill2.name)
        {
            OnAttack(context, hitPower);
        }
        if(context.action.name == controls.Player.Skill3.name)
        {
            OnAttack(context, hitPower);
        }
    }

    //ˆÚ“®’†
    public void OnMove(CallbackContext context)
    {
        if(mover != null)
        {
            mover.OnMove(context);
        }
    }
    //Attack’†
    public void OnAttack(CallbackContext context, float power)
    {
        if (mover != null)
        {
            mover.OnAttack(context, power);
        }
    }

    private void OnSkill(CallbackContext context, int num)
    {
        mover.OnSkill(context, num);
    }
}
