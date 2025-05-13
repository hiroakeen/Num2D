using UnityEngine;

/// <summary>
/// Escapeキーでアプリを終了させるシングルトン
/// </summary>
public class AppExitManager : MonoBehaviour
{
    public static AppExitManager Instance { get; private set; }

    private void Awake()
    {
        // シングルトン化（重複防止）
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // シーン間で保持
    }

    private void Update()
    {
        // エスケープキー（Androidでは戻るボタン）
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void QuitGame()
    {
        Debug.Log("アプリ終了");

        // Unity Editorでは停止
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
