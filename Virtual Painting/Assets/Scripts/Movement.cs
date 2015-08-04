using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	//MouseControl
	private float lookAngle;
	float rotationGain;

	void Start () {
		rotationGain = 4.0f;
	}
	
	// Update is called once per frame
	void Update () {

		//position
		if(Input.GetKey("w")){
			transform.Translate(Vector3.forward * Time.deltaTime);
		} else if (Input.GetKey("a")){
			transform.Translate(Vector3.left * Time.deltaTime);
		} else if (Input.GetKey("s")){
			transform.Translate(Vector3.back * Time.deltaTime);
		} else if (Input.GetKey("d")){
			transform.Translate(Vector3.right * Time.deltaTime);
		}

		//where you lookin' at?
		float mouseXPosition = Input.GetAxis ("Mouse X");
		lookAngle += mouseXPosition * rotationGain;
		Quaternion rotation = Quaternion.AngleAxis (lookAngle, new Vector3 (0.0f, 1.0f, 0.0f));
		transform.rotation = rotation;
	}
}
