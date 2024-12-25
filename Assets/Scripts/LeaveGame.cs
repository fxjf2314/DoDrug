using UnityEngine;
using UnityEngine.UI;

public class LeaveGame : MonoBehaviour
{
    public GameObject exitPanel; // 退出确认面板的引用

    void Update()
    {
        // 监听ESC按键
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            // 显示退出确认面板
            exitPanel.SetActive(true);
        }
    }

    // 确认退出游戏
    public void OnConfirmExit()
    {
        Application.Quit();
    }

    // 取消退出，隐藏退出确认面板
    public void OnCancelExit()
    {
        Cursor.visible=false;
        exitPanel.SetActive(false);
    }
}