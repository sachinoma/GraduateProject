using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRod_UP : MonoBehaviour
{
    [SerializeField]
    private float RotateSpeed;
    // Update is called once per frame
    void Update()
    {
        //�I�u�W�F�N�g����]
        transform.Rotate(new Vector3(0, RotateSpeed, 0));
    }
}
