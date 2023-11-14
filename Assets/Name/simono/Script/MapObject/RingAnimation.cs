using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class RingAnimation : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }



    private void OnTriggerExit(Collider other)
    {
        animator.SetTrigger("RotatorTrigger");
    }

}
