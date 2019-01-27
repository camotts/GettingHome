using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    private static HintManager singleton = null;

    public static HintManager Instance
    {
        get
        {
            if (singleton == null)
            {
                singleton = FindObjectOfType<HintManager>();
            }

            if (singleton == null)
            {
                var obj = new GameObject("HintManager");
                singleton = obj.AddComponent<HintManager>();
            }

            return singleton;
        }
    }
    
    
    private Stack<GameObject> Hints = new Stack<GameObject>();
    
    public int NumStages = 5;
    public int DistanceBetweenPoints = 150;
    public GameObject[] RandomizedStages;
    public Vector3[] PointsLocations;
	
    private Terrain terrain;
    private Vector3 terrainArea;
    private Vector3 PreviouslyCreatedPoint;
    
    private bool playerAdded;

    private GameObject Goal;

    private void Awake()
    {
        PointsLocations = new Vector3[NumStages];
    }

    void Update()
    {
        var pctOff = Vector3.Angle(PlayerManager.player.transform.forward, Goal.transform.position)/180.0f;
    }

    void MakePoints()
    {
        var dist = Vector3.Distance(PlayerManager.player.transform.position, Goal.transform.position);
        var splits = dist / (NumStages+1);
        var hintHolder = new GameObject("Hints").transform;
        IEnumerable<int> range = Enumerable.Range(0, NumStages);
        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
        var currPoint = Goal.transform.position; 
        foreach (int num in range)
        {
            currPoint = Vector3.MoveTowards(currPoint, PlayerManager.player.transform.position, splits);
            PointsLocations[num] = new Vector3(currPoint.x, terrain.SampleHeight(new Vector3(currPoint.x, 0, currPoint.z)), currPoint.z);
            Debug.LogFormat("Creating points {0} at ({1},{2},{3})", num+1, currPoint.x, currPoint.y, currPoint.z);
            var inst = Instantiate(RandomizedStages[Random.Range(0, RandomizedStages.Length)], PointsLocations[num], Quaternion.Euler(0, Random.Range(0, 360), 0));
            inst.transform.SetParent(hintHolder);
            Instance.AddHint(inst);
        }
        Instance.StartHints();
    }
	
    Vector3 MakePointsHelper(int num)
    {
        float XPos, YPos, ZPos;
        
        terrainArea = terrain.terrainData.size;
	    
        XPos = Random.Range(0, terrainArea.x);	
        ZPos = Random.Range(0, terrainArea.z);
        YPos = terrain.SampleHeight(new Vector3(XPos, 0, ZPos));
			
        return new Vector3(XPos, YPos, ZPos);
    }

    public void StartHints()
    {
        Hints.Peek().GetComponent<IHintable>().EnableHint();
    }

    public bool AddHint(GameObject hint)
    {
        var hintable = hint.GetComponent<IHintable>();
        if (hintable == null)
        {
            return false;
        }
        hintable.DisableHint();
        Hints.Push(hint);
        return true;
    }

    public void AddGoal(GameObject goal)
    {
        Goal = goal;
        var hintable = Goal.GetComponent<IHintable>();
        hintable.DisableHint();
        if (playerAdded)
        {
            Instance.MakePoints();
        }
    }

    public void AddPlayer()
    {
        playerAdded = true;
        if (Goal != null)
        {
            Instance.MakePoints();
        }
    }

    public bool CompleteHint(GameObject hint)
    {
        if (Hints.Peek() != hint) return false;
        Hints.Pop();
        if (Hints.Count > 0)
        {
            Hints.Peek().GetComponent<IHintable>().EnableHint();
        }
        else
        {
            Goal.GetComponent<IHintable>().EnableHint();
        }

        return true;
    }
}
