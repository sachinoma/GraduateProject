using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMessageReceiver : MonoBehaviour
{
    [SerializeField]
    private Mover mover;

    [SerializeField]
    private PlayerStatus status;

    [SerializeField]
    private GameMessageReceiver[] otherReceiver;

    MainPlayerManager playerManager;

    private void Start()
    {
        playerManager = FindAnyObjectByType<MainPlayerManager>();
        Invoke(nameof(GetOtherReceiver) , 0.5f);
    }

    private void GetOtherReceiver()
    {
        otherReceiver = playerManager.GetOtherReceiver(status.GetPlayerNum());
    }

    public void BounceAction(Vector3 forceVec, float force)
    {
        mover.BounceAction(forceVec, force);
    }

    public void ChangeOutfit()
    {
        mover.ChangeOutfit();
    }

    public void SetSavePoint(Transform savePos)
    {
        status.SetSavePoint(savePos.transform);
    }

    public void GetItem(ItemState _state)
    {
        if (_state == ItemState.SpeedUp) 
        {
            mover.SpeedUp();
        }
        else if (_state == ItemState.Slow)
        {
            if (otherReceiver.Length != 0)
            {
                foreach (var item in otherReceiver)
                {
                    item.Slow();
                }
            }
        }
        else if (_state == ItemState.Stun) 
        { 
            if(otherReceiver.Length != 0)
            {
                foreach(var item in otherReceiver)
                {
                    item.Stun();
                }
            }     
        }
    }

    public void Slow()
    {
        mover.Slow();
    }

    public void Stun()
    {
        mover.Stun();
    }
}
