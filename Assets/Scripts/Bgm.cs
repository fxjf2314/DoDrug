using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bgm : MonoBehaviour
{
    public Slider slider;
    public AudioSource backgroundMusic;

    private static Bgm instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���ֶ����ڼ����³���ʱ��������
        }
        else if (instance != this)
        {
            Destroy(gameObject); // �����´������ظ�ʵ��
            return; // ȷ���������벻��ִ��
        }

        // ��ʼ������
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
        PlayerPrefs.SetFloat("MusicVolume", slider.value); // ������������
    }
}