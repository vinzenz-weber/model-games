using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Collision_logic : MonoBehaviour
{

public float lastPos;
public int floorHit = 0;

public int endScore = 3;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "floor") {
            Destroy(this.gameObject);
            floorHit++;
        
        
            if (floorHit == endScore)
            {
                // Restart the level
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            }
            

        }
        if (collision.gameObject.tag == "Player")
        {
            this.transform.parent = collision.transform;
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }
            //Destroy(GetComponent<BoxCollider>());
            this.gameObject.tag = "Player";
            lastPos = this.gameObject.transform.position.y;
            Debug.Log(lastPos);
        }
    }
}
