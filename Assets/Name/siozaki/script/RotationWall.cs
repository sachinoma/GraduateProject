using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWall : MonoBehaviour
{
    [SerializeField]
    private float RotateSpeed;
    // Update is called once per frame
    void Update()
    {
        //�I�u�W�F�N�g����]
        transform.Rotate(new Vector3(0, 0, RotateSpeed * Time.deltaTime));
    }
}
