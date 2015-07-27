using UnityEngine;
using System.Collections;
using Leap;

public class Instantiation : MonoBehaviour {
	//public Rigidbody projectile;
	public GameObject projectile;
	public int limiter=5;
	Controller controller;
	//Rigidbody clone;

	void Start(){
		controller = new Controller();
		/*controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
		controller.Config.SetFloat("Gesture.Swipe.MinLength", 20.0f);
		controller.Config.SetFloat("Gesture.Swipe.MinVelocity", 50f);
		controller.Config.Save();*/
	}

	void Update() {
		//Leap Motion code
		Frame frame = controller.Frame(); // controller is a Controller object
		HandList hands = frame.Hands;
		Hand rhand = frame.Hands.Rightmost;
		Hand lhand = frame.Hands.Leftmost;

		//Normal code
		print ("start");
		if (rhand.IsValid && lhand.IsValid) {
			if (rhand.IsRight) {
				print ("Right Hand");
				isRH (rhand);
			} else if (lhand.IsLeft) {
				print ("LEft Hand");
				isLH (lhand);
			} else{
				print ("MOTHER OF GOD HOW MANY HANDS DO YOU HAVE?");
			}
		} else {
			print("leap says fu");
		}

	}

	void isRH(Hand rhand){
		
		//Instantiate (projectile, position, transform.rotation);
		//CloneCode
		GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");
		if (clones.Length < limiter) {
			print ("RHClone");
			//Vector3 position = new Vector3 (rhand.PalmNormal.x, rhand.PalmNormal.y, rhand.PalmNormal.z);
			for (int i=0; i<=clones.Length;i++){
				Vector3 position = new Vector3 (rhand.PalmPosition.x/1000, rhand.PalmPosition.y/1000, rhand.PalmPosition.z/1000); //PalmNormal or PalmPosition
				//Vector3 position = new Vector3 (rhand.PalmPosition.z/1000, 0, rhand.PalmPosition.x/1000); //PalmNormal or PalmPosition
				//Vector3 position = new Vector3 (-rhand.PalmPosition.z/1000, 0, 00); //PalmNormal or PalmPosition
				//Vector3 position = new Vector3 (-rhand.PalmPosition.z/1000, 0, 00); //PalmNormal or PalmPosition
				position= transform.TransformPoint(position);
				Debug.Log ("Defpos"+rhand.PalmPosition);
				Debug.Log ("PalmPos"+position);
				if(i != 0){
					print("i!=0");
					if ( 
					     (clones[i].transform.position.x  <= clones[i-1].transform.position.x *1.5f) ||
						 (clones[i].transform.position.y  <= clones[i-1].transform.position.y *1.5f) ||
						 (clones[i].transform.position.z  <= clones[i-1].transform.position.z *1.5f)
					 	){
						print ("Shouldn't work");
					}
				} else{
					print("Instant");
					Instantiate (projectile, position, transform.rotation);
				}	
			}
			print ("done");
		}else{
			print ("fail"+GameObject.FindGameObjectsWithTag("Clone").Length);
			for(int i=0; i<limiter;i++){
				Destroy(GameObject.FindGameObjectWithTag("Clone"));
			}
		}
	}


	void isLH(Hand lhand){

	}
}