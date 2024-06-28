using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Vinni {
    public class PlayerHealth : MonoBehaviour
{
    public Material material1;
    public Material material2;
    public float fadeSpeed = 0.1f; // Speed at which the material fades out
    public float alphaIncreaseAmount = 10f / 255f; // Amount to increase alpha by when collecting a collectible

    public GameObject gameOverUI;
    public GameObject gameUI;
    //public GameObject MainMenuUI;

    public Score score;

    public Slider slider;

    [SerializeField] private AnimationCurve healthToOpacityCurve; // Curve to control opacity based on health

    private float health = 1;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(health);
        health=1;
        gameOverUI.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1; // Set time scale to 1
        UpdateMaterialOpacity();
        StartCoroutine(FadeOutMaterials());
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health;

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameOver();
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectible")
        {
            Debug.Log("Collected");
            IncreaseHealth(alphaIncreaseAmount);
            StartCoroutine(DisableCollectibleTemporarily(other.gameObject));
        }
    }

    IEnumerator FadeOutMaterials()
    {
        while (true)
        {
            DecreaseHealth(fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    void IncreaseHealth(float amount)
    {
        health = Mathf.Clamp(health + amount, 0, 1); // Increase health without exceeding 1
        UpdateMaterialOpacity();
    }

    void DecreaseHealth(float amount)
    {
        health = Mathf.Clamp(health - amount, 0, 1); // Decrease health without going below 0
        UpdateMaterialOpacity();
    }

    void UpdateMaterialOpacity()
    {
        float opacity = healthToOpacityCurve.Evaluate(health);
        Color color1 = material1.color;
        color1.a = opacity;
        material1.color = color1;

        Color color2 = material2.color;
        color2.a = opacity;
        material2.color = color2;

        if (opacity <= 0)
        {
            GameOver();
        }
    }

    IEnumerator DisableCollectibleTemporarily(GameObject collectible)
    {
        collectible.SetActive(false); // Disable the collectible
        yield return new WaitForSeconds(3); // Wait for 3 seconds
        collectible.SetActive(true); // Enable the collectible again
    }



    public void GameOver()
    {
        Time.timeScale = 0; // Stop time

        gameUI.SetActive(false);

        gameOverUI.SetActive(true); // Show the game over UI
        score.OnGameOver(); // Call the OnGameOver function in the Score script
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the scene
    }
}

}

