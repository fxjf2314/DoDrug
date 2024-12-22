using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Listener : MonoBehaviour
{
    protected Interactive somethingScript;
    protected GameObject objWithInteractive;
    protected virtual void Start()
    {
        objWithInteractive = GameObject.Find("Main Camera");

        
    }

    protected abstract void Dosomething();
}
