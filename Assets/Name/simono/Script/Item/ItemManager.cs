using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision))]
public class ItemManager : MonoBehaviour
{
    static string PlayerTag = "Player";

    [SerializeField] float respawnTime = 5f;
    [SerializeField] GameObject destroyParticle;
    private Renderer[] renderlist;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        renderlist =  transform.GetComponentsInChildren<Renderer>();
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

    public void CallRespawn()
    {
        StartCoroutine(Respawn(respawnTime, gameObject));
    }

    private IEnumerator Respawn(float _time, GameObject gameObject)
    {
        foreach(var render in renderlist)
            render.enabled = false;

        yield return new WaitForSecondsRealtime(_time);

        foreach (var render in renderlist)
            render.enabled = true;
    }
}
