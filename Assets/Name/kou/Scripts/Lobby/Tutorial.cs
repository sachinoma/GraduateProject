using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private int readyNum = 0;

    private bool isOpen = true;

    private void Update()
    {
        if(IsPlayerInside())
        {
            if (!isOpen)
            {
                isOpen = true;
                _animator.SetBool("isOpen", isOpen);
            }
        }
        else
        {
            if (isOpen)
            {
                isOpen = false;
                _animator.SetBool("isOpen", isOpen);
            }
        }
    }

    private bool IsPlayerInside()
    {
        return readyNum != 0;
    }

    void OnTriggerEnter(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
            readyNum++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
            readyNum--;
        }
    }
}
