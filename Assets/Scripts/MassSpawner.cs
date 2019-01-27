using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MassSpawner : MonoBehaviour
{
    [Serializable]
    public class SpawnableObjects
    {
        public GameObject prefab;
        public int count;
    }
    [SerializeField] private SpawnableObjects[] SpawnCounts;

    private void Awake()
    {
        var terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
        var terrainArea = terrain.terrainData.size;
        foreach(var entry in SpawnCounts)
        {
            var holder = new GameObject(entry.prefab.name).transform;
            for (int i = 0; i < entry.count; i++)
            {
                float tmpMax = terrainArea.x;
                float xPos = Random.Range(0, tmpMax);
                tmpMax = terrainArea.z;
                float zPos = Random.Range(0, tmpMax);
                float yPos = terrain.SampleHeight(new Vector3(xPos, 0, zPos));
                
                Vector3 vec = new Vector3(xPos, yPos - 0.01f, zPos);
                GameObject instance = Instantiate(entry.prefab, vec, Quaternion.Euler(0, Random.Range(0, 360), 0));
                //instance.transform.rotation = Random.rotation;
                instance.transform.SetParent(holder);
            }
        }
    }
}
