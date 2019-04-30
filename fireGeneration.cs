using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireGeneration : MonoBehaviour {

	public GameObject simTime;
	public int lambdar;
	public GameObject landscape;
	public Fire firePrefab;
	public GameObject droneManager;

	public float landscapeSize;
	public int numOfFires;

	//taken straight from Distribution Manager asset
	/// <summary>
	/// Sample from Poisson Distribution
	/// </summary>
	/// <param name = "lambda" > Parameter for Poisson distribution </param>
	/// <param name = "sampleLenght" > sample array lenght </param>
	/// <returns> Array with integers </returns>
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

	int fireNum = 0;
	int[] temp_arrivals;
	float[] arrivals;
	droneManager dm;


	// Use this for initialization
	void Start () {

		/*
		temp_arrivals = PoissSample (lambdar, 24);


		for (int x = 0; x < temp_arrivals.Length; x++ ) {
			temp_arrivals [x] = temp_arrivals [x] * 60;
		}

		float[] temp = new float[temp_arrivals.Length];
		float sum = 0;

		for (int x = 0; x < temp_arrivals.Length; x++ ) {

			sum = sum + temp_arrivals [x];

			temp [x] = sum;
		}
		arrivals = temp;
		spawnFire ();
		*/

		dm = droneManager.GetComponent<droneManager> ();

		for (int i = 0; i < numOfFires; i++) {
			spawnFire ();
		}
	}
	
	// Update is called once per frame
	void Update () {

		/*
		if(simTime.GetComponent<simTime>().elapsedTime > arrivals[fireNum]){

			fireNum++;

			spawnFire ();
		}*/
			
	}

	void spawnFire (){

		float lw = Mathf.Sqrt (landscapeSize);

		float fireX = Random.Range (-1 * lw, lw);
		float fireZ = Random.Range (-1 * lw, lw);

		Vector3 firePos = new Vector3 (fireX, 0, fireZ);

		Fire newFire = Instantiate (firePrefab, firePos, new Quaternion ()) as Fire; 

		dm.addFire (newFire);

	}
}
