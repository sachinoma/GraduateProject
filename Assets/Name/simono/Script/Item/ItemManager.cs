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

    //リストからターゲットと同じアイテムの番号を取得
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
        //場所保管
        Vector3 respawnPos = _target.transform.position;

        //モデルを取得
        var model = _target.GetComponentInChildren<ItemModel>();

        //リスポーンするアイテムの番号取得（存在しなければNull）
        int itemNum = GetListNum(_target, ItemPrefab);
        if(itemNum < 0) { return; }

        //アイテムの削除
        Destroy(_target);

        //リスポーンコルーチン
        StartCoroutine(RespawnItem(respawnTime, respawnPos, model.IsRandom, itemNum));
    }

    IEnumerator RespawnItem(float _time, Vector3 _pos, bool _isRandom, int _num)
    {
        //指定時間分待つ
        yield return new WaitForSecondsRealtime(_time);

        //ランダムで再生成
        var item = Instantiate(ItemPrefab[_isRandom ? Random.Range(0, ItemPrefab.Count) : _num], _pos, Quaternion.identity);
        item.GetComponentInChildren<ItemModel>().IsRandom = _isRandom;
    }
}

public enum ItemState
{
    SpeedUp = 0,    //スピードアップ
    HighJump,   //ハイジャンプ
    Blowing,    //周囲を吹き飛ばし
    Stun,       //全体スタン
    Slow,       //全体スロー
    //ランダムは必ず最後にする
    Random      //ランダム
}

public static class Tag
{
    public const string PlayerTag = "Player";
}
