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
                Vector3 hitPos = other.contacts[0].point;
                Vector3 forceDir = other.gameObject.transform.position - hitPos;
                forceDir.y += 1.0f;
                forceDir = forceDir.normalized;

                other.gameObject.GetComponent<GameMessageReceiver>().BounceAction(forceDir, power);
            }
            else if(other.gameObject.GetComponent<ResultCharacter>() != null)
            {
                Vector3 hitPos = other.contacts[0].point;
                Vector3 forceDir = other.gameObject.transform.position - hitPos;
                forceDir.y += 1.0f;
                forceDir = forceDir.normalized;

                other.gameObject.GetComponent<ResultCharacter>().BounceAction(forceDir, power);
            }
        }
    }
}
