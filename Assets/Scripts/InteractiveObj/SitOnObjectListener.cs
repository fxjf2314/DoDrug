using UnityEngine;

public class SitOnObjectListener : Listener
{
    public Transform player; // 玩家对象
    public Camera playerCamera; // 玩家摄像机
    private Interactive interactive; // 引用 Interactive 脚本
    private bool isSitting = false; // 玩家是否已经坐下

    protected override void Start()
    {
        base.Start();
        interactive = FindObjectOfType<Interactive>(); // 假设场景中有 Interactive 脚本
        if (interactive != null)
        {
            interactive.eventSitOnObject += Dosomething;
        }
    }

    protected override void Dosomething()
    {
            if (isSitting)
            {
                StandUp(); // 如果玩家已坐下，则执行站起来逻辑
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

        isSitting = true; // 标记玩家为坐下状态
        Debug.Log("Player is now sitting.");

        // 设置玩家位置和旋转
        player.position = new Vector3(21.5343f, 1.08f, -23.385f);
        player.rotation = Quaternion.Euler(0, -122.188f, 0);

        // 设置相机旋转
        playerCamera.transform.rotation = Quaternion.Euler(42.763f, 0, 0);

        // 禁用玩家移动和鼠标控制视角
        DisablePlayerControls();
    }

    private void StandUp()
    {
        if (player == null || playerCamera == null)
        {
            Debug.LogError("Player or PlayerCamera is null! Please assign them in the Inspector.");
            return;
        }

        isSitting = false; // 标记玩家为站立状态
        Debug.Log("Player has stood up.");

        // 允许玩家移动和视角控制
        EnablePlayerControls();
    }

    private void DisablePlayerControls()
    {
        // 假设玩家控制脚本是 PlayerController
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false; // 禁用玩家移动
        }

        // 假设鼠标控制相机的脚本是 MouseLook
        transform.Find("Main Camera").GetComponent<CameraController>().mouseSensitivity = 0;
    }

    private void EnablePlayerControls()
    {
        // 恢复玩家移动
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // 恢复鼠标控制视角
        transform.Find("Main Camera").GetComponent<CameraController>().mouseSensitivity = 500;
    }
}