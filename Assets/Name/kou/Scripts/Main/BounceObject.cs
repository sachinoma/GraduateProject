using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObject : MonoBehaviour
{
    [SerializeField]
    private float power;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(other.gameObject.GetComponent<GameMessageReceiver>() != null)
            {
                // 衝突位置を取得する
                Vector3 hitPos = other.contacts[0].point;

                // 衝突位置から自機へ向かうベクトルを求める
                Vector3 boundVec = this.transform.position - hitPos;

                // 逆方向にはねる
                Vector3 forceDir = power * (-boundVec.normalized);

                other.gameObject.GetComponent<GameMessageReceiver>().BounceAction(forceDir, power);
            }
        }
    }
}
