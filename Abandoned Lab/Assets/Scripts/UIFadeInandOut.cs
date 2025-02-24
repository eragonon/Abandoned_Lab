using UnityEngine;
using UnityEngine.UI;  // For UI elements
using System.Collections;

public class UIFadeInOut : MonoBehaviour
{
    public GameObject uiElement;  // The UI element that will fade in and out
    private CanvasGroup canvasGroup;  // To control the fading effect

    private void Start()
    {
        // Get the CanvasGroup component from the UI element
        canvasGroup = uiElement.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found on UI element. Please add a CanvasGroup component.");
            return;
        }

        // Start the fade-in and out process
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // Initially set the alpha to 0 (fully transparent)
        canvasGroup.alpha = 0f;

        // Fade in the UI element
        float fadeInDuration = 1f;  // Duration of the fade-in effect
        float startAlpha = canvasGroup.alpha;
        float targetAlpha = 1f;
        float timeElapsed = 0f;

        // Fade loop for fade-in
        while (timeElapsed < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeInDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure it is fully faded in
        canvasGroup.alpha = targetAlpha;

        // Wait for 3 seconds before starting fade-out
        yield return new WaitForSeconds(3f);

        // Start fading out the UI element
        float fadeOutDuration = 1f;  // Duration of the fade-out effect
        startAlpha = canvasGroup.alpha;
        targetAlpha = 0f;
        timeElapsed = 0f;

        // Fade loop for fade-out
        while (timeElapsed < fadeOutDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeOutDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure it is fully faded out
        canvasGroup.alpha = targetAlpha;

        // Optionally, disable the UI element after it fades out
        uiElement.SetActive(false);
    }
}
