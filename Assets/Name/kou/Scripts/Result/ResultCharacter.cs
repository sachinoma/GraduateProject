using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCharacter : MonoBehaviour
{
    [SerializeField]
    private Vector3 savePoint;

    Rigidbody rb;
    void Start()
    {
        SetSavePoint(this.transform);
        rb = this.GetComponent<Rigidbody>();  // rigidbody‚ðŽæ“¾
    }


    private void FixedUpdate()
    {
        if (transform.position.y < -10.0f)
        {
            ReSpawn();
        }
    }


    public void ReSpawn()
    {
        transform.position = savePoint;
    }

    public void SetSavePoint(Transform gameObject)
    {
        savePoint = gameObject.position;
    }

    public void BounceAction(Vector3 forceVec, float force)
    {
        rb.AddForce(forceVec * force, ForceMode.Impulse);
    }
}
