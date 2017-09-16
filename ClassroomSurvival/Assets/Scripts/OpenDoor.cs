using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

	//private Rigidbody rb;

	private bool isOpen;

	void Start(){
		//rb = GetComponent<Rigidbody> ();
		isOpen = false;
	}

	void Update() {
		if (Input.GetButton ("Fire1")) {
			//transform.position = new Vector3(-3.886f, 0.739f, 5.445f);
			if (!isOpen) {
				transform.RotateAround (transform.position + new Vector3 (-0.1f, 0.0f, 0.0f), transform.up, -90f);
				isOpen = true;
			} else {
				transform.RotateAround (transform.position + new Vector3 (-0.1f, 0.0f, 0.0f), transform.up, 90f);
				isOpen = false;
			}
		}
	}
}
