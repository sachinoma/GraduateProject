using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRod_Under: MonoBehaviour
{
    private float RY = 0.1f;
    private int SpeedUpCnt = 0;
    void Update()
    {
        //回転速度上昇
        if(this.SpeedUpCnt > 600)
        {
            this.RY += 0.01f;
            this.SpeedUpCnt = 0;
        }
        //オブジェクトを回転
        transform.Rotate(new Vector3(0, RY, 0));
        //回転速度上昇カウンタ
        this.SpeedUpCnt++;
    }
}
