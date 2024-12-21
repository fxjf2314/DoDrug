using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    private int targetWidth = 1920;  // 目标宽度
    private int targetHeight = 1080; // 目标高度
    private float targetAspectRatio = 16f / 9f; // 目标长宽比

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
                // 如果当前宽高比大于目标宽高比，则调整宽度
                width = Mathf.RoundToInt(height * targetAspectRatio);
            }
            else
            {
                // 如果当前宽高比小于目标宽高比，则调整高度
                height = Mathf.RoundToInt(width / targetAspectRatio);
            }

            Screen.SetResolution(width, height, Screen.fullScreen);
        }
    }
}