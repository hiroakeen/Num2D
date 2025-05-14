using UnityEngine;

/// <summary>
/// �e�V�[���Ŏw�肵��BGM���Đ�����Ȉ�AudioManager�B
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Header("BGM�ݒ�")]
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private float volume = 0.6f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }

    private void Start()
    {
        if (bgmClip != null)
        {
            PlayBGM(bgmClip);
        }
    }

    /// <summary>
    /// �C�ӂ�BGM���Đ�
    /// </summary>
    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    /// <summary>
    /// BGM��~
    /// </summary>
    public void StopBGM()
    {
        audioSource.Stop();
    }
}
