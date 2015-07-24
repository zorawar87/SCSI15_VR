using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	private float lookAngle;
	private float rotationGain = 4.0f;

	// Use this method for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position + offset;

		float mouseXPosition = Input.GetAxis ("Mouse X");
		lookAngle += mouseXPosition * rotationGain;
		Quaternion rotation = Quaternion.AngleAxis (lookAngle, new Vector3 (0.0f, 1.0f, 0.0f));
		transform.rotation = rotation;
	}
}
