using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision))]
public class ItemManager : MonoBehaviour
{
    const string PlayerTag = "Player";

    public GameObject ParticleObject { get; set; }
    public AudioSource AudioManager { get; set; }

    public ItemManager(AudioSource _source = null, GameObject _gameObject = null)
    {
        AudioManager = _source;
        ParticleObject = _gameObject;
    }

    public bool DetectPlayer(GameObject _target)
    {
        return _target.CompareTag(PlayerTag);
    }

    public void CallCreateParticle()
    {
        if(ParticleObject != null)
            Instantiate(ParticleObject, transform);
    }

    public void CallPlaySound()
    {
        if (AudioManager != null)
            AudioManager.PlayOneShot(AudioManager.clip);
        
    }

    public void CallDestroy()
    {
        Destroy(gameObject);
    }
}
