using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class ChickenController : MonoBehaviour {
	Rigidbody rb;
	float multiplier=1;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (VRDevice.isPresent){
			//VRCode

		}else{
			//Fire1
			if(Input.GetButtonDown("Fire1")){
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Vector3 mult = 2000*ray.direction;
				Debug.Log(mult);
				rb.AddForce(mult);
			}
		}
	}

}
