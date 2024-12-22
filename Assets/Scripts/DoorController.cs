using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public RectTransform door; // �����Ű�ť�� RectTransform
    public float rotationSpeed = 15f; // �Ŵ򿪵��ٶȣ��Ƕ�ÿ�룩
    private bool isRotating = false; // �Ƿ�������ת
    private float targetRotationAngle = 60f; // Ŀ����ת�Ƕ�
    private float currentRotationAngle = 0f; // ��ǰ����ת�ĽǶ�

    public void RotateDoor()
    {
        Debug.Log("RotateDoor ������");
        isRotating = true;
        currentRotationAngle = 0f; // ������ת�Ƕ�
    }

    void Update()
    {
        if (isRotating)
        {
            // ����ÿ֡��ת�ĽǶ�
            float rotationAmount = rotationSpeed * Time.deltaTime;

            // �ۼӵ�ǰ��ת�Ƕ�
            currentRotationAngle += rotationAmount;

            // ������ת�ǶȲ��ܳ���Ŀ��Ƕ�
            if (currentRotationAngle > targetRotationAngle)
            {
                rotationAmount -= (currentRotationAngle - targetRotationAngle); // ���������Ĳ���
                isRotating = false; // ֹͣ��ת
                Debug.Log("���Ѵ�");
                LoadNextScene(); // ��ת��ɺ���ת����
            }

            Vector3 newRotation = door.localEulerAngles;
            newRotation.y -= rotationAmount; 
            door.localEulerAngles = newRotation; // Ӧ����ת
        }
    }

    // ������һ�������ķ���
    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.LogWarning("�������һ������������һ�������ɼ��أ�");
        }
    }
}