using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public RectTransform door; // 拖入门按钮的 RectTransform
    public float rotationSpeed = 15f; // 门打开的速度（角度每秒）
    private bool isRotating = false; // 是否正在旋转
    private float targetRotationAngle = 60f; // 目标旋转角度
    private float currentRotationAngle = 0f; // 当前已旋转的角度

    public void RotateDoor()
    {
        Debug.Log("RotateDoor 被调用");
        isRotating = true;
        currentRotationAngle = 0f; // 重置旋转角度
    }

    void Update()
    {
        if (isRotating)
        {
            // 计算每帧旋转的角度
            float rotationAmount = rotationSpeed * Time.deltaTime;

            // 累加当前旋转角度
            currentRotationAngle += rotationAmount;

            // 限制旋转角度不能超过目标角度
            if (currentRotationAngle > targetRotationAngle)
            {
                rotationAmount -= (currentRotationAngle - targetRotationAngle); // 修正超出的部分
                isRotating = false; // 停止旋转
                Debug.Log("门已打开");
                LoadNextScene(); // 旋转完成后跳转场景
            }

            Vector3 newRotation = door.localEulerAngles;
            newRotation.y -= rotationAmount; 
            door.localEulerAngles = newRotation; // 应用旋转
        }
    }

    // 加载下一个场景的方法
    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.LogWarning("已是最后一个场景，无下一个场景可加载！");
        }
    }
}