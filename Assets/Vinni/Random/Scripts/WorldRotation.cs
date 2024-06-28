using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vinni {

public class WorldRotation : MonoBehaviour
{
    public GameObject worldSphere; // Die Kugel, die sich drehen soll
    public float rotationSpeed = 10f; // Grundgeschwindigkeit der Rotation
    public float changeDirectionInterval = 10; // Intervall in Sekunden, in dem die Drehrichtung geändert wird

    public float multiplier = 1.1f;

    private Vector3 rotationAxis; // Die aktuelle Rotationsachse
    //private float currentSpeed; // Die aktuelle Rotationsgeschwindigkeit

    void Start()
    {
        if (worldSphere == null)
        {
            Debug.LogError("WorldSphere is not assigned.");
            return;
        }

        // Initialisiere die erste Rotationsachse und Geschwindigkeit
        ChangeRotationDirection();
        // Starte den wiederholenden Wechsel der Drehrichtung
        StartCoroutine(ChangeDirectionRoutine());
    }

    void Update()
    {
        if (worldSphere != null)
        {
            // Drehe die Kugel um die aktuelle Achse mit der aktuellen Geschwindigkeit
            worldSphere.transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDirectionInterval);
            ChangeRotationDirection();
        }
    }

    void ChangeRotationDirection()
    {
        // Wähle eine zufällige Rotationsachse
        rotationAxis = Random.onUnitSphere;
        // Wähle eine zufällige Geschwindigkeit zwischen -rotationSpeed und rotationSpeed
        rotationSpeed *= multiplier;
        Debug.Log(rotationSpeed);
    }
}

}
