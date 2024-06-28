using UnityEngine;
using System.Collections.Generic;

public class Magneton_Shooter : MonoBehaviour
{
    public ObjectPooler pooler;
    public float shootForce = 500f;
    public float destroyDistance = 50f;
    private List<GameObject> activeSpheres = new List<GameObject>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootSphere();
        }

        RemoveFarSpheres();
    }

    void ShootSphere()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            int randomIndex = Random.Range(0, pooler.prefabs.Length);

            GameObject sphere = pooler.GetObject(randomIndex, transform.position, Quaternion.identity);

            // Set random rotation
            sphere.transform.rotation = Random.rotation;

            // Set random scale
            float randomScale = Random.Range(0.5f, 1.5f);
            sphere.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            // Adjust mass based on scale
            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            //rb.mass = Mathf.Pow(randomScale, 3); // Mass scales with volume

            Vector3 forceDirection = (hit.point - transform.position).normalized;
            rb.AddForce(forceDirection * shootForce);
            activeSpheres.Add(sphere);
        }
    }

    void RemoveFarSpheres()
    {
        for (int i = activeSpheres.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(activeSpheres[i].transform.position, transform.position) > destroyDistance)
            {
                int prefabIndex = System.Array.IndexOf(pooler.prefabs, activeSpheres[i]);
                pooler.ReturnObject(activeSpheres[i], prefabIndex);
                activeSpheres.RemoveAt(i);
            }
        }
    }
}
