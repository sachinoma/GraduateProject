using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //�A�C�e���}�l�[�W���[
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
    SpeedUp,    //�X�s�[�h�A�b�v
    HighJump,   //�n�C�W�����v
    Blowing,    //���͂𐁂���΂�
    Stun,       //�S�̃X�^��
    Random      //�����_��
}
