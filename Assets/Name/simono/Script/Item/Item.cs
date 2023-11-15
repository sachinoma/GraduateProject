using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //�A�C�e���}�l�[�W���[
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
    SpeedUp,    //�X�s�[�h�A�b�v
    HighJump,   //�n�C�W�����v
    Blowing,    //���͂𐁂���΂�
    Stun,       //�S�̃X�^��
    Random      //�����_��
}
