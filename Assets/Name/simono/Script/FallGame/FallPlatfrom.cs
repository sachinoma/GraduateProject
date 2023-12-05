using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FallPlatfrom : MonoBehaviour
{
    [SerializeField] float destroyTime = 1f;
    [SerializeField] Animator animator;
    const string PlayerTag = "Player";

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == PlayerTag)
        {
            animator.SetTrigger("isReady");
            StartCoroutine(CallDestroy(destroyTime));
        }
    }

    IEnumerator CallDestroy(float _time)
    {
        yield return new WaitForSeconds(_time);
        
        Destroy(transform.parent.gameObject);
    }
}
