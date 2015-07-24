using UnityEngine;
using System.Collections;

public class MeshGeneration : MonoBehaviour
{
	private float mWidth = 1.0f;
	private float mLength = 1.0f;

	private GameObject gObject = null;
	private MeshFilter mFilter = null;
	private MeshRenderer mRenderer = null;

	private Vector3[] vertices = null;
	private Vector3[] normals = null;
	private Vector2[] UV;

	private int[]triangles = null;

	private Mesh mesh = null;

	private Material material = null;

	// Use this for initialization
	void Start ()
	{
		Mesh();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	private void Mesh() //creating the mesh
	{
		this.vertices = new Vector3[4];
		this.normals = new Vector3[4];
		this.UV = new Vector2[4];

		this.triangles = new int[6];

		this.triangles = new int[6];

		this.triangles = new int[6];

		this.vertices[0] = new Vector3(0.0f,0.0f,0.0f);
		this.UV[0] = new Vector2(0.0f,0.0f);
		this.normals[0] = Vector3.up;

		this.vertices[1] = new Vector3(0.0f,0.0f,this.mLength);
		this.UV[1] = new Vector2(0.0f,1.0f);
		this.normals[1] = Vector3.up;

		this.vertices[2] = new Vector3(this.mWidth,0.0f,this.mLength);
		this.UV[2] = new Vector2(1.0f,1.0f);
		this.normals[2] = Vector3.up;

		this.vertices[3] = new Vector3(this.mWidth,0.0f,0.0f);
		this.UV[3] = new Vector2(1.0f,0.0f);
		this.normals[3] = Vector3.up;

		this.triangles[0] = 0;
		this.triangles[1] = 1;
		this.triangles[2] = 2;

		this.triangles[3] = 0;
		this.triangles[4] = 2;
		this.triangles[5] = 3;

		this.gObject = new GameObject("Mesh");
		this.gObject.transform.position = new Vector3(0, 0, 0);

		this.gObject.AddComponent(typeof(MeshFilter));
		this.gObject.AddComponent(typeof(MeshRenderer));

		this.mFilter = this.gObject.GetComponent<MeshFilter>();
		this.mRenderer = this.gObject.GetComponent<MeshRenderer>();

		this.mesh = new Mesh();
		this.mesh.vertices = this.vertices;
		this.mesh.triangles = this.triangles;
		this.mFilter.mesh = this.mesh;

		this.mesh.uv = this.UV;
		this.mesh.normals = this.normals;

		this.material = Resources.Load("Default-Material", typeof(Material)) as Material;
		this.mRenderer.material = this.material;

		this.gObject.AddComponent<MeshCollider>();

	}
}

