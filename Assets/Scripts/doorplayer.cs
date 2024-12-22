using UnityEngine;
using UnityEngine.SceneManagement;

public class doorplayer : MonoBehaviour
{
    public AudioSource newAudioSource;    // �µ���Ƶ������
    public AudioClip newAudioClip;        // �µ���Ƶ�ļ�

    void Start()
    {
        if (newAudioSource == null)
        {
            Debug.LogError("AudioSource δ���ã�");
        }

        // ���������л��¼�
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnDestroy()
    {
        // �Ƴ������л��¼��ļ���
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    // ��ť�����ʱ����
    public void PlayNewAudio()
    {
        if (newAudioSource == null || newAudioClip == null)
        {
            Debug.LogError("AudioSource �� AudioClip δ���ã�");
            return;
        }

        // ������Ƶ������
        newAudioSource.clip = newAudioClip;
        newAudioSource.Play();
    }

    // ����ж��ʱֹͣ��Ƶ����
    private void OnSceneUnloaded(Scene currentScene)
    {
        if (newAudioSource != null && newAudioSource.isPlaying)
        {
            newAudioSource.Stop();
        }
    }
}