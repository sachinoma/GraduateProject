using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision))]
public class ItemManager : MonoBehaviour
{
    const string PlayerTag = "Player";

    [SerializeField] GameObject destroyParticle;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool DetectPlayer(Collider _target)
    {
        return _target.CompareTag(PlayerTag);
    }

    public void CallCreateParticle()
    {
        if(destroyParticle != null)
            Instantiate(destroyParticle, transform);
    }

    public void CallPlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
            audioSource.PlayOneShot(audioSource.clip);
        
    }

    public void CallDestroy()
    {
        Destroy(gameObject);
    }
}
