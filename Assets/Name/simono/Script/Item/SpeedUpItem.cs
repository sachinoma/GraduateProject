using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : MonoBehaviour
{
    //アイテムマネージャー
    ItemManager manager;
    [SerializeField] GameObject particleObject;

    // Start is called before the first frame update
    void Start()
    {
        manager = new ItemManager(GetComponent<AudioSource>(), particleObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(manager.DetectPlayer(collision.gameObject))
        {
            var receiver = collision.gameObject.GetComponentInChildren<GameMessageReceiver>();

            receiver.GetItem(ItemState.SpeedUp);

            manager.CallPlaySound();
            manager.CallCreateParticle();
            manager.CallDestroy();
        }
    }
}

public enum ItemState
{
    SpeedUp,
    Stun,
}
