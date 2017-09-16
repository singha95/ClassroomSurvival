using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public int MAXSTRESS = 100;
	public int MAXTASKCOMPLETE = 100;
	public float stress;
	public float taskComplete;
	public Slider stressSlider;
	public Slider taskSlider;
	public Text gameOver;
	public Text timer; 

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update (){

		if (stress < MAXSTRESS)
			addStress (0.05f);
		else if (stress >= MAXSTRESS)
			EndGame ();
		

		if (Input.GetButton("Interact")) {
			
			talkToStudents ();
			doTask ();
		}

		stressSlider.value = stress;
		taskSlider.value = taskComplete;
	}
	void EndGame(){
		gameOver.text = "Too Stressed Out! You Lose.";
		timer.text = "0"; 
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
				GameObject desk = GameObject.FindGameObjectWithTag ("PlayerDesk");
				Vector3 studentDistance = go.transform.position - desk.transform.position;
				float deskDistance = studentDistance.magnitude;
				reduceStress (0.05f * deskDistance);
				break;
			}
		}
	}

	void doTask() {
		GameObject desk = GameObject.FindGameObjectWithTag("PlayerDesk");

		Vector3 position = transform.position;

		Vector3 diff = desk.transform.position - position;
		float curDistance = diff.magnitude;
		Debug.Log ("distance is " + curDistance);
		if (curDistance < 0.5)
		{
			Debug.Log ("doing work");
			increaseTaskComplete (0.05f);
		}
	}

	void addStress(float amount) {
		stress += amount;
	}

	void reduceStress(float amount) {
		stress -= amount;
		if (stress < 0)
			stress = 0;
	}

	void increaseTaskComplete(float amount) {
		taskComplete += amount;
	}
}
