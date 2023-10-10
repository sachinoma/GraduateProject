using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    // Start is called before the first frame update
    void Awake()
    {
        
    }


    public void SetData(float _time)
    {
        lifeTime = _time;
    }
}
