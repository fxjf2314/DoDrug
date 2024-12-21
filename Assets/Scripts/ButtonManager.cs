using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    public static void UpdateButtonOutline(Button button, bool enableOutline)
    {
        if (button == null) return;

        Image buttonImage = button.targetGraphic as Image;
        if (buttonImage != null && buttonImage.GetComponent<Outline>() != null)
        {
            buttonImage.GetComponent<Outline>().enabled = enableOutline;
        }
    }

    public void OnButtonSelected(Button button)
    {
        foreach (Button b in FindObjectsOfType<Button>())
        {
            UpdateButtonOutline(b, false);
        }
        UpdateButtonOutline(button, true);
    }
}
