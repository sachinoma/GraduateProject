using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOutfitObject : MonoBehaviour
{
    [SerializeField]
    private SoundEffect soundEffect;
    [SerializeField]
    private AudioClip clip;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<GameMessageReceiver>() != null)
            {
                other.gameObject.GetComponent<GameMessageReceiver>().ChangeOutfit();
                soundEffect.PlaySoundEffectClip(clip);
            }
        }
    }
}
