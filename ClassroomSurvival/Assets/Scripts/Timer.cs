using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float targetTime = 60.0f;

	public Text TimerText;

	public Text GameEndText;

	void Update(){
		if (targetTime > 0.0f) {
				
			targetTime -= Time.deltaTime;
			TimerText.text = ((int)targetTime).ToString ();
		}
		else {
			timerEnded();
		}

	}


	// Use this for initialization
	void timerEnded() {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		PlayerController playerController = player.GetComponent<PlayerController> ();
		if (playerController.taskComplete >= 100) {
			GameEndText.text = "Finally! It's time to go home.";
		} else
			GameEndText.text = "You didn't get your work done!";

	}

}
