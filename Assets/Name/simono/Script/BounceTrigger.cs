using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BounceTrigger : MonoBehaviour
{
    [SerializeField]
    private float power;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<GameMessageReceiver>() != null)
            {
                Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
                Vector3 forceDir = other.gameObject.transform.position - hitPos;
                forceDir.y += 1.0f;
                forceDir = forceDir.normalized;
                //Debug.Log(forceDir);

                other.gameObject.GetComponent<GameMessageReceiver>().BounceAction(forceDir, power);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
