using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRod_Under: MonoBehaviour
{
    private float RY = 0.1f;
    private int SpeedUpCnt = 0;
    void Update()
    {
        //��]���x�㏸
        if(this.SpeedUpCnt > 600)
        {
            this.RY += 0.01f;
            this.SpeedUpCnt = 0;
        }
        //�I�u�W�F�N�g����]
        transform.Rotate(new Vector3(0, RY, 0));
        //��]���x�㏸�J�E���^
        this.SpeedUpCnt++;
    }
}
