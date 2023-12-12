using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider))]
public class Ring : MonoBehaviour
{
    [SerializeField]
    private GameObject RingEffect;
    Animator animator;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Ring"))
        {
            if (other.CompareTag("Player"))
            {
                animator.SetTrigger("RotatorTrigger");
                Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
                Instantiate(RingEffect, hitPos, Quaternion.identity);
            }
        }
        else if (this.CompareTag("GoldRing"))
        {
            if (other.CompareTag("Player"))
            {
                animator.SetTrigger("RotatorTrigger");
                Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
                Instantiate(RingEffect, hitPos, Quaternion.identity);
            }
        }
    }
}
