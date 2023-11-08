using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Foot : MonoBehaviour
{
    [SerializeField]
    private bool isGround = false;

    [SerializeField]
    GameObject attachParent;

    public void SetIsGround(bool flag)
    {
        isGround = flag;
    }

    public bool GetIsGround()
    {
        return isGround;
    }

    private void OnTriggerStay(Collider other)
    {
        //接触したオブジェクトのタグが"MapObject"のとき
        if (other.CompareTag("MapObject") || other.CompareTag("MoveMapObject"))
        {
            SetIsGround(true);
            
            if(other.CompareTag("MoveMapObject"))
            {
                AttachGround(other.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //接触したオブジェクトのタグが"MapObject"のとき
        if (other.CompareTag("MapObject") || other.CompareTag("MoveMapObject"))
        {
            SetIsGround(false);

            if (other.CompareTag("MoveMapObject"))
            {
                UnAttachGround();
            }
        }
    }

    private void AttachGround(GameObject target)
    {
        attachParent.transform.SetParent(target.transform);
    }

    private void UnAttachGround()
    {
        attachParent.transform.parent = null;
    }
}
