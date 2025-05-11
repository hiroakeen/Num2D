using UnityEngine;

public class AutoScaler : MonoBehaviour
{
    public Vector2 referenceResolution = new Vector2(540, 960); // �z��𑜓x

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
            // ��ʂ����� �� �����ɍ��킹�ďk�ڂ��v�Z
            scaleFactor = (float)Screen.height / referenceResolution.y;
        }
        else
        {
            // ��ʂ��c�� �� ���ɍ��킹�ďk�ڂ��v�Z
            scaleFactor = (float)Screen.width / referenceResolution.x;
        }

        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
    }
}
