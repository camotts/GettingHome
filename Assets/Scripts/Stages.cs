using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using Random = UnityEngine.Random;

public class Stages : MonoBehaviour
{
    public static int NumStages = 5;
	public static int DistanceBetweenPoints = 150;
    	public GameObject RandomizedStages;
	public Vector3[] PointsLocations = new Vector3[NumStages];
	
	private Terrain terrain;
	private Vector3 terrainArea;
	private Vector3 PreviouslyCreatedPoint;

    // Start is called before the first frame update
    void Start()
    {
     	MakePoints();
    }

    void MakePoints()
	{
			IEnumerable<int> range = Enumerable.Range(0, NumStages);
			Vector3 CurrentPoint = new Vector3();
			
			foreach (int num in range)
			{
			    bool TooClose = true;
				while (TooClose)
				{
					CurrentPoint = MakePointsHelper(num);
					if (num == 0) TooClose = false;
					float distance = Vector3.Distance(CurrentPoint, PreviouslyCreatedPoint);
					if (distance > DistanceBetweenPoints)
					{
						PreviouslyCreatedPoint = CurrentPoint;
						TooClose = false;
					}
				}
				PointsLocations[num] = CurrentPoint;
				Debug.LogFormat("Creating points {0} at ({1},{2},{3})", num+1, CurrentPoint.x, CurrentPoint.y, CurrentPoint.z);
				Instantiate(RandomizedStages, PointsLocations[num], Quaternion.Euler(0, Random.Range(0, 360), 0));
			}
	}
	
	Vector3 MakePointsHelper(int num)
	{
		float XPos, YPos, ZPos;
		terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
        terrainArea = terrain.terrainData.size;
	    
		XPos = Random.Range(0, terrainArea.x);	
		ZPos = Random.Range(0, terrainArea.z);
	    	YPos = terrain.SampleHeight(new Vector3(XPos, 0, ZPos));
			
		return new Vector3(XPos, YPos, ZPos);
	}
}
