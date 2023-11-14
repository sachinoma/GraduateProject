using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRod_Under: MonoBehaviour
{
    //�����̉�]���x
    [SerializeField]
    private float RotateSpeed;
    [SerializeField]
    private float SpeedUp;
    [SerializeField]
    private float SpeedUpCnt;

    private float TimeCnt = 0;
    void Update()
    {
        //�J�E���^
        this.TimeCnt = TimeCnt + Time.deltaTime;
        //��]���x�㏸
        if (this.TimeCnt > SpeedUpCnt)
        {
            this.RotateSpeed += SpeedUp;
            this.TimeCnt = 0;
        }
        //�I�u�W�F�N�g����]
        transform.Rotate(new Vector3(0, RotateSpeed * Time.deltaTime, 0));
    }
}
