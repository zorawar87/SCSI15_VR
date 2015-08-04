using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class Instantiation : MonoBehaviour{
	HandController leap;
	HandModel[] hands;
	Controller controller;
	Frame frame;

	Mesh dynaMesh;
	MeshFilter mFilter;
	MeshRenderer meshRenderer;
	List<Vector3> vertices;
	List<int> triangles;
	List<Vector2> uvCoords;
	int whereInTheMesh;
	bool handsInLastFrame;

	public Material mat;
	
	void Start () {
		leap = GetComponent<HandController> ();
		controller = leap.GetLeapController ();

		dynaMesh = new Mesh();
		dynaMesh.subMeshCount = 1;
		mFilter = GameObject.FindGameObjectWithTag("MeshHere").GetComponent<MeshFilter>();
		meshRenderer = GameObject.FindGameObjectWithTag("MeshHere").GetComponent<MeshRenderer>();
		vertices = new List<Vector3>();
		triangles = new List<int>();
		uvCoords = new List<Vector2>();
		whereInTheMesh = 0;
		handsInLastFrame = false;

		//functionality unavailable
		controller.EnableGesture (Gesture.GestureType.TYPE_SCREEN_TAP);
		controller.Config.SetFloat ("Gesture.ScreenTap.MinForwardVelocity", 0.5f);
		controller.Config.SetFloat ("Gesture.ScreenTap.HistorySeconds", 0.5f);
		controller.Config.SetFloat ("Gesture.ScreenTap.MinDistance", 1.0f);
		controller.Config.Save ();
	}
	
	void Update () {
		frame = controller.Frame ();
		hands = leap.GetAllGraphicsHands ();
		if(hands.Length>0){
			handsInLastFrame = true;
			onBothHandsDetected(hands);
			//DynamicMeshGenerator(hands);
		} else{
			noHandsWhatToDo();
		}
	}

	void onBothHandsDetected(HandModel[] hands){
		if(hands.Length==2){
			DynamicMeshGenerator(hands);
		} else if(hands.Length==1){
			print (hands.Length +"hand(s) detected");
		} else{
			print ("ERROR");
			print (hands.Length +" hand(s) detected");
		}
	}

	void noHandsWhatToDo(){
		if (handsInLastFrame == true) {
			triangles.Clear();
			whereInTheMesh=0;
			dynaMesh.subMeshCount++;
			handsInLastFrame =false;
			Material[] mats = new Material[dynaMesh.subMeshCount];
			for(int i=0; i < mats.Length; i++){
				mats[i] = mat;
			}
			meshRenderer.materials = mats;
		}
	}



	void DynamicMeshGenerator(HandModel[] hands){

		Vector3 indexFPos = hands[0].fingers[(int)Finger.FingerType.TYPE_INDEX].GetTipPosition();
		Vector3 thumbFPos = hands[0].fingers[(int)Finger.FingerType.TYPE_THUMB].GetTipPosition();

		vertices.Add(indexFPos);
		whereInTheMesh++;
		uvCoords.Add(new Vector2(0f, 0f));

		vertices.Add(thumbFPos);
		whereInTheMesh++;
		uvCoords.Add(new Vector2(1f, 1f));
		
		if(vertices.Count>3 && whereInTheMesh >3){
			int a = vertices.Count-4;
			int b = vertices.Count-3;
			int c = vertices.Count-2;
			int d = vertices.Count-1;

			triangles.Add (a);
			triangles.Add (d);
			triangles.Add (c);
			triangles.Add (c);
			triangles.Add (d);
			triangles.Add (a);
			
			triangles.Add (a);
			triangles.Add (b);
			triangles.Add (d);
			triangles.Add (d);
			triangles.Add (b);
			triangles.Add (a);
		} else {
			print ("no triangles made");
		}

		dynaMesh.vertices = vertices.ToArray();
		dynaMesh.SetTriangles(triangles.ToArray(), dynaMesh.subMeshCount-1);
		dynaMesh.uv = uvCoords.ToArray();
		mFilter.mesh = dynaMesh;
	}

	//----Unused---
	bool isCloneGenerationActive;

	void GestureRecognition (HandModel[] hands) {
		isCloneGenerationActive=false;
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
				print ("DynamicMeshGeneratorCalled");
				DynamicMeshGenerator (hands);
				print ("willGenerateClones");
			} else {
				print ("DynamicMeshGeneratorNOTCalled");
			}
			print ("finalloopend"+isCloneGenerationActive);
		} while(isCloneGenerationActive);
		print ("wloopover"+isCloneGenerationActive);
	}
}