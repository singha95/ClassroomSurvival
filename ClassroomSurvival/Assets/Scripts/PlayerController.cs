using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int MAXSTRESS = 100;
	public double stress;


	// Use this for initialization
	void Start () {
		stress = 0;
	}

	// Update is called once per frame
	void Update (){

		if (stress < MAXSTRESS)
			addStress(0.05);

		else if (stress > MAXSTRESS)
			stress = MAXSTRESS;

		if (Input.GetButton("Interact")) {
			Debug.Log ("j key is held down");
			talkToStudents ();

		}
	}

	void talkToStudents() {
		GameObject[] students = GameObject.FindGameObjectsWithTag("Student");

		Debug.Log("num of students is " + students.Length);
		Vector3 position = transform.position;


		foreach (GameObject go in students)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.magnitude;
			Debug.Log ("distance is " + curDistance);
			if (curDistance < 0.5)
			{
				Debug.Log ("reducing stress");
				reduceStress (0.2);
				break;
			}
		}
	}

	void addStress(double amount) {
		stress += amount;
	}

	void reduceStress(double amount) {
		stress -= amount;
		if (stress < 0)
			stress = 0;
	}
}
