using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bjm : MonoBehaviour
{
    public Slider slider;
    public AudioSource backgroundMusic;

    private static Bjm instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 保持对象在加载新场景时不被销毁
        }
        else if (instance != this)
        {
            Destroy(gameObject); // 销毁新创建的重复实例
            return; // 确保后续代码不会执行
        }

        // 初始化音量
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume");
            backgroundMusic.volume = savedVolume;
            slider.value = savedVolume;
        }
        else
        {
            backgroundMusic.volume = slider.value;
        }
    }

    public void Volume()
    {
        backgroundMusic.volume = slider.value;
        PlayerPrefs.SetFloat("MusicVolume", slider.value); // 保存音量设置
    }
}