using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class simTime : MonoBehaviour {

	//EVERY SECOND IN SIM TIME IS 1 MIN IRL
	//sim duration in hours
	public float duration;
	public float elapsedTime = 0;
	public droneManager dm;
	public AppModel am; 

	public bool running = true;
	public bool written = false;

	string path = @"D:\Users\Julian\Documents\GitHub\FireSandbox\fire sandbox\ScienceData\tester.txt";
	string[] data;

	// Use this for initialization
	void Start () {

		duration = duration * 60;

		Time.timeScale = 100;
	}
	
	// Update is called once per frame
	void Update () {

		elapsedTime = elapsedTime + Time.deltaTime;

		if (elapsedTime > duration && !written) {

			print ("duration reached");

			data = dm.getFireStats(am.getIter());
			print (data);

			written = true;

			using(StreamWriter writetext = new StreamWriter(path, true))
			{
				foreach (string txt in data) {
					writetext.WriteLine (txt);
				}
			}

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
