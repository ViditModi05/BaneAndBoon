using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour
{
    [Header("Refs")]
    private Collider2D platformCollider;
    private SpriteRenderer platformRenderer;
    [Header("Settings")]
    [SerializeField] private float breakDelay = 2f; 
    [SerializeField] private float shakeDuration = 1f; 
    [SerializeField] private float shakeIntensity = 0.1f;
    [SerializeField] private float respawnTime = 1f;

    private bool isBreaking = false;
    private Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = transform.position;
        platformCollider = GetComponent<Collider2D>();
        platformRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isBreaking)
        {
            isBreaking = true;
            StartCoroutine(ShakePlatform());
        }
    }

    private IEnumerator ShakePlatform()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float xOffset = Random.Range(-shakeIntensity, shakeIntensity);
            float yOffset = Random.Range(-shakeIntensity, shakeIntensity);
            transform.position = originalPosition + new Vector3(xOffset, yOffset, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        platformCollider.enabled = false; 
        platformRenderer.enabled = false; 
        yield return new WaitForSeconds(respawnTime); 
        RespawnPlatform();
    }

    private void RespawnPlatform()
    {
        transform.position = originalPosition;
        platformCollider.enabled = true; // Enable collision
        platformRenderer.enabled = true; // Show platform
        isBreaking = false;
    }
}
