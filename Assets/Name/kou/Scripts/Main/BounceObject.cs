using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObject : MonoBehaviour
{
    [SerializeField]
    private float power;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(other.gameObject.GetComponent<GameMessageReceiver>() != null)
            {
                // �Փˈʒu���擾����
                Vector3 hitPos = other.contacts[0].point;

                // �Փˈʒu���玩�@�֌������x�N�g�������߂�
                Vector3 boundVec = this.transform.position - hitPos;

                // �t�����ɂ͂˂�
                Vector3 forceDir = power * (-boundVec.normalized);

                other.gameObject.GetComponent<GameMessageReceiver>().BounceAction(forceDir, power);
            }
        }
    }
}
