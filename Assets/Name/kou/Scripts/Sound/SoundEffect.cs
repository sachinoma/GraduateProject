using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [SerializeField]
    AudioSource m_AudioSource;

    public void PlaySoundEffectClip(AudioClip audioClip)
    {
        m_AudioSource.PlayOneShot(audioClip);
    }
}
