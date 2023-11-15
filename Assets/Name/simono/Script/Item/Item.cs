using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //アイテムマネージャー
    ItemManager manager;
    [SerializeField] ItemState itemState;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponentInParent<ItemManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(manager.DetectPlayer(other))
        {
            var receiver = other.gameObject.GetComponentInChildren<GameMessageReceiver>();

            receiver.GetItem(itemState);

            manager.CallPlaySound();
            manager.CallCreateParticle();
            manager.CallDestroy();
        }
    }
}

public enum ItemState
{
    SpeedUp,    //スピードアップ
    HighJump,   //ハイジャンプ
    Blowing,    //周囲を吹き飛ばし
    Stun,       //全体スタン
    Random      //ランダム
}
