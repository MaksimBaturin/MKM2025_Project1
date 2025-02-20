using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MoonTerrainGenerator : MonoBehaviour
{
    public int width = 100;
    public int height = 20; 
    public float scale = 20f; 
    public float offsetX = 100f; 
    public float offsetY = 100f; 
    public int seed = 0; 

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        InitializeOffsets();
        GenerateLandscape();
        UpdateMesh();
        AddCollider();
    }

    void InitializeOffsets()
    {
        
        System.Random rand = new System.Random(seed);
        offsetX = rand.Next(0, 10000);
        offsetY = rand.Next(0, 10000);
    }

    void GenerateLandscape()
    {
        vertices = new Vector3[width * 2];
        for (int x = 0; x < width; x++)
        {
            float sampleX = (float)x / width * scale + offsetX;
            float sampleY = offsetY;
            int tileHeight = Mathf.RoundToInt(Mathf.PerlinNoise(sampleX, sampleY) * height);

            vertices[x] = new Vector3(x, tileHeight, 0);
            vertices[x + width] = new Vector3(x, 0, 0);
        }


        triangles = new int[(width - 1) * 6];
        for (int x = 0; x < width - 1; x++)
        {
            triangles[x * 6] = x;
            triangles[x * 6 + 1] = x + width;
            triangles[x * 6 + 2] = x + 1;

            triangles[x * 6 + 3] = x + 1;
            triangles[x * 6 + 4] = x + width;
            triangles[x * 6 + 5] = x + width + 1;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); 
    }

    void AddCollider()
    {
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();

        Vector2[] colliderPoints = new Vector2[width * 2];
        for (int x = 0; x < width; x++)
        {
            colliderPoints[x] = new Vector2(vertices[x].x, vertices[x].y);

            colliderPoints[width * 2 - 1 - x] = new Vector2(vertices[x + width].x, vertices[x + width].y);
        }


        polygonCollider.SetPath(0, colliderPoints);
    }
}