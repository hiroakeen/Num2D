using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private UIFader fader;

    public void StartGame()
    {
        fader.FadeOutAndLoadScene("MainScene", 1f);
    }
}
