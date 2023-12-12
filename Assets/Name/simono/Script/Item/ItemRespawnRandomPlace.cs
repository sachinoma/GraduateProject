using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemRespawnRandomPlace : MonoBehaviour
{
    [SerializeField] List<BoxCollider> colliders;

    public BoxCollider GetRespawnPlace(GameObject _item)
    {
        BoxCollider near = colliders[0];
        for(int i = 1; i < colliders.Count; i++)
        {
            //ターゲットとのY軸の距離を取得
            float before = Mathf.Abs(_item.transform.position.y - colliders[i - 1].bounds.center.y);
            float current = Mathf.Abs(_item.transform.position.y - colliders[i].bounds.center.y);

            //距離が近いほうを生成範囲にする
            if (current < before)
            {
                near = colliders[i];
            }
        }

        return near;
    }
}
