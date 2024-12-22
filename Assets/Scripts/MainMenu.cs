using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // 引用主菜单和设置菜单的Canvas
    public GameObject mainMenuCanvas;
    public GameObject settingsMenuCanvas;

    // 设置按钮点击事件
    public void OnSettingsButtonClick()
    {
        // 隐藏主菜单，显示设置菜单
        mainMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(true);
    }

}