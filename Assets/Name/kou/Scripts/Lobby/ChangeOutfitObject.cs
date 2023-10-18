using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOutfitObject : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<GameMessageReceiver>() != null)
            {
                other.gameObject.GetComponent<GameMessageReceiver>().ChangeOutfit();
            }
        }
    }
}
