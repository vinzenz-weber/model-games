using UnityEngine;

public class ObjectForceUp : MonoBehaviour
{
    public float forceUpAmount = 500f; // The amount of upward force to apply

    void Update()
    {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has the tag "bullet"
                if (hit.collider.CompareTag("bullet"))
                {
                    Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddForce(Vector3.up * forceUpAmount);
                    }
                }
            }
        
    }
}
