using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	public float growthRate;
	public int numOfFires;
	public float size;

	public bool detected = false; 
	public bool scanned = false;
	public bool targetted = false;
	public bool beingScanned = false;

	float radiusA = 0;
	float radiusB = 0;

	float timeSpentScanned = 0;
	float timeSpentBurning = 0; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		radiusA = radiusA + (growthRate * Time.deltaTime);
		radiusB = radiusA / 2;

		//in m^2
		size = Mathf.PI * radiusA * radiusB;

		size = size / 10000;

		timeSpentBurning = timeSpentBurning + Time.deltaTime;

		if (beingScanned) {

			timeSpentScanned = timeSpentScanned + Time.deltaTime;
		}
	}

	public float getTimeBeingScanned(){
		return timeSpentScanned;
	}

	public float getTimeSpentBurning(){
		return timeSpentBurning;
	}

	public float getScanTime(){

		//in seconds?
		//minutes probably?
		return 2 * 60;
	}

}
