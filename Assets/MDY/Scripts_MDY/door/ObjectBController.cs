using System.Collections;
using UnityEngine;

public class ObjectBFader : MonoBehaviour
{
    public float fadeDuration = 2.0f;
    private bool isFading = false;
    private bool isVisible = true;
    private Renderer rend;
    private Material material;
    private Collider objectCollider;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        material = rend.material;
        objectCollider = GetComponent<Collider>();
    }

    public void StartFadingOut()
    {
        if (!isFading && isVisible) 
        {
            StartCoroutine(FadeOut());
        }
    }

    public void StartFadingIn()
    {
        if (!isFading && !isVisible)
        {
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeOut()
    {
        isFading = true;
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            float slideAmount = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            material.SetFloat("_SlideAmount", slideAmount);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        material.SetFloat("_SlideAmount", 1);
        objectCollider.enabled = false; // Disable the collider when fully faded out
        isFading = false;
        isVisible = false;
    }

    private IEnumerator FadeIn()
    {
        isFading = true;
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            float slideAmount = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            material.SetFloat("_SlideAmount", slideAmount);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        material.SetFloat("_SlideAmount", 0);
        objectCollider.enabled = true; // Enable the collider when fully faded in
        isFading = false;
        isVisible = true;
    }
}