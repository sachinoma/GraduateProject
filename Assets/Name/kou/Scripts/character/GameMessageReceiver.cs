using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMessageReceiver : MonoBehaviour
{
    [SerializeField]
    private Mover mover;

    [SerializeField]
    private PlayerStatus status;

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
        if (_state == ItemState.SpeedUp) { }
        else if (_state == ItemState.Stun) { mover.Stun(); }
    }
}
