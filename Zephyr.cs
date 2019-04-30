using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zephyr : MonoBehaviour {

	public Fire targetFire;
	public float flightSpeed;
	public float turnSpeed;
	public float footprintRadius;
	public Material[] materials;

	public bool scanning = false;
	public bool busy = false;

	float tSpentScanning = 0f;
	float tSpentReposng = 0f;
	MeshRenderer[] renderers; 
	float localScanTime = 0f;
	float lstNeeded = 0f;

	Vector3 homePos;

	// Use this for initialization
	void Start () {

		renderers = gameObject.GetComponentsInChildren<MeshRenderer> ();

		foreach (MeshRenderer mr in renderers) {
			
			mr.material = materials [0];
		}

		//km/h >> km/m
		flightSpeed = flightSpeed / 60;

		homePos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 targetPos;

		if (targetFire != null) {

			targetPos = new Vector3 (targetFire.transform.position.x, transform.position.y, targetFire.transform.position.z);

		} else {

			targetPos = homePos;

		}

		Vector3 targetDirection = targetPos - transform.position;

		float rstep = turnSpeed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDirection, rstep, 0.0f);

		transform.rotation = Quaternion.LookRotation (newDir);

		if (Vector3.Distance (transform.position, targetPos) < footprintRadius && busy) {

			scanning = true;
			targetFire.beingScanned = true;
		}

		if (Vector3.Distance (transform.position, targetPos) > 0.001f) {

			float step = flightSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, targetPos, step);
		}

		if (scanning) {

			tSpentScanning = tSpentScanning + Time.deltaTime;
			localScanTime = localScanTime + Time.deltaTime;

			if (localScanTime > lstNeeded) {
				finishFire ();
			}

			foreach (MeshRenderer mr in renderers) {

				mr.material = materials [1];
			}

		} else {

			tSpentReposng = tSpentReposng + Time.deltaTime;

			foreach (MeshRenderer mr in renderers) {
				mr.material = materials [0];
			}
		}

	}

	public void setFire(Fire tf){

		targetFire = tf;
		scanning = false;
		busy = true;
		localScanTime = 0f;
		lstNeeded = tf.getScanTime ();
	}

	public void finishFire(){
		targetFire.scanned = true;
		targetFire.targetted = false;
		targetFire.beingScanned = false;

		scanning = false;
		busy = false;
		targetFire = null;
	}

	public float getScanningTime(){
		return tSpentScanning;
	}
		

	public float getRposingTime(){
		return tSpentReposng;
	}
}
