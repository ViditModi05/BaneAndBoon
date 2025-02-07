using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchManager : MonoBehaviour
{
    [Header("Backgrounds")]
    [SerializeField] GameObject lightbgParent;
    [SerializeField] GameObject shadowbgParent;
    [SerializeField] SpriteRenderer[] lightLayers;
    [SerializeField] SpriteRenderer[] shadowLayers;

    [Header("Transition Settings")]
    [SerializeField] private float transitionDuration = 1f;
    [SerializeField] private float warningDuration = 5f;
    [SerializeField] private float warningStartDelay = 3f;
    [SerializeField] private Image warningOverlay;
    private bool isTransitioning = false;
    private bool isShadowMode = false;
    private Coroutine warningCoroutine = null;
    public System.Action onSwitch;

    [Header("Pause")]
    [SerializeField] private GameObject pausePanel;
    private bool isPaused = false;


    public static SwitchManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }


    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }


    public void SwitchfromShadowtoLight()
    {
        if (!isTransitioning)
        {
            isShadowMode = false;
            FadeOutWarning();
            StartCoroutine(SwitchBackground(false));
        }

        if (onSwitch != null)
        {
            onSwitch();
        }
    }

    public void StartSwitch()
    {
        if (!isTransitioning)
        {
            isShadowMode = true;
            StartCoroutine(SwitchBackground(true));
        }

        if (onSwitch != null)
        {
            onSwitch();
        }
    }

    private IEnumerator SwitchBackground(bool toShadow)
    {
        isTransitioning = true;

        SpriteRenderer[] fadeOutLayers = toShadow ? lightLayers : shadowLayers;
        SpriteRenderer[] fadeInLayers = toShadow ? shadowLayers : lightLayers;

        lightbgParent.SetActive(true);
        shadowbgParent.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            float alpha = elapsedTime / transitionDuration;

            SetLayerAlpha(fadeOutLayers, 1 - alpha);
            SetLayerAlpha(fadeInLayers, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetLayerAlpha(fadeOutLayers, 0f);
        SetLayerAlpha(fadeInLayers, 1f);

        lightbgParent.SetActive(!toShadow);
        shadowbgParent.SetActive(toShadow);

        if (toShadow)
        {
            if (warningCoroutine != null)
                StopCoroutine(warningCoroutine);
            warningCoroutine = StartCoroutine(ShowWarningEffect());

        }
        else
        {
            StartCoroutine(FadeOutWarning());
        }

        isTransitioning = false;
    }

    private IEnumerator ShowWarningEffect()
    {
        yield return new WaitForSeconds(warningStartDelay);
        if (!isShadowMode)
            yield break;
        float elapsedTime = 0f;
        while (elapsedTime < warningDuration)
        {
            if (!isShadowMode)
            {
                FadeOutWarning();
                yield break;
            }
            float alpha = elapsedTime / warningDuration;
            warningOverlay.color = new Color(1, 0, 0, alpha * 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }
    private IEnumerator FadeOutWarning()
    {
        float elapsedTime = 0f;
        float startAlpha = warningOverlay.color.a;

        while (elapsedTime < 1f) // Fade out quickly
        {
            float alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / 1f);
            warningOverlay.color = new Color(1, 0, 0, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        warningOverlay.color = new Color(1, 0, 0, 0);
    }

    private void SetLayerAlpha(SpriteRenderer[] layers, float alpha)
    {
        foreach (SpriteRenderer sr in layers)
        {
            Color color = sr.color;
            color.a = alpha;
            sr.color = color;
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
