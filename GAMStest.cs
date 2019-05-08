using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GAMS;
using System;
using System.IO;
using System.Text;

public class GAMStest : MonoBehaviour {

    public List<Transform> transforms = new List<Transform>();
    public AppModel am;

    public Stack<Vector3> route = new Stack<Vector3>();

    public List<GameObject> lines;
    public Material lineMaterial;
    // Use this for initialization
    public void Init () {

        string inputPath, modelPath, setPath;

        if (am.onLaptop)
        {
            inputPath = "C:/Users/User/Desktop/SCHOOL/Unity Model/fire sandbox/Assets/Scripts/GAMSinput.txt";
            modelPath = "C:/Users/User/Desktop/SCHOOL/Unity Model/fire sandbox/Assets/Scripts/GAMSmodel.gms";
            setPath = "C:/Users/User/Desktop/SCHOOL/Unity Model/fire sandbox/Assets/Scripts/GAMSset.txt";
        }
        else
        {
            inputPath = "D:/Users/Julian/Documents/GitHub/FireSandbox/fire sandbox/Assets/Scripts/GAMSinput.txt";
            modelPath = "D:/Users/Julian/Documents/GitHub/FireSandbox/fire sandbox/Assets/Scripts/GAMSmodel.gms";
            setPath = "D:/Users/Julian/Documents/GitHub/FireSandbox/fire sandbox/Assets/Scripts/GAMSset.txt";
        }
        
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
            }
        }

        using (StreamWriter writetext = new StreamWriter(inputPath, false))
        {
            //write out the data line by line to .CSV
            foreach (string txt in data)
            {
                writetext.WriteLine(txt);
            }
        }

        using (StreamWriter writetext = new StreamWriter(setPath, false))
        {
            int i = transforms.Count;
            writetext.WriteLine("i / i1 * i" + i + " /; ");
        }

        GAMSWorkspace ws = new GAMSWorkspace();
        GAMSJob t0 = ws.AddJobFromFile(modelPath);
        t0.Run();

        print("ran with default");

        int[,] matrix = new int[transforms.Count, transforms.Count];

        foreach (GAMSVariableRecord rec in t0.OutDB.GetVariable("x"))
        {

            //print("x(" + rec.Keys[0] + "," + rec.Keys[1] + "): level=" +
            //rec.Level + " marginal=" + rec.Marginal);

            //print("z=" + t0.OutDB.GetVariable("z").LastRecord().Level);

            string xString = rec.Keys[0];
            string yString = rec.Keys[1];
            double level = rec.Level;
            double z = t0.OutDB.GetVariable("z").LastRecord().Level;

            int x = int.Parse(xString[1] + "") - 1;
            int y = int.Parse(yString[1] + "") - 1;

            //print(x + " " + y + " " + level);

            if(level == 1)
            {
                GameObject line = new GameObject();
                line.transform.position = transforms[y].transform.position;
                line.AddComponent<LineRenderer>();

                LineRenderer lr = line.GetComponent<LineRenderer>();
                lr.material = lineMaterial;
                lr.startColor = Color.blue;
                lr.startWidth = 0.5f;

                lr.SetPosition(0, transforms[x].transform.position);
                lr.SetPosition(1, transforms[y].transform.position);

                lines.Add(line);

                //GameObject.Destroy(line, 2);

                Debug.DrawLine(transforms[x].transform.position, transforms[y].transform.position, Color.green, 10f);

                if(matrix[x,y] == 0 && matrix[y, x] == 0)
                {
                    matrix[x,y] = 1;
                }
            }
        }

        int curr = 0;
        int next = 0;

        for (int outer = 0; outer < transforms.Count-1; outer++)
        {
            for (int i = 0; i < transforms.Count; i++)
            {
                if (matrix[curr, i] == 1)
                {
                    next = i;
                }
            }

            curr = next;
            route.Push(transforms[curr].transform.position);
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
