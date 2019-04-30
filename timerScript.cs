using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class timerScript : MonoBehaviour {

	public GameObject simTime;
	public Text lblText;

	int hours;
	int mins = 0;
	float elapsed;

	// Use this for initialization
	void Start () {

		lblText = gameObject.GetComponent<Text> ();

	}

	// Update is called once per frame
	void Update () {

		elapsed = simTime.GetComponent<simTime> ().elapsedTime;
		hours = Mathf.FloorToInt(elapsed / 60);

		if (hours == 0) {
			mins = Mathf.FloorToInt (elapsed);
		} 
		else {
			mins = Mathf.FloorToInt (elapsed - (hours * 60));
		}

		//print (elapsed);
		//print ("h:" + hours + " m:" + mins);

		lblText.text = "Hours : Minutes -> " + hours + ":" + mins;
	}
}
