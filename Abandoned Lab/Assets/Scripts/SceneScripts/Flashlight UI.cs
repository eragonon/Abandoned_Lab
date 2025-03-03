using UnityEngine;
using TMPro; // Make sure to include the TextMeshPro namespace
using System.Collections;

public class UIPopAndFadeTMP : MonoBehaviour
{
    public TMP_Text uiText; // Reference to the TextMeshPro UI element (drag and drop in the Inspector)
    public float delayBeforeAppear = 3f; // Time before the UI element starts to fade in
    public float fadeInDuration = 1f;  // Time it takes to fade in
    public float displayDuration = 3f;  // Time the UI element stays fully visible
    public float fadeOutDuration = 1f; // Time it takes to fade out

    private Color originalColor; // Store the original color of the text

    void Start()
    {
        if (uiText != null)
        {
            originalColor = uiText.color; // Save the original color
            uiText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); // Set alpha to 0 (fully transparent)
            StartCoroutine(FadeInAndOut()); // Start the fade sequence
        }
        else
        {
            Debug.LogError("TextMeshPro UI element is not assigned in the Inspector!");
        }
    }

    private IEnumerator FadeInAndOut()
    {
        // Wait for the initial delay
        yield return new WaitForSeconds(delayBeforeAppear);

        // Fade in the UI element
        float timeElapsed = 0f;
        while (timeElapsed < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeInDuration);
            uiText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure it is fully faded in
        uiText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        // Wait for the display duration
        yield return new WaitForSeconds(displayDuration);

        // Fade out the UI element
        timeElapsed = 0f;
        while (timeElapsed < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timeElapsed / fadeOutDuration);
            uiText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure it is fully faded out
        uiText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // Optionally, disable the UI element after it fades out
        uiText.gameObject.SetActive(false);
    }
}