using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    static ItemManager instance;
    public static ItemManager Instance { get { return instance; } }
    [SerializeField] float respawnTime = 5f;
    [SerializeField] List<GameObject> ItemPrefab;
        
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    //���X�g����^�[�Q�b�g�Ɠ����A�C�e���̔ԍ����擾
    int GetListNum(GameObject _target, List<GameObject> _list)
    {
        var state = _target.GetComponentInChildren<Item>().GetState();
        for (int i = 0; i < _list.Count; i++)
        {
            if (state == _list[i].GetComponentInChildren<Item>().GetState())
            {
                return i;
            }
        }

        return -1;
    }

    public void Respawn(GameObject _target)
    {
        //�ꏊ�ۊ�
        Vector3 respawnPos = _target.transform.position;

        //���f�����擾
        var model = _target.GetComponentInChildren<ItemModel>();

        //���X�|�[������A�C�e���̔ԍ��擾�i���݂��Ȃ����Null�j
        int itemNum = GetListNum(_target, ItemPrefab);
        if(itemNum < 0) { return; }

        //�A�C�e���̍폜
        Destroy(_target);

        //���X�|�[���R���[�`��
        StartCoroutine(RespawnItem(respawnTime, respawnPos, model.IsRandom, itemNum));
    }

    IEnumerator RespawnItem(float _time, Vector3 _pos, bool _isRandom, int _num)
    {
        //�w�莞�ԕ��҂�
        yield return new WaitForSecondsRealtime(_time);

        //�����_���ōĐ���
        var item = Instantiate(ItemPrefab[_isRandom ? Random.Range(0, ItemPrefab.Count) : _num], _pos, Quaternion.identity);
        item.GetComponentInChildren<ItemModel>().IsRandom = _isRandom;
    }
}

public enum ItemState
{
    SpeedUp = 0,    //�X�s�[�h�A�b�v
    HighJump,   //�n�C�W�����v
    Blowing,    //���͂𐁂���΂�
    Stun,       //�S�̃X�^��
    Slow,       //�S�̃X���[
    //�����_���͕K���Ō�ɂ���
    Random      //�����_��
}

public static class Tag
{
    public const string PlayerTag = "Player";
}
