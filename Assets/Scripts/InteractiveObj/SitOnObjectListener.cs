using UnityEngine;

public class SitOnObjectListener : Listener
{
    public Transform player; // ��Ҷ���
    public Camera playerCamera; // ��������
    private Interactive interactive; // ���� Interactive �ű�
    private bool isSitting = false; // ����Ƿ��Ѿ�����

    protected override void Start()
    {
        base.Start();
        interactive = FindObjectOfType<Interactive>(); // ���賡������ Interactive �ű�
        if (interactive != null)
        {
            interactive.eventSitOnObject += Dosomething;
        }
    }

    protected override void Dosomething()
    {
            if (isSitting)
            {
                StandUp(); // �����������£���ִ��վ�����߼�
            }
            else if (PickAndInteractiveFather.pickObj != null &&PickAndInteractiveFather.pickObj.CompareTag("InteractiveObj"))
            {
                SitDown(); 
            }
    }

    private void SitDown()
    {
        if (player == null || playerCamera == null)
        {
            Debug.LogError("Player or PlayerCamera is null! Please assign them in the Inspector.");
            return;
        }

        isSitting = true; // ������Ϊ����״̬
        Debug.Log("Player is now sitting.");

        // �������λ�ú���ת
        player.position = new Vector3(21.5343f, 1.08f, -23.385f);
        player.rotation = Quaternion.Euler(0, -122.188f, 0);

        // ���������ת
        playerCamera.transform.rotation = Quaternion.Euler(42.763f, 0, 0);

        // ��������ƶ����������ӽ�
        DisablePlayerControls();
    }

    private void StandUp()
    {
        if (player == null || playerCamera == null)
        {
            Debug.LogError("Player or PlayerCamera is null! Please assign them in the Inspector.");
            return;
        }

        isSitting = false; // ������Ϊվ��״̬
        Debug.Log("Player has stood up.");

        // ��������ƶ����ӽǿ���
        EnablePlayerControls();
    }

    private void DisablePlayerControls()
    {
        // ������ҿ��ƽű��� PlayerController
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false; // ��������ƶ�
        }

        // ��������������Ľű��� MouseLook
        transform.Find("Main Camera").GetComponent<CameraController>().mouseSensitivity = 0;
    }

    private void EnablePlayerControls()
    {
        // �ָ�����ƶ�
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // �ָ��������ӽ�
        transform.Find("Main Camera").GetComponent<CameraController>().mouseSensitivity = 500;
    }
}