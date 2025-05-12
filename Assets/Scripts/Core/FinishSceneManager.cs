using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class FinishSceneManager : MonoBehaviour
{
    [Header("UI�v�f")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private UIFader fader;

    [Header("�{�^��")]
    [SerializeField] private Button retryButton;
    [SerializeField] private Button titleButton;

    [Header("�J�E���g�A�b�v�ݒ�")]
    [SerializeField] private float countDelay = 0.05f; // �����̑����Ԋu

    private int finalScore;

    void Start()
    {
        // �����ݒ�F�{�^����\���A�X�R�A��\���A�t�F�[�h�J�n
        retryButton.gameObject.SetActive(false);
        titleButton.gameObject.SetActive(false);
        scoreText.text = "";
        fader.FadeIn(1f);

        // �X�R�A�ǂݍ���
        finalScore = PlayerPrefs.GetInt("LastScore", 0);

        // �J�E���g�A�b�v���o�J�n
        StartCoroutine(CountUpScore());
    }

    private IEnumerator CountUpScore()
    {
        int current = 0;

        yield return new WaitForSeconds(1f); // ���o�O�̊�

        while (current <= finalScore)
        {
            scoreText.text = $"���𐔁F{current} ��";
            current++;
            yield return new WaitForSeconds(countDelay);
        }

        // �����Ԃ������ă{�^���\��
        yield return new WaitForSeconds(0.5f);

        retryButton.gameObject.SetActive(true);
        titleButton.gameObject.SetActive(true);

        // �{�^�����t�F�[�h�\��
        retryButton.transform.DOScale(1f, 0.4f).From(0f).SetEase(Ease.OutBack);
        titleButton.transform.DOScale(1f, 0.4f).From(0f).SetEase(Ease.OutBack);
    }

    public void OnRetry()
    {
        fader.FadeOutAndLoadScene("MainScene", 1f);
    }

    public void OnTitle()
    {
        fader.FadeOutAndLoadScene("TitleScene", 1f);
    }
}
