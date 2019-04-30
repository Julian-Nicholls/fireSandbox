using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppModel : MonoBehaviour {

	public int maxIterations = 4;
	public static int sceneIteration = 0; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setIter(int x){
		sceneIteration = x;
	}

	public int getIter(){
		return sceneIteration;
	}
}
