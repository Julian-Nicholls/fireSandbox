using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneManager : MonoBehaviour {

	public int numOfDrone;
	public GameObject landscape;
	public Zephyr dronePrefab;

	public List<Fire> fireArray = new List<Fire>();
	public Zephyr theDrone;
	int nextFire = 0;
	int sRoute = 0;

	bool routed = false;
	public bool complete = false;
	List<int[]> routes = new List<int[]>();

	int fireCount = 0;

	// Use this for initialization
	void Start () {

		/*
		Vector3[] startPoss = new Vector3[2];

		if (numOfDrone == 1) {
			startPoss [0] = new Vector3 (0, 21, 0);
			startPoss [1] = new Vector3 (0, 21, 0);
		}
		if (numOfDrone == 2) {
			startPoss [0] = new Vector3 (75, 21, 75);
			startPoss [1] = new Vector3 (-75, 21, -75);
		}
q	    */

		theDrone = Instantiate (dronePrefab, new Vector3 (0, 21, 0), new Quaternion (0,0,0,0));
	}
	
	// Update is called once per frame
	void Update () {

		/*
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
		}*/

		if (!routed) {

			float sDistance = float.MaxValue;


			int[] poot = new int[fireCount];
			int counter = 0;

			for (int i = 0; i < poot.Length; i++) {
				poot [i] = counter;
				counter++;
			}

			heapRecurse (poot, poot.Length,  poot.Length);
			routed = true;
			print (routes.Count);

			for (int x = 0; x < routes.Count; x++) {

				float cDistance = 0;

				for (int y = 0; y < routes [x].Length-1; y++) {
					Fire f1 = fireArray [routes [x] [y]];
					Fire f2 = fireArray [routes [x] [y + 1]];

					float d = Vector3.Distance (f1.transform.position, f2.transform.position);
					cDistance = cDistance + d;
				}

				if (cDistance < sDistance) {
					sDistance = cDistance;
					sRoute = x;
				}
					
				//print ("route: " + x + ", distance: " + cDistance);
			}

		}

		if (!theDrone.busy && nextFire < fireArray.Count) {
			theDrone.setFire (fireArray [routes [sRoute] [nextFire]]);
			nextFire++;

		} 

		if(nextFire == fireArray.Count && !theDrone.busy){
			complete = true;
		}

	}

	public void heapRecurse(int[] a, int size, int n){

		//base case
		if (size == 1) {

			int[] newArr = new int[n];
			for (int i = 0; i < n; i++) {
				newArr[i] = a [i];
			}
			/*
			string str = "";
			for (int i = 0; i < n; i++) {
				str = str + newArr [i];
			}
			print (str);
			*/
			routes.Add (newArr);
		}

		for (int i = 0; i < size; i++) {
			heapRecurse (a, size - 1, n);

			if (size % 2 == 1) {
				int temp = a [0];
				a [0] = a [size - 1];
				a [size - 1] = temp;

			} 
			else {
				int temp = a [i];
				a [i] = a [size - 1];
				a [size - 1] = temp;
			}
		}
	}

	public void addFire(Fire newFire){

		fireCount++;
		fireArray.Add (newFire);

	}

	/*public string[] getDroneStats(int i){

		string[] output = new string[droneArray.Length];

		for (int x = 0; x < output.Length; x++) {

			string ln = i + "," + x + ",";
			ln = ln + droneArray [x].getScanningTime() + ",";
			ln = ln + droneArray [x].getRposingTime() + "";

			output [x] = ln;
		}
			
		return output;
	}*/

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
