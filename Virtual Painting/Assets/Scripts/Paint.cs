using UnityEngine;
using System.Collections;

public class Paint : MonoBehaviour {
	GameObject whatever;

	// Use this for initialization
	void Start () {
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.position = new Vector3(0, 0.5F, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
			Debug.Log("Pressed left click.");
		if(Input.GetMouseButtonDown(1))
			Debug.Log("Pressed right click.");
		if(Input.GetMouseButtonDown(2))
			Debug.Log("Pressed middle click.");
	}
	void  FixedUpdate(){
		//0 is for when the left button is clicked, 1 is for the right
		//if(Input.GetMouseButtonDown(0))
			//instantiate(whatever,transform.position,Quaternion.identity);
	}
}
