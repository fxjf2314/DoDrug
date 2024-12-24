using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneTrigger : MonoBehaviour
{
    public TextMeshProUGUI Triggertext;

    Color color;


    bool fademode = false;
    // Start is called before the first frame update
    void Start()
    {


        color = Triggertext.color;

        color.a = 0;
        Triggertext.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (color.a == 1)
        {
            fademode = true;
            Fadein();
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fademode = false;
            Fadeout();
            
        }
    }

    private void Fadein()
    {


        if (color.a > 0)
        {

            color.a = color.a - 0.04f;
            Triggertext.color = color;
            Invoke("Fadein", 0.03f);
        }

    }

    private void Fadeout()
    {
        if (color.a < 1.2 && !fademode)
        {
            Triggertext.text = transform.name;

            color.a = color.a + 0.04f;
            Triggertext.color = color;
            Invoke("Fadeout", 0.03f);
        }
        if (color.a >= 1)
        {
            color.a = 1;
        }

    }
}