using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RespawnFromSavePoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Tag.PlayerTag))
        {
            PlayerStatus status;
            if (status = other.GetComponent<PlayerStatus>())
            {
                status.PlusFallNum();
                status.ReSpawn();
            }
        }
    }
}
