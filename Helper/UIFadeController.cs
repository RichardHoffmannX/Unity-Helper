using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIFadeController : MonoBehaviour
{
    public float delay = 3f;
    public float duration = 1f;

    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;

    void Awake()
    {
        // Automatically get the CanvasGroup component on this object
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
  
    }

    void OnEnable()
    {
        canvasGroup.alpha = 1f;
        StartFadeOut();
    }

    void OnDisable()
    {
        canvasGroup.alpha = 0f;
        if(fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = null;
    }

    [Button]
    public void StartFadeOut()
    {
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(canvasGroup.alpha, 0f));
    }

    // Call this method to fade back in if needed
    public void StartFadeIn(float duration)
    {
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(canvasGroup.alpha, 1f));
    }

    private IEnumerator FadeCanvasGroup(float startAlpha, float targetAlpha)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;

        // Keep looping until the duration is reached
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Smoothly calculate the current alpha based on elapsed time
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);

            yield return null; // Wait until the next frame
        }

        // Ensure the final alpha hits the exact target value
        canvasGroup.alpha = targetAlpha;

        // Optional: Disable interaction once completely faded out
        if (targetAlpha == 0f)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}
