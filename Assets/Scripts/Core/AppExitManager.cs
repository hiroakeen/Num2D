using UnityEngine;

/// <summary>
/// Escape�L�[�ŃA�v�����I��������V���O���g��
/// </summary>
public class AppExitManager : MonoBehaviour
{
    public static AppExitManager Instance { get; private set; }

    private void Awake()
    {
        // �V���O���g�����i�d���h�~�j
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // �V�[���Ԃŕێ�
    }

    private void Update()
    {
        // �G�X�P�[�v�L�[�iAndroid�ł͖߂�{�^���j
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void QuitGame()
    {
        Debug.Log("�A�v���I��");

        // Unity Editor�ł͒�~
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
