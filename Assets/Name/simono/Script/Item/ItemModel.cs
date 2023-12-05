using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision))]
public class ItemModel : MonoBehaviour
{
    [SerializeField] GameObject destroyParticle;
    [SerializeField] private bool isRandom = true;
    public bool IsRandom { get { return isRandom; } set { isRandom = value; } }
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool DetectPlayer(Collider _target)
    {
        return _target.CompareTag(Tag.PlayerTag);
    }

    public void CallCreateParticle()
    {
        if (destroyParticle != null)
            Instantiate(destroyParticle, this.transform.position, Quaternion.identity);
    }

    public void CallPlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
            audioSource.PlayOneShot(audioSource.clip);
        
    }

    public void CallRespawn()
    {
        ItemManager.Instance.Respawn(this.gameObject);
    }
}
