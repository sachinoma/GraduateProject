using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DropDown : MonoBehaviour
{
    [SerializeField] private OnePlayerGameManager gameManager;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            //•‰‚¯ˆ—
            gameManager.GameOver();
        }

    }
}
