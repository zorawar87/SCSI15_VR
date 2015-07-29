using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshGenerator : MonoBehaviour {

	MeshFilter mFilter;
	LayerMask planeMask;

	// Use this for initialization
	void Start () {
		mFilter = GetComponent<MeshFilter>();
		planeMask = LayerMask.GetMask("GroundPlane");

		Vector3 center = new Vector3(0, 1, 0);
		Vector3 normal = new Vector3(0, 0, -1);
		float radius = 0.5f;
		CreateMesh(center, normal, radius);
	}
	
	// Update is called once per frame
	void Update () {
		DynamicMeshGenerator();
		Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(r, out hit, 10.0f, planeMask)){
			Vector3 center = hit.point;
			Vector3 normal = new Vector3(0,0,-1);
			float radius = 0.5f;
			CreateMesh(center, normal, radius);
		}
	
	}

	void CreateMesh(Vector3 center, Vector3 normal, float radius){
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Vector2> uvCoords = new List<Vector2>();
		uvCoords.Add(new Vector2(0f, 0f));

		Vector3 right = Vector3.Cross(Vector3.up, normal);
		Vector3 up = Vector3.Cross(normal, right);

		right = right.normalized*radius;
		up = up.normalized*radius;

		vertices.Add(center);

		int numSections = 20;
		for(int slice = 0; slice < numSections; slice++){
			float theta = (float)slice/(float)numSections*2.0f*Mathf.PI;

			Vector3 pt = center + right*Mathf.Cos(theta) + up*Mathf.Sin(theta);
			vertices.Add(pt);
			uvCoords.Add(new Vector2(Mathf.Cos(theta)/2.0f+0.5f, Mathf.Sin(theta)/2.0f+0.5f));

			if (slice > 0){
				//add the triangle
				triangles.Add(0);
				triangles.Add(slice);
				triangles.Add(slice + 1);
			}
		}
		//add last triangle
		triangles.Add(0);
		triangles.Add(numSections-1);
		triangles.Add(1);

		Mesh circularPlane = new Mesh();
		circularPlane.vertices = vertices.ToArray();
		circularPlane.triangles = triangles.ToArray();
		circularPlane.uv = uvCoords.ToArray();

		circularPlane.RecalculateBounds();
		circularPlane.RecalculateNormals();

		mFilter.mesh = circularPlane;
	}

	void DynamicMeshGenerator(Vector3 thumbPos, Vector3 palmPos, Vector3 indexPos ){
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Vector2> uvCoords = new List<Vector2>();
		uvCoords.Add(new Vector2(0f, 0f));
		
		Vector3 right = Vector3.Cross(Vector3.up, normal);
		Vector3 up = Vector	3.Cross(normal, right);
		
		right = right.normalized*radius;
		up = up.normalized*radius;
		
		vertices.Add(center);
		
		int numSections = 20;
		for(int slice = 0; slice < numSections; slice++){
			float theta = (float)slice/(float)numSections*2.0f*Mathf.PI;
			
			Vector3 pt = center + right*Mathf.Cos(theta) + up*Mathf.Sin(theta);
			vertices.Add(pt);
			uvCoords.Add(new Vector2(Mathf.Cos(theta)/2.0f+0.5f, Mathf.Sin(theta)/2.0f+0.5f));
			
			if (slice > 0){
				//add the triangle
				triangles.Add(0);
				triangles.Add(slice);
				triangles.Add(slice + 1);
			}
		}
		//add last triangle
		triangles.Add(0);
		triangles.Add(numSections-1);
		triangles.Add(1);
		
		Mesh circularPlane = new Mesh();
		circularPlane.vertices = vertices.ToArray();
		circularPlane.triangles = triangles.ToArray();
		circularPlane.uv = uvCoords.ToArray();
		
		circularPlane.RecalculateBounds();
		circularPlane.RecalculateNormals();
		
		mFilter.mesh = circularPlane;
	}
}















