using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialTextFader : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private TextMeshProUGUI tutorialText;

    [Header("Settings")]
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float displayDuration = 2f;
    private Color normalWorldColor = Color.black;
    private Color shadowWorldColor = Color.white;

    private void Start()
    {
        StartCoroutine(ShowTutorial());
    }

    private void Update()
    {
        tutorialText.color = Player_Manager.instance.player.inShadowState ? shadowWorldColor : normalWorldColor;

    }

    IEnumerator ShowTutorial()
    {
        string[] instructions =
        {
            "Press A and D to Move",
            "Press Space to Jump",
            "Press Space Twice to Double Jump",
            "Hold Shift to Dash",
            "Press Tab to Switch Worlds",
            "You Can Only Stay in the Shadow World for 5 seconds",
            "You cannot Double Jump and Dash in Shadow World"
        };

        foreach (string instruction in instructions)
        {
            yield return StartCoroutine(FadeText(instruction, true));
            yield return new WaitForSeconds(displayDuration);
            yield return StartCoroutine(FadeText(instruction, false));
        }
        tutorialText.gameObject.SetActive(false); // Hide text after tutorial
    }

    IEnumerator FadeText(string message, bool fadeIn)
    {
        tutorialText.text = message;
        tutorialText.gameObject.SetActive(true);

        float elapsedTime = 0f;
        Color textColor = tutorialText.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = fadeIn ? Mathf.Lerp(0, 1, elapsedTime / fadeDuration) : Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            tutorialText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            yield return null;
        }

        if (!fadeIn)
        {
            tutorialText.gameObject.SetActive(false);
        }
    }
}
