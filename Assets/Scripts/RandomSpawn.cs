using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSpawn : MonoBehaviour
{	
	public GameObject Player;
	public GameObject House;
	public float XPos, YPos, ZPos;
	
	private Terrain terrain;
	private Vector3 terrainArea;
	
    void Start( )
    {
     	terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
        terrainArea = terrain.terrainData.size;
	    
		float XMax = terrainArea.x;	
		float ZMax = terrainArea.z;
	    
	    XPos = Random.Range(0, XMax);
		ZPos = Random.Range(0, ZMax);
	    YPos = terrain.SampleHeight(new Vector3(XPos, 0, ZPos));
		
		Vector3 v = new Vector3(XPos, YPos, ZPos);
		var ply = Instantiate(Player, v, Quaternion.identity);
	    HintManager.Instance.AddPlayer();
	    PlayerManager.player = ply;
	    
	    
	    XPos = Random.Range(0, XMax);
	    ZPos = Random.Range(0, ZMax);
	    YPos = terrain.SampleHeight(new Vector3(XPos, 0, ZPos));
		
	    v = new Vector3(XPos, YPos, ZPos);
	    var inst = Instantiate(House, v, Quaternion.identity);
	    HintManager.Instance.AddGoal(inst);
    } 
}
