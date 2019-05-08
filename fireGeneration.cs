using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireGeneration : MonoBehaviour {

	/*
	 * fireGeneration creates the fires on the landscape 
	 * using a poisson process. fireGeneration may be changed 
	 * based on the specified situation.
	 */

	//link to simTime
	public GameObject simTime;

	//average fire arrival interval for poisson process
	public int lambdar;

	public GameObject landscape;
	public Fire firePrefab;
	public GameObject droneManager;

	//taken straight from Distribution Manager asset
	/// <summary>
	/// Sample from Poisson Distribution
	/// </summary>
	/// <param name = "lambda" > Parameter for Poisson distribution </param>
	/// <param name = "sampleLenght" > sample array lenght </param>
	/// <returns> Array with integers </returns>

	/*
	 * this function was borrowed from a package and returns 
	 * fire arrival intervals (in whole numbers) defined
	 * by a poisson process
	 */

    /*
	public static int[] PoissSample (float lambda, int sampleLenght)
	{
		if(lambda > 0)
		{
			int[] sample = new int[sampleLenght];
			for(int i = 0; i < sample.Length; i++)
			{
				float expLambda = Mathf.Exp(-1 * lambda);
				int sampleValue = 1;
				float p = UnityEngine.Random.Range(0f,1f);
				while(p > expLambda)
				{
					sampleValue++;
					p *= UnityEngine.Random.Range(0f,1f);
				}
				sample[i] = sampleValue;
			}
			return sample;
		} else
		{
			Debug.Log("ERROR");
			Debug.Log("Lambda < 0");
			return null;
		}
	}
    */

	public int fireNum = 0;
	int[] temp_arrivals;
	float[] arrivals;
	droneManager dm;

	// Use this for initialization
	void Start () {

        dm = droneManager.GetComponent<droneManager>();

        //get an array of fire arrival intervals
        //temp_arrivals = PoissSample (lambdar, 24);
		
        /*
		//convert arrival intervals from hours to minutes
		for (int x = 0; x < temp_arrivals.Length; x++ ) {
			temp_arrivals [x] = temp_arrivals [x] * 60;
		}

		//convert fire arrival intervals to fire arrival times
		float[] temp = new float[temp_arrivals.Length];
		float sum = 0;

		for (int x = 0; x < temp_arrivals.Length; x++ ) {

			sum = sum + temp_arrivals [x];
			temp [x] = sum;
		}
		arrivals = temp;
        */

		//arrive the fires
        for(int i = 0; i < fireNum; i++)
        {

            spawnFire();
        }
		

	}
	
    /*
	// Update is called once per frame
	void Update () {

		//if time > the next fire arrival time
		//create a new fire
		if(simTime.GetComponent<simTime>().elapsedTime > arrivals[fireNum]){

			fireNum++;
			spawnFire ();
		}
	}
    */

	void spawnFire (){

		//create a fire pseudo-randomly on the landscape
		float fireX = Random.Range (-132, 132);
		float fireZ = Random.Range (-132, 132);

		Vector3 firePos = new Vector3 (fireX, 1, fireZ);

		Fire newFire = Instantiate (firePrefab, firePos, new Quaternion ()) as Fire; 

		//alert droneMAnager to the new fire
		dm.addFire (newFire);
	}
}
