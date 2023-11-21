using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    static ItemManager instance;
    public static ItemManager Instance { get { return instance; } }

    [SerializeField] float respawnTime = 5f;
    [SerializeField] bool isRandom = true;
    [SerializeField] List<GameObject> ItemPrefab;
        
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

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
        Vector3 respawnPos = _target.transform.position;

       int itemNum = GetListNum(_target, ItemPrefab);
        if(itemNum < 0) { return; }

        Destroy(_target);

        StartCoroutine(RespawnItem(respawnTime, respawnPos, itemNum));
    }

    IEnumerator RespawnItem(float _time, Vector3 _pos, int _num)
    {
        //指定時間分待つ
        yield return new WaitForSecondsRealtime(_time);

        //ランダムで再生成
        Instantiate(ItemPrefab[isRandom ? Random.Range(0, ItemPrefab.Count) : _num], _pos, Quaternion.identity);
    }
}

public enum ItemState
{
    SpeedUp,    //スピードアップ
    HighJump,   //ハイジャンプ
    Blowing,    //周囲を吹き飛ばし
    Stun,       //全体スタン
    Random,     //ランダム
    Slow        //全体スロー
}

public static class Tag
{
    public const string PlayerTag = "Player";
}
