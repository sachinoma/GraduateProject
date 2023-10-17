using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMessageReceiver : MonoBehaviour
{
    [SerializeField]
    private Mover mover;

    public void BounceAction(Vector3 forceVec, float force)
    {
        mover.BounceAction(forceVec, force);
    }
}
