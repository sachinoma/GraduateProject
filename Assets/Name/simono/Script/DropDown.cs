using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DropDown : MonoBehaviour
{
    [SerializeField] private OnePlayerGameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //•‰‚¯ˆ—
            gameManager.GameOver();
        }

    }
}
