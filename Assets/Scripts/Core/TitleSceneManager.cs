using UnityEngine;
using TMPro;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;

    void Start()
    {
        int topScore = PlayerPrefs.GetInt("HighScore_0", 0);
        highScoreText.text = $"ç≈çÇãLò^ÅF{topScore} ñ‚";
    }
}
