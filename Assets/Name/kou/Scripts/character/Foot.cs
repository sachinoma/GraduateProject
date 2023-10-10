using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Foot : MonoBehaviour
{
    [SerializeField]
    private bool isGround = false;

    public void SetIsGround(bool flag)
    {
        isGround = flag;
    }

    public bool GetIsGround()
    {
        return isGround;
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    //接触したオブジェクトのタグが"Player"のとき
    //    if (other.CompareTag("MapObject"))
    //    {
    //        SetIsGround(true);
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("MapObject"))
        {
            SetIsGround(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("MapObject"))
        {
            SetIsGround(false);
        }
    }
}
