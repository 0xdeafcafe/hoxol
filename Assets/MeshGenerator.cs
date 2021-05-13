using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
	private Mesh mesh;
	private Vector3[] vertices;
	private int[] triangles;

	public int RingWorldDiameter = 1080;
	public int RingWorldCrossSectionWidth = 500;
	public int RingWorldTempX = 25;

	// Start is called before the first frame update
	void Start()
	{
		mesh = new Mesh();

		GetComponent<MeshFilter>().mesh = mesh;

		GenerateMesh();
		UpdateMesh();
	}

	void GenerateMesh()
	{
        vertices = new Vector3[(RingWorldCrossSectionWidth + 1) * (RingWorldTempX + 1)];
        triangles = new int[RingWorldCrossSectionWidth * RingWorldTempX * 6];

		var i = 0;

		for (var z = 0; z <= RingWorldCrossSectionWidth; z++)
        {
			for (var x = 0; x <= RingWorldTempX; x++)
			{
				// vertices[i] = new Vector3(x, Mathf.PerlinNoise(x * 0.2f, z * 0.2f) * 2f, z);
				vertices[i] = new Vector3(x, 0, z);

				i++;
			}
		}

		var vert = 0;
		var tris = 0;

		for (var z = 0; z < RingWorldCrossSectionWidth; z++)
        {
			for (var x = 0; x < RingWorldTempX; x++)
            {
				triangles[tris + 0] = vert;
				triangles[tris + 1] = vert + RingWorldTempX + 1;
				triangles[tris + 2] = vert + 1;
				triangles[tris + 3] = vert + 1;
				triangles[tris + 4] = vert + RingWorldTempX + 1;
				triangles[tris + 5] = vert + RingWorldTempX + 2;

				vert += 1;
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
}
