using System.Collections;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [Header("Backgrounds")]
    [SerializeField] GameObject lightbgParent;
    [SerializeField] GameObject shadowbgParent;
    [SerializeField] SpriteRenderer[] lightLayers;
    [SerializeField] SpriteRenderer[] shadowLayers;

    [Header("Transition Settings")]
    [SerializeField] private float transitionDuration = 1f;
    private bool isTransitioning = false;

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


    private void Update()
    {


    }

    public void SwitchfromShadowtoLight()
    {
        if(!isTransitioning) 
            StartCoroutine(SwitchBackground(false));
    }

    public void StartSwitch()
    {
        if(!isTransitioning)
            StartCoroutine(SwitchBackground(true));
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

        isTransitioning = false;
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
}
