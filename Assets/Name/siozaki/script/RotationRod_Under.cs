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
    
    private int TimeCnt = 0;
    void Update()
    {
        //回転速度上昇カウンタ
        this.TimeCnt++;
        //回転速度上昇
        if (this.TimeCnt > 600)
        {
            this.RotateSpeed += SpeedUp;
            this.TimeCnt = 0;
        }
        //オブジェクトを回転
        transform.Rotate(new Vector3(0, RotateSpeed, 0));
    }
}
