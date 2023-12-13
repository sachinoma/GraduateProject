using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SavePointTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform savePos;

    [SerializeField]
    private SoundEffect soundEffect;

    [SerializeField]
    private AudioClip clip;

    [SerializeField]
    private GameObject saveEffect;

    [SerializeField]
    private MainPlayerManager mainPlayerManager;

    private List<GameMessageReceiver> playerReceivers;

    private void Start()
    {
        //マネージャーがリストを取得するのはStart内で、タイミングによっては0の場合もあるためコルーチンで待つ
        StartCoroutine(SetPlayerReceivers());
    }

    //マネージャーがプレイヤーリストを取得するまで待つ
    IEnumerator SetPlayerReceivers()
    {
        GameMessageReceiver[] list = mainPlayerManager.GetAllReceiver();
        while (list.Length <= 0)
        {
            yield return null;

            list = mainPlayerManager.GetAllReceiver();
        }

        playerReceivers = new(list);
    }

    void OnTriggerEnter(Collider other)
    {
        var receiver = other.GetComponent<GameMessageReceiver>();

        if (other.CompareTag("Player") && !IsAlreadyCheck(playerReceivers, receiver))
        {
            if (other.gameObject.GetComponent<GameMessageReceiver>() != null)
            {
                other.gameObject.GetComponent<GameMessageReceiver>().SetSavePoint(savePos);
                soundEffect.PlaySoundEffectClip(clip);

                Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
                Instantiate(saveEffect, hitPos, Quaternion.identity);
            }
        }
    }

    //すでに中間をとっているか
    bool IsAlreadyCheck(List<GameMessageReceiver> _receiverList, GameMessageReceiver _target)
    {
        //中身がない場合は全員が通ったものとみなしループ前に終了
        if(_receiverList.Count == 0) { return true; }

        foreach (var receiver in _receiverList)
        {
            //はじめて通るときのみ
            if (receiver == _target)
            {
                _receiverList.Remove(receiver);
                return false;
            }
        }

        return true;
    }
}
