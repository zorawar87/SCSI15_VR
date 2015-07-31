﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class Instantiation : MonoBehaviour
{
	public GameObject cube;
	public int limiter = 5;
	public float boxSeparation = 4f;

	HandController leap;
	HandModel[] hands;
	Controller controller;
	bool isCloneGenerationActive;
	Frame frame;

	MeshFilter mFilter;
	List<Vector3> vertices;
	List<int> triangles;
	List<Vector2> uvCoords;
	Material meshMat;

	//Rigidbody clone;
	
	void Start () {
		isCloneGenerationActive = false;
		leap = GetComponent<HandController> ();
		controller = leap.GetLeapController ();

		controller.EnableGesture (Gesture.GestureType.TYPE_SCREEN_TAP);
		controller.Config.SetFloat ("Gesture.ScreenTap.MinForwardVelocity", 0.5f);
		controller.Config.SetFloat ("Gesture.ScreenTap.HistorySeconds", 0.5f);
		controller.Config.SetFloat ("Gesture.ScreenTap.MinDistance", 1.0f);
		controller.Config.Save ();

		mFilter = GameObject.FindGameObjectWithTag("MeshHere").GetComponent<MeshFilter>();
		//mFilter = GameObject.FindGameObjectWithTag("MeshHere").GetComponent<MeshFilter>();
		vertices = new List<Vector3>();
		triangles = new List<int>();
		uvCoords = new List<Vector2>();
	}
	
	void Update () {
		frame = controller.Frame ();
		hands = leap.GetAllGraphicsHands ();
		if(hands.Length>0){
			DynamicMeshGenerator(hands);
		}
	}

	void DynamicMeshGenerator(HandModel[] hands){

		Vector3 indexFPos = hands[0].fingers[(int)Finger.FingerType.TYPE_INDEX].GetTipPosition();
		Vector3 thumbFPos = hands[0].fingers[(int)Finger.FingerType.TYPE_THUMB].GetTipPosition();
		//Finger indexF = frame.Hands [0].Fingers [(int)Finger.FingerType.TYPE_INDEX];
		//Finger thumbF = frame.Hands [0].Fingers [(int)Finger.FingerType.TYPE_THUMB];
		//Vector3 indexFPos = Camera.main.transform.TransformPoint (leap.transform.TransformPoint(indexF.TipPosition.ToUnityScaled ()));
		//Vector3 thumbFPos = Camera.main.transform.TransformPoint (leap.transform.TransformPoint(thumbF.TipPosition.ToUnityScaled ()));
		print ("IP"+indexFPos);
		print ("TP"+thumbFPos);
		print("gho"+hands[0].GetHandOffset());
		vertices.Add(indexFPos);
		uvCoords.Add(new Vector2(0f, 0.0f));
		vertices.Add(thumbFPos);
		uvCoords.Add(new Vector2(0f, 1f));

		
		if(vertices.Count>3){
			int a = vertices.Count-4;
			int b = vertices.Count-3;
			int c = vertices.Count-2;
			int d = vertices.Count-1;
			print ("vertice count "+vertices.Count);

			triangles.Add (a);
			triangles.Add (d);
			triangles.Add (c);
			triangles.Add (c);
			triangles.Add (d);
			triangles.Add (a);
			print ("triangle at ("+a+", "+d+", "+c+")");
			
			triangles.Add (a);
			triangles.Add (b);
			triangles.Add (d);
			triangles.Add (d);
			triangles.Add (b);
			triangles.Add (a);
			print ("triangle at "+a+b+d);
		} else {
			print ("condition fails");
		}


		Mesh dynaMesh = new Mesh();
		dynaMesh.vertices = vertices.ToArray();
		dynaMesh.triangles = triangles.ToArray();
		dynaMesh.uv = uvCoords.ToArray();
		mFilter.mesh = dynaMesh;

	}

	void onBothHandsDetected(HandModel[] hands){
		if(hands.Length==2){
			CreateClones(hands);
			print (hands.Length+"hands detected");
		} else if(hands.Length==1){
			print (hands.Length +"hands detected");
		} else{
			print ("ERROR");
			print (hands.Length +" hands detected");
		}
	}

	void CreateClones (HandModel[] hands){
		
		GameObject[] clones = GameObject.FindGameObjectsWithTag ("Clone");
		if (clones.Length < limiter) {
			for (int i=0; i<=clones.Length; i++) {
				//!!!!GETS POSITION OF FIRST HAND!!!!!
				Vector3 position = hands [0].GetPalmPosition ();
				
				if (i != 0) {
					Debug.Log ("i!=0");
					if (
						(clones [i].transform.position.x <= clones [i - 1].transform.position.x * boxSeparation) &&
						(clones [i].transform.position.y <= clones [i - 1].transform.position.y * boxSeparation) &&
						(clones [i].transform.position.z <= clones [i - 1].transform.position.z * boxSeparation)
						) {
						Debug.Log ("Shouldn't work");
					}
				} else {
					print ("FromInstant");
					Instantiate (cube, position, transform.rotation);
				}	
			}
			Debug.Log ("done");
		} else {
			Debug.Log ("fail" + GameObject.FindGameObjectsWithTag ("Clone").Length);
			for (int i=0; i<limiter; i++) {
				Destroy (GameObject.FindGameObjectWithTag ("Clone"));
			}
		}
	}

	//----------------------------------------------------------------------

	//purge/fix the following

	void GestureRecognition (HandModel[] hands) {
		GestureList gesturesInFrame = frame.Gestures ();
		
		do{
			print ("loopStart");
			print("gesturesInFrame.Count "+gesturesInFrame.Count);
			for (int i = 0; i < gesturesInFrame.Count%2; i++) {	
				Gesture gesture = gesturesInFrame [i];
				if (gesture.Type == Gesture.GestureType.TYPE_SCREEN_TAP) {
					//ScreenTapGesture screentapGesture = new ScreenTapGesture (gesture);
					Debug.Log ("scrtap");
					if (isCloneGenerationActive) {
						print ("screentapDetected"+isCloneGenerationActive); 
						isCloneGenerationActive = false;
					} else if (isCloneGenerationActive == false) {
						isCloneGenerationActive = true;
						print ("screentapDetected"+isCloneGenerationActive); 
					} else {
						Debug.Log ("what condition is this?");
					}
				} else if (gesture.Type == Gesture.GestureType.TYPE_INVALID) {
					print ("invalid");
				}
				print (i+" of "+gesturesInFrame.Count);
			}
			print ("loopover");
			
			if (isCloneGenerationActive) {
				print ("CreateClonesCalled");
				CreateClones (hands);
				print ("willGenerateClones");
			} else {
				print ("CreateClonesNOTCalled");
			}
			print ("finalloopend"+isCloneGenerationActive);
		} while(isCloneGenerationActive);
		print ("wloopover"+isCloneGenerationActive);
	}




}