using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;

public class OpenBag : MonoBehaviour
{
    public GameObject mybag;
    bool isOpen;
    float finalMoveSpeed;
    float mouseSensitivity;
    // Start is called before the first frame update
    void Start()
    {
        finalMoveSpeed = GetComponent<PlayerController>().finalMoveSpeed;
        mouseSensitivity = transform.Find("Main Camera").GetComponent<CameraController>().mouseSensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        isOpen = mybag.activeSelf;
        OpenMyBag();
        if (!isOpen)
        {
            GetComponent<PlayerController>().finalMoveSpeed = finalMoveSpeed;
            transform.Find("Main Camera").GetComponent<CameraController>().mouseSensitivity = mouseSensitivity;
        }
        else
        {
            GetComponent<PlayerController>().finalMoveSpeed = 0;
            transform.Find("Main Camera").GetComponent<CameraController>().mouseSensitivity = 0;
        }
    }

    void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOpen = !mybag.activeSelf;
            mybag.SetActive(isOpen);
            Cursor.visible = isOpen;
        }
    }
}
