using UnityEngine;
using System.Collections.Generic;

public class Magneton_SpringConnector : MonoBehaviour
{
    public float connectionDistance = 1f;
    public float springForce = 50f;
    public float springDamper = 1f;
    public float checkInterval = 0.5f;

    private Rigidbody rb;
    private float nextCheckTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nextCheckTime = Time.time + Random.Range(0f, checkInterval); // stagger initial checks
    }

    void Update()
    {
        if (Time.time >= nextCheckTime)
        {
            ConnectNearbySpheres();
            nextCheckTime = Time.time + checkInterval;
        }
    }

    void ConnectNearbySpheres()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, connectionDistance);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != gameObject && hitCollider.CompareTag("Sphere"))
            {
                SpringJoint[] existingJoints = gameObject.GetComponents<SpringJoint>();
                bool alreadyConnected = false;
                foreach (var joint in existingJoints)
                {
                    if (joint.connectedBody == hitCollider.GetComponent<Rigidbody>())
                    {
                        alreadyConnected = true;
                        break;
                    }
                }
                
                if (!alreadyConnected)
                {
                    SpringJoint springJoint = gameObject.AddComponent<SpringJoint>();
                    springJoint.connectedBody = hitCollider.GetComponent<Rigidbody>();
                    springJoint.spring = springForce;
                    springJoint.damper = springDamper;
                }
            }
        }
    }
}
