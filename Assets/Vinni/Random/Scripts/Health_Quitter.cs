using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health_Quitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "wall")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the scene
        }

        if (collision.gameObject.tag == "goal")
        {
            Debug.Log("You win!");
        }
    }
}
