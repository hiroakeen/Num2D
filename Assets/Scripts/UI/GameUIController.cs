using UnityEngine;
using TMPro;

/// <summary>
/// UIの表示管理（ターゲット数と現在の合計）
/// </summary>
public class GameUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private TextMeshProUGUI currentSumText;

    public void UpdateTarget(int target)
    {
        targetText.text = $"{target}";
    }

    public void UpdateCurrentSum(int sum)
    {
        currentSumText.text = $"いま: {sum}";
    }

    public void ClearCurrentSum()
    {
        currentSumText.text = "";
    }
}
