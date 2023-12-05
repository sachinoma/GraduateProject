using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform createOffset;
    [SerializeField] int length = 50;

    //横のオフセット
    Vector3 HorOffset = new(1.74f, 0.0f, 0.0f);
    //斜めのオフセット
    Vector3 angOffset = new(-0.87f, 0f, 1.5f);

    void Start()
    {
        //ベースとなる座標
        Vector3 baseVec = createOffset.position;
        
        //上半分
        for (int i = length / 2; i > 0; i--)
        {
            //一列に生成
            for (int j = 0; j < length - i; j++)
            {
                Vector3 offset = baseVec + j * HorOffset;
                Instantiate(prefab, offset, Quaternion.identity, transform);
            }
            //斜め方向に加算
            baseVec += angOffset;

        }

        //下半分
        for (int i = 0; i <= length / 2; i++)
        {
            //一列に生成
            for (int j = 0; j < length - i; j++)
            {
                Vector3 offset = baseVec + j * HorOffset;
                Instantiate(prefab, offset, Quaternion.identity, transform);
            }
            //横軸は元の位置に戻るように加算
            baseVec = baseVec + new Vector3(-angOffset.x, angOffset.y, angOffset.z);
        }
    }
}
