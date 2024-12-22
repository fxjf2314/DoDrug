using System.Collections;
using UnityEngine;

public class AutoPlayerActions : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float shakeIntensity = 0.1f;
    public float shakeDuration = 0.5f;
    public float fallSpeed = 1f;
    public float getUpSpeed = 1f;
    public float shakeFrequency = 0.5f;

    private Rigidbody rb;
    private Camera playerCamera;
    private Vector3 cameraOffset;
    private bool isFallen = false;
    private bool allowInput = false; // �������봦��ı�־

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        playerCamera.transform.SetParent(this.transform); // ���������Ϊ��ҵ��Ӷ���
        cameraOffset = playerCamera.transform.localPosition; // ��¼��ʼ���ƫ��

        StartCoroutine(AutoSequence());
    }

    void Update()
    {
        if (allowInput && !isFallen)
        {
            ProcessInput();
        }
    }

    void ProcessInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
    }

    IEnumerator AutoSequence()
    {
        Debug.Log("��ʼЭ��");
        Debug.Log("���ü���");
        // ��������
        allowInput = false;

        StartCoroutine(MovePlayer());
        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(ShakeCamera());
        yield return new WaitForSeconds(shakeDuration);

        Debug.Log("��ʼ����");
        yield return StartCoroutine(FallDown());

        Debug.Log("��ʼ����");
        yield return StartCoroutine(GetUp());

        // ��������
        allowInput = true;
        Debug.Log("ȡ������");
    }

    IEnumerator MovePlayer()
    {
        Vector3 moveDirection = new Vector3(1, 0, 0).normalized;

        while (!isFallen)
        {
            rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
            yield return null;
        }
    }

    IEnumerator ShakeCamera()
    {
        Vector3 originalPos = playerCamera.transform.localPosition;
        float shakeTime = 0f;

        while (shakeTime < shakeDuration)
        {
            float currentShakeAmount = shakeIntensity * (1 - (shakeTime / shakeDuration));
            Vector3 randomPoint = originalPos + Random.insideUnitSphere * currentShakeAmount;
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, randomPoint, Time.deltaTime * shakeFrequency);

            shakeTime += Time.deltaTime;
            yield return null;
        }

        playerCamera.transform.localPosition = originalPos;
    }

    IEnumerator FallDown()
    {
        isFallen = true;
        Vector3 fallPosition = cameraOffset + new Vector3(0, -2, 0);
        float threshold = 0.01f; // ���һ��С��ƫ��ֵ

        while ((playerCamera.transform.localPosition - fallPosition).sqrMagnitude > threshold * threshold)
        {
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, fallPosition, fallSpeed * Time.deltaTime);
            yield return null;
        }

        playerCamera.transform.localPosition = fallPosition;
        Debug.Log("Falllllllllll");
    }

    IEnumerator GetUp()
    {
        Vector3 standPosition = cameraOffset;
        float threshold = 0.01f; // ���һ��С��ƫ��ֵ

        while ((playerCamera.transform.localPosition - standPosition).sqrMagnitude > threshold * threshold)
        {
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, standPosition, getUpSpeed * Time.deltaTime);
            yield return null;
        }

        playerCamera.transform.localPosition = standPosition;
        isFallen = false;
        Debug.Log("getuuuuuuuuuuup!");
    }
}