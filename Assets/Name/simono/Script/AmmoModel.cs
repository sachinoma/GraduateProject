using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoModel : MonoBehaviour
{
    [SerializeField] float deleteTime = 7f;

    Rigidbody rb;

    float force = 100f;
    public float Force{ get { return force; } set { force = value; } }

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);

        StartCoroutine(DestroyTimer(deleteTime));
    }

    private void Update()
    {
        transform.Rotate(new Vector3(360, 360, 360) * Time.deltaTime);
    }

    IEnumerator DestroyTimer(float _time)
    {
        yield return new WaitForSeconds(_time);

        Destroy(gameObject);
    }
}
