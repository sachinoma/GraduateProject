using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [SerializeField]
    AudioSource m_AudioSource;
    [SerializeField]
    AudioClip m_Clip;

    public void PlaySoundEffect()
    {
        m_AudioSource.PlayOneShot(m_Clip);
    }

    public void PlaySoundEffectClip(AudioClip audioClip)
    {
        m_AudioSource.PlayOneShot(audioClip);
    }
}
