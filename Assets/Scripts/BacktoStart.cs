using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacktoStart : MonoBehaviour
{
    public Transform startPos;
    public Transform finalPos;
    private PickUp finalObj;

    private void Start()
    {
        finalObj = GameObject.Find("Main Camera").GetComponent<PickUp>();
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player") )
        {
            if(finalObj.handObj == null || finalObj.handObj.name != "Tea")
            {
                CharacterController playerController = other.GetComponent<CharacterController>();
                playerController.enabled = false;
                other.transform.position = startPos.position;
                playerController.enabled = true;
            }
            else
            {
                CharacterController playerController = other.GetComponent<CharacterController>();
                playerController.enabled = false;
                other.transform.position = finalPos.position;
                playerController.enabled = true;
            }
            
            
        }
    }
}
