using UnityEngine;
using TMPro;

public class FinishSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
        scoreText.text = $"³‰ğ”F{lastScore} –â";
    }
}
