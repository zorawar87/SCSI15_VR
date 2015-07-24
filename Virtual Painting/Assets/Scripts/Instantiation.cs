using UnityEngine;
using System.Collections;
using Leap;

public class Instantiation : MonoBehaviour {
	//public Rigidbody projectile;
	public GameObject projectile;
	public int limiter=5;
	//Rigidbody clone;



	void Update() {
		//Leap Motion code
		Controller controller = new Controller();
		Frame frame = controller.Frame(); // controller is a Controller object
		HandList hands = frame.Hands;
		Hand rhand = frame.Hands.Rightmost;
		Hand lhand = frame.Hands.Leftmost;

		//Normal code
		print ("start");
		if (rhand.IsValid && lhand.IsValid) {
			if (rhand.IsRight) {
				print ("Right Hand");
				print ("rh normal " + rhand.PalmNormal);

				//CloneCode
				if (GameObject.FindGameObjectsWithTag ("Clone").Length < limiter) {
					print ("RHClone");
					print (GameObject.FindGameObjectsWithTag ("Clone").Length);
					Vector3 position = new Vector3 (rhand.PalmNormal.x, rhand.PalmNormal.y, rhand.PalmNormal.z);
					print ("x" + rhand.PalmNormal.x);
					print ("y" + rhand.PalmNormal.y);
					print ("z" + rhand.PalmNormal.z);
					Instantiate (projectile, position, transform.rotation);
					print ("done");
				}else{
					print ("fail"+GameObject.FindGameObjectsWithTag("Clone").Length);
					for(int i=0; i<limiter;i++){
						Destroy(GameObject.FindGameObjectWithTag("Clone"));
					}
				}
			} else if (lhand.IsLeft) {
				print ("LEft Hand");
				print ("lh normal " + lhand.PalmNormal);
			}
		} else {
			print("leap says fu");
		}

		}
}