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
        //�}�l�[�W���[�����X�g���擾����̂�Start���ŁA�^�C�~���O�ɂ���Ă�0�̏ꍇ�����邽�߃R���[�`���ő҂�
        StartCoroutine(SetPlayerReceivers());
    }

    //�}�l�[�W���[���v���C���[���X�g���擾����܂ő҂�
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

    //���łɒ��Ԃ��Ƃ��Ă��邩
    bool IsAlreadyCheck(List<GameMessageReceiver> _receiverList, GameMessageReceiver _target)
    {
        //���g���Ȃ��ꍇ�͑S�����ʂ������̂Ƃ݂Ȃ����[�v�O�ɏI��
        if(_receiverList.Count == 0) { return true; }

        foreach (var receiver in _receiverList)
        {
            //�͂��߂Ēʂ�Ƃ��̂�
            if (receiver == _target)
            {
                _receiverList.Remove(receiver);
                return false;
            }
        }

        return true;
    }
}
