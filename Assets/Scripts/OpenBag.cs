using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class OpenBag : MonoBehaviour
{
    public GameObject mybag;
    bool isOpen;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isOpen = mybag.activeSelf;
        OpenMyBag();
        GetComponent<PlayerController>().enabled = !isOpen;
        transform.Find("Main Camera").GetComponent<CameraController>().enabled = !isOpen;
    }

    void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOpen = !mybag.activeSelf; 
            mybag.SetActive(isOpen);  
        }
    }
}
