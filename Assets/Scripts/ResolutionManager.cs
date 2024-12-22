using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    private int targetWidth = 1920;  // Ŀ����
    private int targetHeight = 1080; // Ŀ��߶�
    private float targetAspectRatio = 16f / 9f; // Ŀ�곤���

    void Start()
    {
        SetResolutionWithAspectRatio(targetWidth, targetHeight);
    }

    public void SetResolutionWithAspectRatio(int width, int height)
    {
        float aspectRatio = (float)width / height;

        if (Mathf.Abs(aspectRatio - targetAspectRatio) < 0.01f)
        {
            Screen.SetResolution(width, height, Screen.fullScreen);
        }
        else
        {
            if (aspectRatio > targetAspectRatio)
            {
                // �����ǰ��߱ȴ���Ŀ���߱ȣ���������
                width = Mathf.RoundToInt(height * targetAspectRatio);
            }
            else
            {
                // �����ǰ��߱�С��Ŀ���߱ȣ�������߶�
                height = Mathf.RoundToInt(width / targetAspectRatio);
            }

            Screen.SetResolution(width, height, Screen.fullScreen);
        }
    }
}