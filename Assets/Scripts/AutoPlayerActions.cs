using System.Collections;
using UnityEngine;

public class AutoPlayerActions : MonoBehaviour
{
    public float shakeIntensity = 3f; // �������ǿ��
    public float shakeDuration = 1.5f; // ������𶯳���ʱ��
    public float fallSpeed = 1f; // ��ҵ����ٶ�
    public float getUpSpeed = 1f; // ���վ���ٶ�
    public float shakeFrequency = 10f; // �������Ƶ��

    private Camera playerCamera;
    private Vector3 cameraOffset;
    private bool isFallen = false;

    private PlayerController playerController; // ���� PlayerController

    void Start()
    {
        playerCamera = Camera.main;
        playerController = GetComponent<PlayerController>();

        if (playerCamera == null)
        {
            Debug.LogError("�������δ�ҵ�����ȷ����������һ�����������");
            return;
        }

        if (playerController == null)
        {
            Debug.LogError("PlayerController δ�ҵ�����ȷ���ýű����ص���Ҷ����ϡ�");
            return;
        }

        cameraOffset = playerCamera.transform.localPosition;

        StartCoroutine(AutoSequence());
    }

    void Update()
    {
        if (playerController != null && !playerController.enabled)
        {
            // ������뱻����ʱִ�б�Ҫ��ֹͣ�߼�
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