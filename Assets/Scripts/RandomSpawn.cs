using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSpawn : MonoBehaviour
{	
	public GameObject Player;
	public float XPos, YPos, ZPos;
	
	private Terrain terrain;
	private Vector3 terrainArea;
	
    void Start( )
    {
     	terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
        terrainArea = terrain.terrainData.size;
	    
		float XMax = terrainArea.x;
		XPos = Random.Range(0, XMax);	
		float ZMax = terrainArea.z;
		ZPos = Random.Range(0, ZMax);
	    YPos = terrain.SampleHeight(new Vector3(XPos, 0, ZPos));
		
		Vector3 v = new Vector3(XPos, YPos, ZPos);
		Instantiate(Player, v, Quaternion.identity);
    } 
}
