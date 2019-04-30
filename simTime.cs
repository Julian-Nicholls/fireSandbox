using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class simTime : MonoBehaviour {

	/*
	 * simTime monitors the time in the simulation
	 * and uses it to reload the scene upon reaching
	 * the desired temporal length.
	 */

	//EVERY SECOND IN SIM TIME IS 1 MIN IRL
	//sim duration in hours

	//how long to run the sim for
	public float duration;

	public float elapsedTime = 0;

	//assign am and dm for communication
	public droneManager dm;
	public AppModel am; 

	//monitor the state of the simulation
	public bool running = true;
	public bool written = false;

	//where to write output to
	string path = @"D:\Users\Julian\Documents\GitHub\FireSandbox\fire sandbox\ScienceData\tester.txt";
	string[] data;

	// Use this for initialization
	void Start () {

		//track duration in minutes
		duration = duration * 60;

		//timeScale defines how fast the simulation runs
		Time.timeScale = 100;
	}
	
	// Update is called once per frame
	void Update () {

		//track time elapsed since the start
		elapsedTime = elapsedTime + Time.deltaTime;

		//if we've reached the end of the replication
		//and haven't written results, do that
		if (elapsedTime > duration && !written) {

			print ("duration reached");

			//get desired performance stats
			data = dm.getFireStats(am.getIter());

			using(StreamWriter writetext = new StreamWriter(path, true))
			{
				//write out the data line by line to .CSV
				foreach (string txt in data) {
					writetext.WriteLine (txt);
				}
			}

			written = true;

			//either end the simulation of reinitialize 
			// a new replication
			if (am.getIter() == am.maxIterations) {

				print ("it's over, it's really over");
				written = true;
				Time.timeScale = 0;

			} else {

				print ("reinitializing sim");
				am.setIter (am.getIter() + 1);
				SceneManager.LoadScene("fire sandbox") ;
			}
		}
	}
}
