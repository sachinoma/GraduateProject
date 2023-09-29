using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRod_UP : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //オブジェクトを回転
        transform.Rotate(new Vector3(0, 0.1f, 0));
    }
}
