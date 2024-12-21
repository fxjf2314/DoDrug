using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Listener : MonoBehaviour
{
    protected Interactive somethingScript;

    protected virtual void Start()
    {
        

        
    }

    protected abstract void Dosomething();
}
