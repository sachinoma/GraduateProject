using System;
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
        if(playerManager != null) { otherReceiver = playerManager.GetOtherReceiver(status.GetPlayerNum()); }
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
        else if (_state == ItemState.Blowing)
        {
            mover.Blowing();
        }
        else if(_state == ItemState.Random)
        {
            //ランダムアイテムを除いた中からランダムで取得
            int randValue = UnityEngine.Random.Range(0, ((int)ItemState.Random) - 1);
            ItemState state = (ItemState)Enum.ToObject(typeof(ItemState), randValue);
            Debug.Log(randValue.ToString());
            GetItem(state);
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
