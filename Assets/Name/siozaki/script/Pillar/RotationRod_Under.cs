using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRod_Under: MonoBehaviour
{
    //�����̉�]���x
    [SerializeField]
    private float RotateSpeed;
    [SerializeField]
    private int SpeedUp;
    
    private int TimeCnt = 0;
    void Update()
    {
        //��]���x�㏸�J�E���^
        this.TimeCnt++;
        //��]���x�㏸
        if (this.TimeCnt > SpeedUp)
        {
            this.RotateSpeed += 0.01f;
            this.TimeCnt = 0;
        }
        //�I�u�W�F�N�g����]
        transform.Rotate(new Vector3(0, RotateSpeed, 0));
    }
}