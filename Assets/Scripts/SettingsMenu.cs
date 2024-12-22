using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // �������˵������ò˵���Canvas
    public GameObject mainMenuCanvas;
    public GameObject settingsMenuCanvas;

    // ��������
    public Slider volumeSlider;

    // �ֱ���ѡ��TMP_Dropdown��
    public TMP_Dropdown resolutionDropdown;

    // ȫ���л���Toggle��
    public Toggle fullscreenToggle;  // ʹ��ԭ�� Toggle

    void Start()
    {
        // ��ʼ����������
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // ��ʼ��ȫ��ģʽ
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(ToggleFullscreen);

        // ��ʼ���ֱ���ѡ��
        resolutionDropdown.ClearOptions();
        PopulateResolutionOptions();
    }

    // ��������
    void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    // �л�ȫ��ģʽ
    void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // ���ֱ���������
    void PopulateResolutionOptions()
    {
        Resolution[] resolutions = Screen.resolutions;
        var options = new System.Collections.Generic.List<string>();

        foreach (var res in resolutions)
        {
            options.Add(res.width + "x" + res.height);
        }

        resolutionDropdown.AddOptions(options);

        // ����Ϊ��ǰ�ֱ���
        int currentResolutionIndex = GetCurrentResolutionIndex();
        resolutionDropdown.value = currentResolutionIndex;
    }

    // ��ȡ��ǰ�ֱ�������
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

        return 0; // Ĭ��ѡ���һ��
    }

    // ѡ��ֱ���
    public void SetResolution(int index)
    {
        Resolution selectedResolution = Screen.resolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }

    // �������˵�
    public void OnBackButtonClick()
    {
        // �������ò˵�����ʾ���˵�
        settingsMenuCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}