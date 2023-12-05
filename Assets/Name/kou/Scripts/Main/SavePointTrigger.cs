using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform savePos;

    [SerializeField]
    private SoundEffect soundEffect;

    [SerializeField]
    private AudioClip clip;

    [SerializeField]
    private GameObject saveEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<GameMessageReceiver>() != null)
            {
                other.gameObject.GetComponent<GameMessageReceiver>().SetSavePoint(savePos);
                soundEffect.PlaySoundEffectClip(clip);

                Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
                Instantiate(saveEffect, hitPos, Quaternion.identity);
            }
        }
    }
}
