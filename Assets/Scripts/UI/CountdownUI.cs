using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    public IEnumerator PlayCountdown()
    {
        countdownText.text = "Ready";
        yield return new WaitForSeconds(1f);

        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);

        countdownText.text = "";
    }
}
