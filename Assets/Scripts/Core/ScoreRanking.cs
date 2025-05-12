using System.Collections.Generic;
using UnityEngine;

public static class ScoreRanking
{
    private const int MaxRank = 5;

    /// <summary>
    /// ハイスコアを登録し、ランキングを更新する
    /// </summary>
    public static void RegisterScore(int newScore)
    {
        List<int> scores = LoadScores();
        scores.Add(newScore);
        scores = scores.OrderByDescending(s => s).Take(MaxRank).ToList();

        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetInt($"HighScore_{i}", scores[i]);
        }

        PlayerPrefs.Save();
    }

    /// <summary>
    /// ランキングリスト（最大5件）を返す
    /// </summary>
    public static List<int> LoadScores()
    {
        List<int> scores = new List<int>();
        for (int i = 0; i < MaxRank; i++)
        {
            if (PlayerPrefs.HasKey($"HighScore_{i}"))
            {
                scores.Add(PlayerPrefs.GetInt($"HighScore_{i}"));
            }
        }
        return scores;
    }
}
