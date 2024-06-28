using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGameLogic : MonoBehaviour
{

    public GameObject Bullet;
    public GameObject Stamp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletHitDetection();
    }

    public void BulletHitDetection()
    {
        // Check for collision between two game objects
        if (Bullet != null && Stamp != null)
        {
            if (Bullet.GetComponent<Collider>().bounds.Intersects(Stamp.GetComponent<Collider>().bounds))
            {
                // Collision detected
                Debug.Log("Collision detected between " + Bullet.name + " and " + Stamp.name);
            }
        }
    }


}
