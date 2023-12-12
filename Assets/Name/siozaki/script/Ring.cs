using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider))]
public class Ring : MonoBehaviour
{
    [SerializeField]
    private GameObject RingEffect;
    Animator animator;

    private GameManager gameManager;
    [SerializeField]
    private ResultData[] resultData;

    private void Start()
    {
        animator = GetComponent<Animator>();
        GameObject playerInputManager = GameObject.Find("PlayerInputManager");
        gameManager = playerInputManager.GetComponent<GameManager>();
        resultData = GameManager.Instance.GetResultData().ToArray();
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Ring"))
        {
            if (other.CompareTag("Player"))
            {
                PlayerStatus playerStatus = other.gameObject.GetComponent<PlayerStatus>();
                int playerNum = playerStatus.GetPlayerNum();
                resultData[playerNum].AddRingNum(1);

                //TMP ÉVÅ[ÉìëJà⁄ÇÃëOÇ…àÍâÒÇæÇØÇ≈Ç¢Ç¢
                gameManager.SetMode(GameManager.Mode.Ring);

                animator.SetTrigger("RotatorTrigger");
                Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
                Instantiate(RingEffect, hitPos, Quaternion.identity);
            }
        }
        else if (this.CompareTag("GoldRing"))
        {
            if (other.CompareTag("Player"))
            {
                PlayerStatus playerStatus = other.gameObject.GetComponent<PlayerStatus>();
                int playerNum = playerStatus.GetPlayerNum();
                resultData[playerNum].AddRingNum(3);

                animator.SetTrigger("RotatorTrigger");
                Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
                Instantiate(RingEffect, hitPos, Quaternion.identity);
            }
        }
    }
}
