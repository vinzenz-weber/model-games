using UnityEngine;

public class ScatterCubes : MonoBehaviour
{
    public GameObject worldSphere; // Das Weltobjekt (die große Kugel)
    public GameObject cubePrefab; // Das Prefab des Würfels, das verteilt werden soll
    public int cubeAmount = 10; // Anzahl der zu verteilenden Würfel
    public Vector3 minCubeSize = new Vector3(0.5f, 0.5f, 0.5f); // Minimale Größe der Würfel
    public Vector3 maxCubeSize = new Vector3(2f, 2f, 2f); // Maximale Größe der Würfel
    public float penetrationDepth = 0.1f; // Wie tief die Würfel in die Oberfläche eindringen

    void Start()
    {
        ScatterCubesOnSurface();
    }

    void ScatterCubesOnSurface()
    {
        for (int i = 0; i < cubeAmount; i++)
        {
            // Erzeuge einen zufälligen Punkt auf der Oberfläche der Kugel
            Vector3 randomPoint = Random.onUnitSphere * (worldSphere.transform.localScale.x / 2f);
            Vector3 worldPosition = worldSphere.transform.position + randomPoint;

            // Erzeuge einen Raycast, um die genaue Position und Normale auf der Oberfläche zu finden
            RaycastHit hit;
            if (Physics.Raycast(worldPosition, -randomPoint.normalized, out hit))
            {
                // Erzeuge den Würfel und setze seine Position und Rotation
                GameObject cube = Instantiate(cubePrefab, hit.point - hit.normal * penetrationDepth, Quaternion.identity, worldSphere.transform);
                cube.transform.up = hit.normal;

                // Setze die zufällige Größe des Würfels
                Vector3 randomSize = new Vector3(
                    Random.Range(minCubeSize.x, maxCubeSize.x),
                    Random.Range(minCubeSize.y, maxCubeSize.y),
                    Random.Range(minCubeSize.z, maxCubeSize.z)
                );
                cube.transform.localScale = randomSize;
            }
        }
    }
}
