using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class timerScript : MonoBehaviour {

	/*
	 * timerScript manages the GUI time element
	 * that displays how much time has elapsed within 
	 * the animation of the simulation
	 */

	public GameObject simTime;
	public Text lblText;

	int hours;
	int mins = 0;
	float elapsed;

	// Use this for initialization
	void Start () {

		//assign the gui object
		lblText = gameObject.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {

		//update the elapsed time
		elapsed = simTime.GetComponent<simTime> ().elapsedTime;
		hours = Mathf.FloorToInt(elapsed / 60);

		//format the hours and minutes nicely
		if (hours == 0) {
			mins = Mathf.FloorToInt (elapsed);
		} 
		else {
			mins = Mathf.FloorToInt (elapsed - (hours * 60));
		}

		//update the label
		lblText.text = "Hours : Minutes -> " + hours + ":" + mins;
	}
}
