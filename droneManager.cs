using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneManager : MonoBehaviour {

	public int numOfDrone;
	public GameObject landscape;
	public Zephyr dronePrefab;

	public List<Fire> fireArray = new List<Fire>();
	public Zephyr[] droneArray;

	int fireCount = 0;

	// Use this for initialization
	void Start () {

		Vector3[] startPoss = new Vector3[2];

		if (numOfDrone == 1) {
			startPoss [0] = new Vector3 (0, 21, 0);
			startPoss [1] = new Vector3 (0, 21, 0);
		}
		if (numOfDrone == 2) {
			startPoss [0] = new Vector3 (75, 21, 75);
			startPoss [1] = new Vector3 (-75, 21, -75);
		}


		droneArray = new Zephyr[numOfDrone];

		for (int x = 0; x < numOfDrone; x++) {

			Zephyr newDrone = Instantiate (dronePrefab, startPoss[x], new Quaternion (0,0,0,0));
			droneArray [x] = newDrone;
		
		}
	}
	
	// Update is called once per frame
	void Update () {

		float sDistance = float.MaxValue;
		Zephyr closestDrone = null;
		Fire closestFire = null;

		for (int j = 0; j < fireArray.Count; j++) {

			if (!fireArray [j].targetted && !fireArray [j].scanned) {

				for (int i = 0; i < droneArray.Length; i++) {

					if (!droneArray [i].busy) {

						float d = Vector3.Distance (droneArray [i].transform.position, fireArray [j].transform.position);
						if (d < sDistance) {
							sDistance = d;
							closestDrone = droneArray [i];
							closestFire = fireArray [j];
						}
					}
				}
			}
		}

		if (closestDrone != null && closestFire != null) {
			closestDrone.setFire(closestFire);
			closestDrone.busy = true;
			closestFire.targetted = true;
		}
	}

	public void addFire(Fire newFire){

		fireCount++;
		fireArray.Add (newFire);

	}

	public string[] getDroneStats(int i){

		string[] output = new string[droneArray.Length];

		for (int x = 0; x < output.Length; x++) {

			string ln = i + "," + x + ",";
			ln = ln + droneArray [x].getScanningTime() + ",";
			ln = ln + droneArray [x].getRposingTime() + "";

			output [x] = ln;
		}
			
		return output;
	}

	public string[] getFireStats(int i){

		string[] output = new string[fireArray.ToArray().Length];

		for (int x = 0; x < output.Length; x++) {

			string ln = i + "," + x + ",";
			ln = ln + fireArray [x].getTimeBeingScanned() + ",";
			ln = ln + fireArray [x].getTimeSpentBurning() + "";

			output [x] = ln;
		}

		return output;
	}
}
