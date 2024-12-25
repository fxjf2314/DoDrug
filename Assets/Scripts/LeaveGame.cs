using UnityEngine;
using UnityEngine.UI;

public class LeaveGame : MonoBehaviour
{
    public GameObject exitPanel; // �˳�ȷ����������

    void Update()
    {
        // ����ESC����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            // ��ʾ�˳�ȷ�����
            exitPanel.SetActive(true);
        }
    }

    // ȷ���˳���Ϸ
    public void OnConfirmExit()
    {
        Application.Quit();
    }

    // ȡ���˳��������˳�ȷ�����
    public void OnCancelExit()
    {
        Cursor.visible=false;
        exitPanel.SetActive(false);
    }
}