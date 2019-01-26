using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class HousePlacement : MonoBehaviour
{
    public GameObject House;
    public Vector3 terrainArea;
    void Awake()
    {
        var ter = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
        terrainArea = ter.terrainData.size;

        var tmpmax = terrainArea.x;
        var XPos = Random.Range(0, tmpmax);
        tmpmax = terrainArea.z;
        var ZPos = Random.Range(0, tmpmax);
        var YPos = Terrain.activeTerrain.SampleHeight(new Vector3(XPos, 0, ZPos));
        var bounds = House.GetComponent<MeshFilter>().sharedMesh.bounds.extents;

        Debug.LogFormat("height: {0}", bounds.y);

        YPos = YPos + 50;

        Vector3 v1 = new Vector3(XPos, YPos, ZPos);
    
        Debug.LogFormat("Placed house at: ({0},{1},{2})", XPos, YPos, ZPos);

        Instantiate(House, v1, Quaternion.identity);
    }
}
