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

	public float timeScale;

	string path = @"C:\Users\User\Desktop\tester.txt";
	string[] data;

	// Use this for initialization
	void Start () {

		duration = duration * 60;

		Time.timeScale = timeScale;
	}
	
	// Update is called once per frame
	void Update () {

		elapsedTime = elapsedTime + Time.deltaTime;

		if (dm.complete && !written) {

			print ("duration reached");
			written = true;

			using(StreamWriter writetext = new StreamWriter(path, true))
			{
				
				writetext.WriteLine (am.getIter() + ", " + elapsedTime);

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
