using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorChange : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color startColor = Color.green;
    private Color endColor = Color.red;
    private float duration;

    public GameObject playerPrefab; // Reference to the player prefab
    public float movementThreshold = 0.1f; // Acceptable small movement

    private bool isRed = false;

    private Rigidbody rb;

    void Start()
    {
        rb = playerPrefab.GetComponent<Rigidbody>();

        objectRenderer = GetComponent<Renderer>();
        StartColorChangeRoutine();
    }

    void Update()
    {
        if (isRed && playerPrefab != null)
        {
            if (rb.velocity.magnitude > movementThreshold)
            {
                RestartLevel();
            }
        }
    }

    private void StartColorChangeRoutine()
    {
        duration = Random.Range(3f, 7f); // Random duration between 3 and 7 seconds
        StartCoroutine(ChangeColorOverTime());
    }

    IEnumerator ChangeColorOverTime()
    {
        float elapsedTime = 0f;

        // Turn from green to red
        while (elapsedTime < duration)
        {
            objectRenderer.material.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectRenderer.material.color = endColor; // Ensure the color is set to endColor at the end
        isRed = true; // Set flag indicating the GameObject is now red

        yield return new WaitForSeconds(2f); // Wait for 2 seconds while the cube is red

        // Turn back to green
        objectRenderer.material.color = startColor;
        isRed = false;

        StartColorChangeRoutine(); // Restart the color change process
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
