using UnityEngine;
using System.Collections;
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
	}
	
	void Update ()
	{
		HandModel[] hands = leap.GetAllGraphicsHands ();
		//GestureRecognition (hands);
		ifBothHandsDetected(hands);
	}

	void CreateClones (HandModel[] hands)
	{

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

	void GestureRecognition (HandModel[] hands) {
		Frame frame = controller.Frame ();
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

	void ifBothHandsDetected(HandModel[] hands){
		if(hands.Length==2){
			CreateClones(hands);
			print (hands.Length+"hands detected");
		} else if(hands.Length==1){
			print (hands.Length +"hands detected");
		} else{
			print ("ERROR");
			print (hands.Length +"hands detected");
		}
	}
}