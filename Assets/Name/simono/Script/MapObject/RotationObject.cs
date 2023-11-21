using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    [SerializeField] Vector3 rotation;
    [SerializeField] bool isWorldScale = true;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(isWorldScale) transform.Rotate(rotation * Time.deltaTime, Space.World);
        else transform.Rotate(rotation * Time.deltaTime);
    }
}
