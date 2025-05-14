using UnityEngine;

/// <summary>
/// �ėp�I��SE�Đ��Ǘ��B�C���X�y�N�^�[��SE��ݒ肵�āA�ÓI���\�b�h�ōĐ��\�B
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SEManager : MonoBehaviour
{
    public static SEManager Instance { get; private set; }

    [Header("���ʉ��ꗗ")]
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
            DontDestroyOnLoad(gameObject); // �K�v�ɉ����č폜��
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

    // ���ʂ̍Đ��֐��i�ȈՁj
    private void Play(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // �ȉ��͗p�r���ƂɌĂяo���p���\�b�h
    public static void PlayTouch() => Instance?.Play(Instance.touchSE);
    public static void PlayMiss() => Instance?.Play(Instance.missSE);
    public static void PlaySuccess() => Instance?.Play(Instance.successSE);
    public static void PlayClick() => Instance?.Play(Instance.clickSE);
    public static void PlayRankIn() => Instance?.Play(Instance.rankInSE);
    public static void PlayNewRecord() => Instance?.Play(Instance.newRecordSE);
}
