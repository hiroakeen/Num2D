using UnityEngine;

public class AutoScaler : MonoBehaviour
{
    public Vector2 referenceResolution = new Vector2(540, 960); // ‘z’è‰ğ‘œ“x

    void Start()
    {
        ScaleToScreen();
    }

    void ScaleToScreen()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = referenceResolution.x / referenceResolution.y;

        float scaleFactor = 1f;

        if (screenRatio >= targetRatio)
        {
            // ‰æ–Ê‚ª‰¡’· ¨ ‚‚³‚É‡‚í‚¹‚ÄkÚ‚ğŒvZ
            scaleFactor = (float)Screen.height / referenceResolution.y;
        }
        else
        {
            // ‰æ–Ê‚ªc’· ¨ •‚É‡‚í‚¹‚ÄkÚ‚ğŒvZ
            scaleFactor = (float)Screen.width / referenceResolution.x;
        }

        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
    }
}
