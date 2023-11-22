using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    //アイテムマネージャー
    ItemModel item;
    [SerializeField] ItemState itemState;
    Collider trigger;

    // Start is called before the first frame update
    void Start()
    {
        item = GetComponentInParent<ItemModel>();
        trigger = GetComponent<MeshCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(item.DetectPlayer(other))
        {
            var receiver = other.gameObject.GetComponentInChildren<GameMessageReceiver>();
            receiver.GetItem(itemState);

            item.CallPlaySound();
            item.CallCreateParticle();
            item.CallRespawn();
        }
    }

    public ItemState GetState()
    {
        return itemState;
    }
}


