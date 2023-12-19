using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PlaneGenerator : MonoBehaviour
{
    // Variables 
    private Vector2Int m_size;
    private Vector3 m_positionOffset;
    private MeshFilter m_meshFilter;
    private Vector3[] m_vertices;
    private Color[] m_colors;

    // Methods
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        m_meshFilter = GetComponent<MeshFilter>();
    }

    /// <summary>
    /// Initialize the plane using a specific size
    /// </summary>
    public void Create(int sizeX, int sizeZ)
    {
        sizeX -= 1;
        sizeZ -= 1;

        m_size = new Vector2Int(sizeX, sizeZ);

        Mesh m_mesh = new Mesh();

        Vector3[] vertices = new Vector3[(sizeX * sizeZ * 6)];
        int[] triangles = new int[vertices.Length];
        Vector3[] normals = new Vector3[vertices.Length];

        for (int x = 0, i = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                // First Triangle
                vertices[i] = new Vector3(x, 0, z) + m_positionOffset;
                normals[i] = Vector3.up;
                triangles[i] = i;
                i++;

                vertices[i] = new Vector3(x, 0, z + 1) + m_positionOffset;
                normals[i] = Vector3.up;
                triangles[i] = i;
                i++;

                vertices[i] = new Vector3(x + 1, 0, z) + m_positionOffset;
                normals[i] = Vector3.up;
                triangles[i] = i;
                i++;

                // Second Triangle
                vertices[i] = new Vector3(x, 0, z + 1) + m_positionOffset;
                normals[i] = Vector3.up;
                triangles[i] = i;
                i++;

                vertices[i] = new Vector3(x + 1, 0, z + 1) + m_positionOffset;
                normals[i] = Vector3.up;
                triangles[i] = i;
                i++;

                vertices[i] = new Vector3(x + 1, 0, z) + m_positionOffset;
                normals[i] = Vector3.up;
                triangles[i] = i;
                i++;
            }
        }

        m_mesh.vertices = vertices;
        m_mesh.triangles = triangles;
        m_mesh.normals = normals;
        m_mesh.Optimize();

        Color[] colors = new Color[m_mesh.vertices.Length];

        for (int i = 0; i < m_mesh.vertices.Length; i++)
        {
            colors[i] = Color.white;
        }

        m_mesh.colors = colors;
        m_meshFilter.mesh = m_mesh;

        m_vertices = m_meshFilter.mesh.vertices;
        m_colors = m_meshFilter.mesh.colors;
    }

    /// <summary>
    /// Set the height of a specific vertex using the X and Z position.
    /// </summary>
    public void SetHeight(int x, int z, float height)
    {
        for (int i = 0; i < m_vertices.Length; i++)
        {
            Vector3 vertex = m_vertices[i];
            if (vertex.x == x + m_positionOffset.x && vertex.z == z + m_positionOffset.z)
            {
                vertex.y = height;
                m_vertices[i] = vertex;
            }
        }
    }

    /// <summary>
    /// Set the color of a specific vertex using the x and the z position.
    /// </summary>
    public void SetColor(int x, int z, Color color)
    {
        Vector3[] vertices = m_meshFilter.mesh.vertices;

        for (int i = 0; i < m_colors.Length; i++)
        {
            Vector3 vertex = vertices[i];
            if (vertex.x == x + m_positionOffset.x && vertex.z == z + m_positionOffset.z)
            {
                m_colors[i] = color;
            }
        }
    }

    /// <summary>
    /// Refresh the mesh to assign the position and colors.
    /// </summary>
    public void RefreshMesh()
    {
        m_meshFilter.mesh.vertices = m_vertices;
        m_meshFilter.mesh.colors = m_colors;
    }
}
