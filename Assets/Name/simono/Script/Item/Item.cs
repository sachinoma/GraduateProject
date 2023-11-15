using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //アイテムマネージャー
    ItemManager manager;
    [SerializeField] GameObject particleObject;

    [SerializeField] ItemState itemState;

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
