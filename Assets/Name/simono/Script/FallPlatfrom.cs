using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FallPlatfrom : MonoBehaviour
{
    [SerializeField] float destroyTime = 1f;
    Animator animator;
    bool isReady = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && !isReady)
        {
            isReady = true;
            animator.SetTrigger("isReady");
            StartCoroutine(CallDestroy(destroyTime));
        }
    }

    IEnumerator CallDestroy(float _time)
    {
        yield return new WaitForSeconds(_time);

        Destroy(gameObject);
    }
}
