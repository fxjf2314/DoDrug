using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // �������˵������ò˵���Canvas
    public GameObject mainMenuCanvas;
    public GameObject settingsMenuCanvas;

    // ���ð�ť����¼�
    public void OnSettingsButtonClick()
    {
        // �������˵�����ʾ���ò˵�
        mainMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(true);
    }

}