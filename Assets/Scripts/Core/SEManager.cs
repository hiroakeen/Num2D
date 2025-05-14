using UnityEngine;

/// <summary>
/// 汎用的なSE再生管理。インスペクターでSEを設定して、静的メソッドで再生可能。
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SEManager : MonoBehaviour
{
    public static SEManager Instance { get; private set; }

    [Header("効果音一覧")]
    public AudioClip touchSE;
    public AudioClip missSE;
    public AudioClip successSE;
    public AudioClip clickSE;
    public AudioClip rankInSE;
    public AudioClip newRecordSE;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 必要に応じて削除可
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    // 共通の再生関数（簡易）
    private void Play(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // 以下は用途ごとに呼び出す用メソッド
    public static void PlayTouch() => Instance?.Play(Instance.touchSE);
    public static void PlayMiss() => Instance?.Play(Instance.missSE);
    public static void PlaySuccess() => Instance?.Play(Instance.successSE);
    public static void PlayClick() => Instance?.Play(Instance.clickSE);
    public static void PlayRankIn() => Instance?.Play(Instance.rankInSE);
    public static void PlayNewRecord() => Instance?.Play(Instance.newRecordSE);
}
