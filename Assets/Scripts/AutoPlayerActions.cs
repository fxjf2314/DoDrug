using System.Collections;
using UnityEngine;

public class AutoPlayerActions : MonoBehaviour
{
    public float shakeIntensity = 3f; // 摄像机震动强度
    public float shakeDuration = 1.5f; // 摄像机震动持续时间
    public float fallSpeed = 1f; // 玩家倒下速度
    public float getUpSpeed = 1f; // 玩家站起速度
    public float shakeFrequency = 10f; // 摄像机震动频率

    private Camera playerCamera;
    private Vector3 cameraOffset;
    private bool isFallen = false;

    private PlayerController playerController; // 引用 PlayerController

    void Start()
    {
        playerCamera = Camera.main;
        playerController = GetComponent<PlayerController>();

        if (playerCamera == null)
        {
            Debug.LogError("主摄像机未找到，请确保场景中有一个主摄像机。");
            return;
        }

        if (playerController == null)
        {
            Debug.LogError("PlayerController 未找到，请确保该脚本挂载到玩家对象上。");
            return;
        }

        cameraOffset = playerCamera.transform.localPosition;

        StartCoroutine(AutoSequence());
    }

    void Update()
    {
        if (playerController != null && !playerController.enabled)
        {
            // 玩家输入被禁用时执行必要的停止逻辑
        }
    }

    IEnumerator AutoSequence()
    {
        GetComponent<PlayerController>().finalMoveSpeed = 0;
        transform.Find("Main Camera").GetComponent<CameraController>().mouseSensitivity = 0;
        DisablePlayerControl();
        yield return StartCoroutine(ShakeCamera());
        yield return StartCoroutine(FallDown());
        yield return StartCoroutine(GetUp());
        EnablePlayerControl();
        GetComponent<PlayerController>().finalMoveSpeed = 4;
        transform.Find("Main Camera").GetComponent<CameraController>().mouseSensitivity = 500;
    }

    IEnumerator ShakeCamera()
    {
        Vector3 originalPos = playerCamera.transform.localPosition;
        float elapsedTime = 0f;
        float updateInterval = 1f / shakeFrequency;

        while (elapsedTime < shakeDuration)
        {
            float currentShakeAmount = shakeIntensity * (1 - (elapsedTime / shakeDuration));
            Vector3 randomPoint = originalPos + Random.insideUnitSphere * currentShakeAmount;
            playerCamera.transform.localPosition = randomPoint;

            yield return new WaitForSeconds(updateInterval);
            elapsedTime += updateInterval;
        }

        playerCamera.transform.localPosition = originalPos;
    }

    IEnumerator FallDown()
    {
        isFallen = true;
        Vector3 fallPosition = cameraOffset + new Vector3(0, -1.5f, 0);
        float threshold = 0.01f;

        while ((playerCamera.transform.localPosition - fallPosition).sqrMagnitude > threshold * threshold)
        {
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, fallPosition, fallSpeed * Time.deltaTime);
            yield return null;
        }

        playerCamera.transform.localPosition = fallPosition;
    }

    IEnumerator GetUp()
    {
        Vector3 standPosition = cameraOffset;
        float threshold = 0.01f;

        while ((playerCamera.transform.localPosition - standPosition).sqrMagnitude > threshold * threshold)
        {
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, standPosition, getUpSpeed * Time.deltaTime);
            yield return null;
        }

        playerCamera.transform.localPosition = standPosition;
        isFallen = false;
    }

    void DisablePlayerControl()
    {
        if (playerController != null)
        {
            playerController.enabled = false;
        }
    }

    void EnablePlayerControl()
    {
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }
}