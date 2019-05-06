using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GAMS;
using System;
using System.IO;
using System.Text;

public class GAMStest : MonoBehaviour {

    public List<Transform> transforms = new List<Transform>();

    // Use this for initialization
    void Start () {

        float[,] distanceMatrix = new float[transforms.Count, transforms.Count];

        string path = "C:/Users/User/Desktop/SCHOOL/Unity Model/fire sandbox/Assets/Scripts/GAMSinput.txt";
        string[] data = new string[transforms.Count * transforms.Count];
        int count = 0;

        for (int i = 1; i < transforms.Count+1; i++)
        {
            for (int j = 1; j < transforms.Count+1; j++)
            {

                float d = Mathf.Round(Vector3.Distance(transforms[i-1].position, transforms[j-1].position));

                string str = "d('i" + i + "','i" + j + "') = " + d + ";";
                data[count] = str;
                count++;

                print(count);
            }
        }

        using (StreamWriter writetext = new StreamWriter(path, false))
        {
            //write out the data line by line to .CSV
            foreach (string txt in data)
            {
                writetext.WriteLine(txt);
            }
        }



        GAMSWorkspace ws = new GAMSWorkspace();
        GAMSJob t0 = ws.AddJobFromFile("C:/Users/User/Desktop/SCHOOL/Unity Model/fire sandbox/Assets/Scripts/GAMSmodel.gms");
        t0.Run();

        print("ran with default");

        foreach (GAMSVariableRecord rec in t0.OutDB.GetVariable("x"))
        {

            print("x(" + rec.Keys[0] + "," + rec.Keys[1] + "): level=" +
            rec.Level + " marginal=" + rec.Marginal);

            print("z=" + t0.OutDB.GetVariable("z").LastRecord().Level);

        }











    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void runGAMS()
    {
        GAMSWorkspace ws = new GAMSWorkspace();
        ws.GamsLib("trnsport");

        //create a job from file
        GAMSJob t0 = ws.AddJobFromFile("trnsport.gms");
        t0.Run();

        print("ran with default");

        foreach (GAMSVariableRecord rec in t0.OutDB.GetVariable("x"))
        {

            print("x(" + rec.Keys[0] + "," + rec.Keys[1] + "): level=" +
            rec.Level + " marginal=" + rec.Marginal);

            print("z=" + t0.OutDB.GetVariable("z").LastRecord().Level);

        }

    }
}
