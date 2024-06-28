using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name); // This will print the name of the collided object

        if (collision.gameObject.tag == "Stamp")
        {
            Destroy(gameObject); // Destroy the bullet
            Debug.Log("Bullet collided with stamp");
        }
    }

}

