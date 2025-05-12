using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// �I�𒆂̃s�[�X����łȂ��N���X�iLineRenderer����j
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;

        // �}�e���A����F�������Őݒ�\�i�f�t�H���g�����ƍ��j
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
    }

    /// <summary>
    /// �����X�V�i�I�𒆃s�[�X�̈ʒu�����ɐڑ��j
    /// </summary>
    public void UpdateLine(List<Piece> selectedPieces)
    {
        if (selectedPieces == null || selectedPieces.Count == 0)
        {
            ClearLine();
            return;
        }

        lineRenderer.positionCount = selectedPieces.Count;
        for (int i = 0; i < selectedPieces.Count; i++)
        {
            lineRenderer.SetPosition(i, selectedPieces[i].transform.position);
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void ClearLine()
    {
        lineRenderer.positionCount = 0;
    }
}
