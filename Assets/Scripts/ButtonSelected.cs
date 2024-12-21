using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelected : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(() => ButtonManager.instance.OnButtonSelected(button));
    }
}
