using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObject : MonoBehaviour
{
    [SerializeField]
    private float power;
    private GameObject MyPlayer;

    [SerializeField]
    private bool isAnimator = false;
    [SerializeField]
    Animator animator;

    [SerializeField]
    SoundEffect soundEffect;
    [SerializeField]
    AudioClip clip;

    [SerializeField]
    private float timer = 0.0f;
    [SerializeField]
    private float timerLimit = 0.3f;

    private void Awake()
    {
        if(GetComponent<Collider>().isTrigger)
            MyPlayer = transform.parent.gameObject;

        timer = timerLimit;
    }

    private void Update()
    {
        if(timer < timerLimit)
        {
            timer += Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(other.gameObject.GetComponent<GameMessageReceiver>() != null)
            {   
                if(isAnimator)
                {
                    animator.SetTrigger("Bounce");
                }
                if(clip != null)
                {
                    soundEffect.PlaySoundEffectClip(clip);
                }
                Vector3 hitPos = other.contacts[0].point;
                Vector3 forceDir = other.gameObject.transform.position - hitPos;
                forceDir.y += 1.0f;
                forceDir = forceDir.normalized;

                other.gameObject.GetComponent<GameMessageReceiver>().BounceAction(forceDir, power);
            }
            else if(other.gameObject.GetComponent<ResultCharacter>() != null)
            {
                Vector3 hitPos = other.contacts[0].point;
                Vector3 forceDir = other.gameObject.transform.position - hitPos;
                forceDir.y += 1.0f;
                forceDir = forceDir.normalized;

                other.gameObject.GetComponent<ResultCharacter>().BounceAction(forceDir, power);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && other.gameObject != MyPlayer)
        {
            if(other.gameObject.GetComponent<GameMessageReceiver>() != null)
            {
                if (clip != null)
                {
                    if (timer >= timerLimit)
                    {
                        timer = 0.0f;
                        soundEffect.PlaySoundEffectClip(clip);
                    }
                }
                Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
                Vector3 forceDir = other.gameObject.transform.position - hitPos;
                forceDir.y += 1.0f;
                forceDir = forceDir.normalized;

                other.gameObject.GetComponent<GameMessageReceiver>().BounceAction(forceDir, power);
            }
        }
    }
}
