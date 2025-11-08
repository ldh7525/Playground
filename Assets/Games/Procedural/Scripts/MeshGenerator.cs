using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    MeshFilter meshFilter;
    Vector3[] vertices;
    int[] triangles;

    public int xSize;
    public int zSize;

    [SerializeField] private float _noiseScale = 0.3f;
    [SerializeField] private float _heightMultiplier = 2f;

    void Start()
    {
        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                var perlin= Mathf.PerlinNoise(x * _noiseScale, z * _noiseScale) * _heightMultiplier;
                vertices[z * (xSize + 1) + x] = new Vector3(x, perlin, z);
            }
        }

        triangles = new int[xSize * zSize * 6];

        int tris = 0;
        int vert = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;

                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }


    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void OnValidate()
    {
        CreateShape();
        UpdateMesh();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
