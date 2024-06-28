using UnityEngine;
using System.Collections.Generic;

public class MeshSlicer : MonoBehaviour
{
    public int numberOfSlices = 5;
    public Material sliceMaterial;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    SliceMesh();
                }
            }
        }
    }

    void SliceMesh()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        for (int i = 0; i < numberOfSlices; i++)
        {
            Vector3 sliceNormal = Random.onUnitSphere;
            Vector3 slicePoint = RandomPointInMesh(mesh);

            List<Vector3> leftVertices, rightVertices;
            List<int> leftTriangles, rightTriangles;

            SliceMesh(vertices, triangles, slicePoint, sliceNormal, out leftVertices, out rightVertices, out leftTriangles, out rightTriangles);

            CreatePiece(leftVertices, leftTriangles);
            CreatePiece(rightVertices, rightTriangles);
        }

        Destroy(gameObject);
    }

    void SliceMesh(Vector3[] vertices, int[] triangles, Vector3 point, Vector3 normal,
        out List<Vector3> leftVertices, out List<Vector3> rightVertices,
        out List<int> leftTriangles, out List<int> rightTriangles)
    {
        leftVertices = new List<Vector3>();
        rightVertices = new List<Vector3>();
        leftTriangles = new List<int>();
        rightTriangles = new List<int>();

        // Implementation of mesh slicing logic goes here
        // You would need to iterate over the triangles, check which side of the plane they lie on, and split triangles if necessary
        // This part can be quite complex and is beyond a simple example

        // Here, you would add your vertices and triangles to the appropriate lists (left or right)
    }

    Vector3 RandomPointInMesh(Mesh mesh)
    {
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        int randomTriangleIndex = Random.Range(0, triangles.Length / 3) * 3;
        Vector3 pointA = vertices[triangles[randomTriangleIndex]];
        Vector3 pointB = vertices[triangles[randomTriangleIndex + 1]];
        Vector3 pointC = vertices[triangles[randomTriangleIndex + 2]];

        float r1 = Random.value;
        float r2 = Random.value;

        return (1 - Mathf.Sqrt(r1)) * pointA + (Mathf.Sqrt(r1) * (1 - r2)) * pointB + (Mathf.Sqrt(r1) * r2) * pointC;
    }

    void CreatePiece(List<Vector3> vertices, List<int> triangles)
    {
        GameObject piece = new GameObject("Piece", typeof(MeshFilter), typeof(MeshRenderer), typeof(Rigidbody), typeof(MeshCollider));

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        piece.GetComponent<MeshFilter>().mesh = mesh;
        piece.GetComponent<MeshRenderer>().material = sliceMaterial;
        piece.GetComponent<MeshCollider>().sharedMesh = mesh;
        piece.GetComponent<MeshCollider>().convex = true;
    }
}
