using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * droneManager is used to coordinate the use of drones. While 
 * each drone is an independent agent, a centralized script
 * is used to assign jobs to drones such that an effective or 
 * indeed optimal solution to a problem is found and implemented. 
 * This is where TSP Solving occurs, or where heuristic practices 
 * are evaluated.
 */

public class droneManager : MonoBehaviour {

	//The desired # of drones active in the simulation
	public int numOfDrone;

	//The landscape droneManager is protecting from fire
	public GameObject landscape;

	//The stock drone gameObject
	public Zephyr dronePrefab;

    public GAMStest gams;

	//List of fires on the landscape
	public List<Fire> fireArray = new List<Fire>();

	//Array of drones in use by droneManager
	public Zephyr[] droneArray;

	//This should be derived from fireArray
	int fireCount = 0;

    Zephyr theDrone;

	// Use this for initialization
	void Start () {

        /*
		Vector3[] startPoss = new Vector3[2];

		//For one or two drones, start positions are defined
		//to help reduce response times (hueristic)
		if (numOfDrone == 1) {
			startPoss [0] = new Vector3 (0, 21, 0);
			startPoss [1] = new Vector3 (0, 21, 0);
		}
		if (numOfDrone == 2) {
			startPoss [0] = new Vector3 (75, 21, 75);
			startPoss [1] = new Vector3 (-75, 21, -75);
		}

		//Initialize all the required drones
		droneArray = new Zephyr[numOfDrone];

		for (int x = 0; x < numOfDrone; x++) {

			Zephyr newDrone = Instantiate (dronePrefab, startPoss[x], new Quaternion (0,0,0,0));
			droneArray [x] = newDrone;
		}
        */

        theDrone = Instantiate(dronePrefab, new Vector3(0, 21, 0), new Quaternion(0, 0, 0, 0));

        gams.transforms.Add(theDrone.transform);

        foreach (Fire f in fireArray)
        {

            gams.transforms.Add(f.transform);
        }

        gams.Init();
    }



	
    
	// Update is called once per frame
	void Update () {

        /*
         * This is a hueristic strategy in which each unscanned
         * fire is assigned to the nearest drone that is free
         *
         */

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
        }
        */

        if(!theDrone.busy)
        {
            Fire tarFire = null;
            float dist = float.MaxValue;

            Vector3 locale = gams.route.Pop();
            print(locale);

            foreach(Fire f in fireArray)
            {
                if (Vector3.Distance(locale, f.transform.position) < dist)
                {
                    tarFire = f;
                    dist = Vector3.Distance(locale, f.transform.position);
                }
            }

            theDrone.setFire(tarFire);

        }
	}
    

	public void addFire(Fire newFire){

		/*
		 * As fires arrive on the landscape, they are added
		 * to the fireArray.
		 */

		fireCount++;
		fireArray.Add (newFire);
	}

	public string[] getDroneStats(int i){

		/*
		 * Each drone is responsible for tracking its own 
		 * performance (like hours spent scanning). This function
		 * aggregrates performance and prepares it for output 
		 * to a text file. This is highly variable as different 
		 * configurations demand different performance metrics.
		 */

		//each string in output is a line of a .CSV
		//in this case one line per drone
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

		/*
		 * Each fire is responsible for tracking its own 
		 * statistics (like hours spent burning). This function
		 * aggregrates performance and prepares it for output 
		 * to a text file. This is highly variable as different 
		 * configurations demand different performance metrics.
		 */

		//each string in output is a line of a .CSV
		//in this case one line per fire
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
