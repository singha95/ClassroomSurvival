using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed; 


	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		float moveHorizontal = Mathf.Abs(Input.GetAxis ("Horizontal")) > 0.5 ? Input.GetAxis ("Horizontal") : 0;
		float moveVertical = Mathf.Abs(Input.GetAxis ("Vertical")) > 0.5 ? Input.GetAxis ("Vertical") : 0;
		Vector3 movement = new Vector3 (moveVertical * speed, 0.0f, moveHorizontal * speed);

		//float step = speed * Time.deltaTime;
		//transform.position = new Vector3(transform.position.x + moveHorizontal * speed, 0.0f, transform.position.z + moveVertical * speed);
		transform.Translate(movement);

		//movement = new Vector3 (
		//rb.AddForce(movement * speed);
	}
}
