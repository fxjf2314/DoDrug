using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // 引用主菜单和设置菜单的Canvas
    public GameObject mainMenuCanvas;
    public GameObject settingsMenuCanvas;

    // 音量控制
    public Slider volumeSlider;

    // 分辨率选择（TMP_Dropdown）
    public TMP_Dropdown resolutionDropdown;

    // 全屏切换（Toggle）
    public Toggle fullscreenToggle;  // 使用原生 Toggle

    void Start()
    {
        // 初始化音量控制
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // 初始化全屏模式
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(ToggleFullscreen);

        // 初始化分辨率选择
        resolutionDropdown.ClearOptions();
        PopulateResolutionOptions();
    }

    // 设置音量
    void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    // 切换全屏模式
    void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // 填充分辨率下拉框
    void PopulateResolutionOptions()
    {
        Resolution[] resolutions = Screen.resolutions;
        var options = new System.Collections.Generic.List<string>();

        foreach (var res in resolutions)
        {
            options.Add(res.width + "x" + res.height);
        }

        resolutionDropdown.AddOptions(options);

        // 设置为当前分辨率
        int currentResolutionIndex = GetCurrentResolutionIndex();
        resolutionDropdown.value = currentResolutionIndex;
    }

    // 获取当前分辨率索引
    int GetCurrentResolutionIndex()
    {
        Resolution currentResolution = Screen.currentResolution;
        Resolution[] resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == currentResolution.width && resolutions[i].height == currentResolution.height)
            {
                return i;
            }
        }

        return 0; // 默认选择第一个
    }

    // 选择分辨率
    public void SetResolution(int index)
    {
        Resolution selectedResolution = Screen.resolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }

    // 返回主菜单
    public void OnBackButtonClick()
    {
        // 隐藏设置菜单，显示主菜单
        settingsMenuCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}