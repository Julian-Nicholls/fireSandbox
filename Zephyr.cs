using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zephyr : MonoBehaviour {

	//the fire this drone has been assigned to scan
	public Fire targetFire;

	//how fast the drone can move
	public float flightSpeed;

	//how fast the drone can corner (does not affect performance atm)
	public float turnSpeed;

	//how larger the scanner radius is
	public float footprintRadius;

	//so that the drone can change colour when it's scanning
	public Material[] materials;
	MeshRenderer[] renderers; 

	//for use by performance tracking and droneManager respectively
	public bool scanning = false;
	public bool busy = false;

	//performance stats
	float tSpentScanning = 0f;
	float tSpentReposng = 0f;

	//how long the drone has been scanning a particular fire
	float localScanTime = 0f;

	//how long the drone must spend scanning it's target fire
	float lstNeeded = 0f;

	//where the drone should return if there's 
	//no fires that need attention
	Vector3 homePos;

	// Use this for initialization
	void Start () {

		//set up the colours
		renderers = gameObject.GetComponentsInChildren<MeshRenderer> ();

		foreach (MeshRenderer mr in renderers) {
			
			mr.material = materials [0];
		}

		//km/h >> km/m
		flightSpeed = flightSpeed / 60;

		//set the start position as the home position
		homePos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		//the drone will always proceed to it's target position
		//that may be it's next assigned fire, or its homePos
		Vector3 targetPos;

		if (targetFire != null) {

			targetPos = new Vector3 (targetFire.transform.position.x, transform.position.y, targetFire.transform.position.z);

		} else {

			targetPos = homePos;
		}
			
		//target direction is derived from targetPos
		Vector3 targetDirection = targetPos - transform.position;

		//rotate the gameObject so animations look good
		float rstep = turnSpeed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDirection, rstep, 0.0f);

		transform.rotation = Quaternion.LookRotation (newDir);

		//if the fire is within scanning distance, start scanning
		if (Vector3.Distance (transform.position, targetPos) < footprintRadius && busy) {

			scanning = true;
			targetFire.beingScanned = true;
		}

		//if the drone is really close to the fire, stop moving;
		// otherwise continue towards targetPos
		if (Vector3.Distance (transform.position, targetPos) > 0.001f) {

			float step = flightSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, targetPos, step);
		}

		if (scanning) {

			//update performance statistics
			tSpentScanning = tSpentScanning + Time.deltaTime;
			localScanTime = localScanTime + Time.deltaTime;

			//if the drone has spent enough time over the fire
			//call it a day and move on to the next objective
			if (localScanTime > lstNeeded) {
				finishFire ();
			}

			//if scanning, change colour
			foreach (MeshRenderer mr in renderers) {

				mr.material = materials [1];
			}

		} else {

			//update performance stats
			tSpentReposng = tSpentReposng + Time.deltaTime;

			//change to "not scanning" colour
			foreach (MeshRenderer mr in renderers) {
				mr.material = materials [0];
			}
		}

	}

	public void setFire(Fire tf){

		//assign a fire to this drone
		targetFire = tf;
		scanning = false;
		busy = true;
		localScanTime = 0f;
		lstNeeded = tf.getScanTime ();
	}

	public void finishFire(){

		//consider this fire scanned 
		targetFire.scanned = true;
		targetFire.targetted = false;
		targetFire.beingScanned = false;

		scanning = false;
		busy = false;
		targetFire = null;
	}

	public float getScanningTime(){

		//get performance stats
		return tSpentScanning;
	}
		

	public float getRposingTime(){

		//get performance stats
		return tSpentReposng;
	}
}
