using UnityEngine;

public class TargetNumberProvider : MonoBehaviour
{
    public int TargetNumber { get; private set; }

    [SerializeField] private GameUIController uiController; // ← 追加

    void Start()
    {
        GenerateNewTarget();
    }

    public void GenerateNewTarget()
    {
        TargetNumber = Random.Range(10, 21);
        Debug.Log($"🎯 新しいターゲット: {TargetNumber}");

        if (uiController != null)
        {
            uiController.UpdateTarget(TargetNumber);
        }
    }
}
