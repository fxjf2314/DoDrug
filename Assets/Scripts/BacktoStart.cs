using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacktoStart : MonoBehaviour
{
    public Transform startPos;
    
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            CharacterController playerController = other.GetComponent<CharacterController>();
            playerController.enabled = false;
            other.transform.position = startPos.position;
            playerController.enabled = true;
            
        }
    }
}
