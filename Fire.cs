using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	//how fast the fire is growing
	public float growthRate;

	//not sure if this is being used
	public int numOfFires;

	//how big the fire is 
	public float size;

	//these are used by droneManager to know the 
	//present state of the fire
	public bool detected = false; 
	public bool scanned = false;
	public bool targetted = false;
	public bool beingScanned = false;

	//for use in elliptcal fire growth
	float radiusA = 0;
	float radiusB = 0;

	//performance statistics
	float timeSpentScanned = 0;
	float timeSpentBurning = 0; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//the fire grows a little every frame
		radiusA = radiusA + (growthRate * Time.deltaTime);
		radiusB = radiusA / 2;

		//in m^2
		size = Mathf.PI * radiusA * radiusB;

		//convert to square kilometers (?)
		size = size / 10000;

		//update total burn time
		timeSpentBurning = timeSpentBurning + Time.deltaTime;

		if (beingScanned) {

			//update total scanned time
			timeSpentScanned = timeSpentScanned + Time.deltaTime;
		}
	}

	public float getTimeBeingScanned(){
		//return performance stat
		return timeSpentScanned;
	}

	public float getTimeSpentBurning(){
		//return performance stats
		return timeSpentBurning;
	}

	public float getScanTime(){

		//return how long it will take to scan the fire
		//in seconds?
		//minutes probably?
		return 2 * 60;
	}

}
