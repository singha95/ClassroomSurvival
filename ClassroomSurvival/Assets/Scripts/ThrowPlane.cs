using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPlane : MonoBehaviour {
	bool isThrowPlane = false;
	public Transform playerCam;
	public float throwForce = 10;
	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		if (isThrowPlane) {
			rb.isKinematic = false;
			transform.parent = null;
			rb.AddForce (playerCam.forward * throwForce);
		}
	}
}
