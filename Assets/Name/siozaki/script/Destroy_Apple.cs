using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Apple : MonoBehaviour
{
    private float timeCnt = 0;
    void Update()
    {
        this.timeCnt = this.timeCnt + Time.deltaTime;
        if (transform.position.y < -10.0f || this.timeCnt > 25.0f)
        {
            Destroy(gameObject);
            this.timeCnt = 0;
        } 
    }
}
