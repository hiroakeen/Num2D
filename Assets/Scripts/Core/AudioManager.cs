using UnityEngine;

/// <summary>
/// 各シーンで指定したBGMを再生する簡易AudioManager。
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Header("BGM設定")]
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
    /// 任意のBGMを再生
    /// </summary>
    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    /// <summary>
    /// BGM停止
    /// </summary>
    public void StopBGM()
    {
        audioSource.Stop();
    }
}
