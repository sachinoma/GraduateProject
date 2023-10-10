using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRod_Under: MonoBehaviour
{
    //初期の回転速度
    [SerializeField]
    private float RotateSpeed;
    [SerializeField]
    private int SpeedUp;
    
    private int TimeCnt = 0;
    void Update()
    {
        //回転速度上昇カウンタ
        this.TimeCnt++;
        //回転速度上昇
        if (this.TimeCnt > SpeedUp)
        {
            this.RotateSpeed += 0.01f;
            this.TimeCnt = 0;
        }
        //オブジェクトを回転
        transform.Rotate(new Vector3(0, RotateSpeed, 0));
    }
}
