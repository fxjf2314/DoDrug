using System.Collections;
using UnityEngine;

public class AutoPlayerActions : MonoBehaviour
{
    public float moveSpeed = 10f;           // 角色的移动速度
    public float shakeIntensity = 0.1f;      // 视角摇晃的强度
    public float shakeDuration = 0.5f;       // 视角摇晃持续时间
    public float fallSpeed = 1f;             // 倒下的速度
    public float getUpSpeed = 1f;            // 起立的速度
    public float shakeFrequency = 0.5f;

    private Rigidbody rb;
    private Camera playerCamera;
    private Vector3 originalCameraPosition;
    private Vector3 moveDirection;
    private bool isFallen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        originalCameraPosition = playerCamera.transform.localPosition;

        // 自动执行所有动作
        StartCoroutine(AutoSequence());
    }

    IEnumerator AutoSequence()
    {
        // 1. 快速移动
        StartCoroutine(MovePlayer());
        yield return new WaitForSeconds(0.5f); // 等待 2 秒

        // 2. 视角摇晃
        StartCoroutine(ShakeCamera());
        yield return new WaitForSeconds(shakeDuration); // 等待摇晃持续时间

        // 3. 倒下
        StartCoroutine(FallDown());
        yield return new WaitForSeconds(2.5f); // 等待 2 秒，让倒下动作完成

        // 4. 起立
        StartCoroutine(GetUp());
        yield return new WaitForSeconds(5f);
    }

    // 控制玩家移动
    IEnumerator MovePlayer()
    {
        // 移动的方向
        moveDirection = new Vector3(1, 0, 0).normalized; // 向右移动

        // 执行移动
        while (true)
        {
            if (isFallen) break; // 如果倒下则停止移动
            rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
            yield return null;
        }
    }

    // 视角摇晃
    IEnumerator ShakeCamera()
    {
        Vector3 originalPos = playerCamera.transform.localPosition;      // 获取初始位置
        float remainingShakeDuration = shakeDuration;                     // 剩余的摇晃时间
        float remainingShakeIntensity = shakeIntensity;                   // 初始强度

        // 频率控制，仍然按指定频率进行更新
        float shakeInterval = 1f / shakeFrequency;
        float timeSinceLastShake = 0f;

        // 用于平滑插值的变量
        Vector3 currentShakePosition = originalPos;                      // 当前摇晃位置
        Vector3 targetShakePosition = originalPos;                       // 目标摇晃位置

        // 使摇晃更平滑
        while (remainingShakeDuration > 0)
        {
            timeSinceLastShake += Time.deltaTime;

            // 如果已经到达更新频率的时机
            if (timeSinceLastShake >= shakeInterval)
            {
                // 根据剩余时间动态调整摇晃强度
                float currentShakeAmount = remainingShakeIntensity * (remainingShakeDuration / shakeDuration);

                // 使用Random.insideUnitSphere创建一个新的目标摇晃位置
                targetShakePosition = originalPos + Random.insideUnitSphere * currentShakeAmount;

                // 使用 Lerp 或 SmoothDamp 来平滑过渡到目标位置
                currentShakePosition = Vector3.Lerp(currentShakePosition, targetShakePosition, 0.2f);

                // 重置时间
                timeSinceLastShake = 0f;
            }

            // 更新摄像机位置为平滑过渡后的结果
            playerCamera.transform.localPosition = currentShakePosition;

            // 减少剩余时间
            remainingShakeDuration -= Time.deltaTime;

            yield return null;
        }

        // 最后恢复到原始位置
        playerCamera.transform.localPosition = originalPos;
    }
    // 倒下动作
    IEnumerator FallDown()
    {
        isFallen = true;
        Vector3 fallPosition = playerCamera.transform.position + new Vector3(0, -2, 0);

        while (playerCamera.transform.position.y > fallPosition.y)
        {
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, fallPosition, fallSpeed * Time.deltaTime);
            yield return null;
        }

        playerCamera.transform.position = new Vector3(17, 2.6f, 21);
    }

    // 起立动作
    IEnumerator GetUp()
    {
        isFallen = false;

        // 设置恢复的目标位置
        Vector3 standPosition = new Vector3(2, 0, 0);
        // Vector3 currentPosition = playerCamera.transform.position;

        // 防止“突然下降”的情况，先平滑调整 y 轴的目标
        //while (currentPosition.y < standPosition.y)
        //{
        //    currentPosition.y = Mathf.MoveTowards(currentPosition.y, standPosition.y, getUpSpeed * Time.deltaTime);
        //   playerCamera.transform.position = currentPosition;
        //yield return null;
        //  }

        // 最后确保y轴位置已经完全恢复
        //currentPosition.y = standPosition.y;
        playerCamera.transform.position = standPosition;
        Debug.Log("已启用起立");
        yield return null;
    }
}