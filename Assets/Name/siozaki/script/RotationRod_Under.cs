using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRod_Under: MonoBehaviour
{
    //初期の回転速度
    [SerializeField]
    private float RotateSpeed;
    [SerializeField]
    private float SpeedUp;
    [SerializeField]
    private float SpeedUpCnt;

    private float TimeCnt = 0;
    void Update()
    {
        //カウンタ
        this.TimeCnt = TimeCnt + Time.deltaTime;
        //回転速度上昇
        if (this.TimeCnt > SpeedUpCnt)
        {
            this.RotateSpeed += SpeedUp;
            this.TimeCnt = 0;
        }
        //オブジェクトを回転
        transform.Rotate(new Vector3(0, RotateSpeed * Time.deltaTime, 0));
    }
}
