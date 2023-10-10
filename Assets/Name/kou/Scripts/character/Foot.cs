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
    //    //�ڐG�����I�u�W�F�N�g�̃^�O��"Player"�̂Ƃ�
    //    if (other.CompareTag("MapObject"))
    //    {
    //        SetIsGround(true);
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        //�ڐG�����I�u�W�F�N�g�̃^�O��"Player"�̂Ƃ�
        if (other.CompareTag("MapObject"))
        {
            SetIsGround(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        //�ڐG�����I�u�W�F�N�g�̃^�O��"Player"�̂Ƃ�
        if (other.CompareTag("MapObject"))
        {
            SetIsGround(false);
        }
    }
}
