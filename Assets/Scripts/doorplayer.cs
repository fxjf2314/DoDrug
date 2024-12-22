using UnityEngine;
using UnityEngine.SceneManagement;

public class doorplayer : MonoBehaviour
{
    public AudioSource newAudioSource;    // 新的音频播放器
    public AudioClip newAudioClip;        // 新的音频文件

    void Start()
    {
        if (newAudioSource == null)
        {
            Debug.LogError("AudioSource 未设置！");
        }

        // 监听场景切换事件
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnDestroy()
    {
        // 移除场景切换事件的监听
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    // 按钮被点击时调用
    public void PlayNewAudio()
    {
        if (newAudioSource == null || newAudioClip == null)
        {
            Debug.LogError("AudioSource 或 AudioClip 未设置！");
            return;
        }

        // 设置音频并播放
        newAudioSource.clip = newAudioClip;
        newAudioSource.Play();
    }

    // 场景卸载时停止音频播放
    private void OnSceneUnloaded(Scene currentScene)
    {
        if (newAudioSource != null && newAudioSource.isPlaying)
        {
            newAudioSource.Stop();
        }
    }
}