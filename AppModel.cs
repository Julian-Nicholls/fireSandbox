using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The AppModel class is a static script that allows for multiple
 * iterations. Because it's static, it is preserved between scene 
 * loading, and thus can be used to track how many iterations have 
 * occured. 
 */

public class AppModel : MonoBehaviour {

	//maxIterations defines how many replications of the
	//simulation are desired
	public int maxIterations = 4;

	//sceneIteration tracks how many replications have been finished
	public static int sceneIteration = 0;

    public bool onLaptop = false;

	void Start () {
		
	}

	public void setIter(int x){
		sceneIteration = x;
	}

	public int getIter(){
		return sceneIteration;
	}
}
