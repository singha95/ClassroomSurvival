using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

	private bool isOpen;
	private Vector3 position;

	void Start(){
		isOpen = false;
		position = transform.position + new Vector3 (-0.4f, 0.0f, 0.0f);
	}

	void OnTriggerEnter() {
		//controller?
		if (Input.GetKeyDown (KeyCode.R)) {
			if (!isOpen) {
				transform.RotateAround (position, transform.up, -90f);
				isOpen = true;
			} else {
				transform.RotateAround (position, transform.up, 90f);
				isOpen = false;
			}
		}
	}
}
