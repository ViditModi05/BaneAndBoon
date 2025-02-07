using System.Collections;
using TMPro;
using UnityEngine;

public class GameEndManager : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public GameObject creditsPanel;
    public float fadeDuration = 1f;
    public float displayDuration = 2f;

    private void Start()
    {
        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);
        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        text1.gameObject.SetActive(true);
        yield return StartCoroutine(FadeText(text1, true));
        yield return new WaitForSeconds(displayDuration);
        yield return StartCoroutine(FadeText(text1, false));
        text1.gameObject.SetActive(false);

        text2.gameObject.SetActive(true);
        yield return StartCoroutine(FadeText(text2, true));
        yield return new WaitForSeconds(displayDuration);
        yield return StartCoroutine(FadeText(text2, false));
        text2.gameObject.SetActive(false);

        creditsPanel.SetActive(true);
    }

    IEnumerator FadeText(TextMeshProUGUI text, bool fadeIn)
    {
        float startAlpha = fadeIn ? 0f : 1f;
        float endAlpha = fadeIn ? 1f : 0f;
        float elapsedTime = 0f;

        Color color = text.color;
        color.a = startAlpha;
        text.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            text.color = color;
            yield return null;
        }

        color.a = endAlpha;
        text.color = color;
    }
}
