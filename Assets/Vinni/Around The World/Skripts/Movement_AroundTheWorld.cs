using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vinni {

public class Movement_AroundTheWorld : MonoBehaviour
{
    public Camera mainCamera; // Die Kamera, die auf die Weltkugel schaut
    public Rigidbody playerRigidbody; // Das Rigidbody der Spielerkugel
    public Transform worldSphere; // Die große Weltkugel
    public float maxForce = 10f; // Maximale Bewegungskraft
    public float minDistance = 1f; // Minimale Distanz, ab der die Kraft angewendet wird
    public float maxSpeed = 5f; // Maximale Geschwindigkeit des Spielers
    public LayerMask layerMask; // LayerMask für das Raycast

    void FixedUpdate()
    {
        // Raycast von der Kamera zur Mausposition
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Wenn der Ray die Weltkugel trifft
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && hit.collider.transform == worldSphere)
        {
            // Berechne die Richtung vom Spieler zur Treffpunkt
            Vector3 direction = (hit.point - playerRigidbody.position).normalized;

            // Berechne die Distanz vom Spieler zur Treffpunkt
            float distance = Vector3.Distance(hit.point, playerRigidbody.position);

            // Wende die Kraft proportional zur Distanz an, bis zu einer maximalen Kraft
            if (distance > minDistance)
            {
                float forceMagnitude = Mathf.Clamp(distance, 0, maxForce);
                playerRigidbody.AddForce(direction * forceMagnitude, ForceMode.Impulse); // Verwende ForceMode.Impulse für ein snappier Gefühl
            }

            // Begrenze die Geschwindigkeit des Spielers
            LimitPlayerSpeed();
        }
    }

    void LimitPlayerSpeed()
    {
        // Begrenze die Geschwindigkeit des Rigidbodys
        if (playerRigidbody.velocity.magnitude > maxSpeed)
        {
            playerRigidbody.velocity = playerRigidbody.velocity.normalized * maxSpeed;
        }
    }
}

}
